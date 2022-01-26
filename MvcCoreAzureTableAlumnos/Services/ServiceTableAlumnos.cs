using Microsoft.Azure.Cosmos.Table;
using MvcCoreAzureTableAlumnos.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MvcCoreAzureTableAlumnos.Services
{
    public class ServiceTableAlumnos
    {
        private string UrlApi;
        private string UrlTableAlumnos;
        public ServiceTableAlumnos(string UrlApi, string UrlTableAlumnos)
        {
            this.UrlApi = UrlApi;
            this.UrlTableAlumnos = UrlTableAlumnos;
        }
        //Tendremos un método para leer el token del servicio API
        public async Task<string> GetTokenAsync(string curso)
        {
            string request = "/api/AlumnosKeys/token/" + curso;
            using(WebClient client = new WebClient())
            {
                client.Headers["content-type"] = "application/json";
                Uri uri = new Uri(this.UrlApi + request);
                //Descargamos el contenido JSON
                string contenido =
                    await client.DownloadStringTaskAsync(uri);
                //Convertimos el string a objeto JSON
                JObject jobject = JObject.Parse(contenido);
                //Recuperamos la KEY token del documento JSON
                string token = jobject.GetValue("token").ToString();
                return token;
            }
        }

        //Método para mostrar los alumnos a partir del token 
        //Para acceder se necesita token y url de acceso
        public List<Alumno> GetAlumnos(string token)
        {
            StorageCredentials credentials =
                new StorageCredentials(token);
            //Cuando recuperamos el table client debemos hacerlo con la URL y las credenciales
            CloudTableClient client =
                new CloudTableClient(new Uri(this.UrlTableAlumnos), credentials);
            //Recuperamos nuestra tabla de alumnos
            CloudTable tabla = client.GetTableReference("tablaalumnos");
            //Ejecutamos una consulta y nos traemos los alumnos que tengamos con permiso
            TableQuery<Alumno> query = new TableQuery<Alumno>();
            List<Alumno> alumnos = tabla.ExecuteQuery(query).ToList();
            return alumnos;
        }

    }
}
