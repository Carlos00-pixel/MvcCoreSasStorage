using Azure.Data.Tables;
using MvcCoreSasStorage.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MvcCoreSasStorage.Services
{
    public class ServiceAzureAlumnos
    {
        private TableClient tableAlumnos;

        private string UrlApi;

        public ServiceAzureAlumnos(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiTableTokens");
        }

        public async Task<List<Alumno>> GetAlumnosAsync(string token)
        {
            Uri uriToken = new Uri(token);
            this.tableAlumnos = new TableClient(uriToken);
            List<Alumno> alumnos = new List<Alumno>();
            var consulta = this.tableAlumnos.QueryAsync<Alumno>
                (filter: "");

            await foreach (Alumno al in consulta)
            {
                alumnos.Add(al);
            }
            return alumnos;
        }

        public async Task<string> GetTokenAsync(string curso)
        {
            using(WebClient client = new WebClient())
            {
                string request =
                    "api/tabletoken/generatetoken/" + curso;
                client.Headers["content-type"] = "application/json";
                Uri uri = new Uri(this.UrlApi + request);
                string data = await client.DownloadStringTaskAsync(uri);
                JObject objetoJSON = JObject.Parse(data);
                string token = objetoJSON.GetValue("token").ToString();
                return token;
            }
        }
    }
}
