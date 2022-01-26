using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreAzureTableAlumnos.Models
{
    public class Alumno : TableEntity
    {
        public string IdAlumno { get; set; }
        public string Curso { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Nota { get; set; }
    }
}
