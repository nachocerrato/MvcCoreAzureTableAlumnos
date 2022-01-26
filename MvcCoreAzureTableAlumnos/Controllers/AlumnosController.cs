using Microsoft.AspNetCore.Mvc;
using MvcCoreAzureTableAlumnos.Models;
using MvcCoreAzureTableAlumnos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreAzureTableAlumnos.Controllers
{
    public class AlumnosController : Controller
    {
        private ServiceTableAlumnos service;

        public AlumnosController(ServiceTableAlumnos service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string curso)
        {
            //Para poder realizar acciones necesitamos el token
            string token =
                await this.service.GetTokenAsync(curso);
            //Extraemos todos los alumnos que nos deje el token
            List<Alumno> alumnos = this.service.GetAlumnos(token);
            return View(alumnos);
        }
    }
}
