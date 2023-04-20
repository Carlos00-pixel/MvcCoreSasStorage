using Azure.Data.Tables;
using MvcCoreSasStorage.Helpers;
using MvcCoreSasStorage.Models;
using System.Xml.Linq;

namespace MvcCoreSasStorage.Services
{
    public class ServiceStorageAlumnosXML
    {
        private TableClient tableAlumno;
        private HelperPathProvider helper;

        public ServiceStorageAlumnosXML
            (TableServiceClient tableService, HelperPathProvider helper)
        {
            this.tableAlumno = tableService.GetTableClient("alumnos");
            Task.Run(async () =>
            {
                await this.tableAlumno.CreateIfNotExistsAsync();
            });
            this.helper = helper;
        }

        public List<Alumno> MostrarAlumnos()
        {
            string path = helper.MapPath("alumnos_tables.xml", Folders.Documents);

            XDocument document = XDocument.Load(path);
            List<Alumno> alumnos = new List<Alumno>();
            var consulta = from datos in document.Descendants("alumno")
                           select datos;

            foreach (XElement tag in consulta)
            {
                Alumno alumno = new Alumno();

                alumno.IdAlumno = int.Parse(tag.Element("idalumno").Value);
                alumno.Curso = tag.Element("curso").Value;
                alumno.Nombre = tag.Element("nombre").Value;
                alumno.Apellidos = tag.Element("apellidos").Value;
                alumno.Nota = int.Parse(tag.Element("nota").Value);

                alumnos.Add(alumno);
            }

            return alumnos;
        }

        public async Task InsertarAlumnos(List<Alumno> alumnos)
        {
            foreach(Alumno item in alumnos)
            {
               await this.tableAlumno.AddEntityAsync<Alumno>(item);
            }
        }
    }
}
