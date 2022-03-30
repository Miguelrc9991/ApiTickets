using ApiTickets.Data;
using ApiTickets.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiTickets.Repositories
{
    public class RepositoryTickets
    {
        private TicketsContext context;
        private MediaTypeWithQualityHeaderValue Header;

        public RepositoryTickets(TicketsContext context)
        {
           

            this.Header = new MediaTypeWithQualityHeaderValue("application/json");

            this.context = context;
        }
     

        public List<Ticket> GetTickets()
        {
            return this.context.Tickets.ToList();
        }
        private int GetMaxIdUsuario()
        {
            int id = 0;
            if (this.context.Usuarios.Count() > 0)
            {
                int max = this.context.Usuarios.Max(user => user.IdUsuario);
                id = max + 1;
                return id;
            }
            else
            {
                return 1;
            }
        }
        public Ticket FindTicket(int idticket)
        {
            return this.context.Tickets.SingleOrDefault(x => x.IdTicket == idticket);
        }

        public void InsertUsuario(string nombre,string apellidos,string email,string username,string password)
        {
            int id = GetMaxIdUsuario();
           Usuario usuario = new Usuario
            {
               IdUsuario=id,
               Nombre=nombre,
               Apellidos=apellidos,
               Email=email,
               Username=username,
               Password=password
               
            };
            this.context.Usuarios.Add(usuario);
            this.context.SaveChanges();
        }
        public async Task InsertarTicketAsync(int idticket,int idusuario,DateTime fecha,string importe,string producto,string filename,string storagepath)
        {
            string urlFlowInsert =
            "https://prod-12.westeurope.logic.azure.com:443/workflows/8a7f804468b448aebf8c2b8a68ff93e3/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=hdi8mxg_PKKdndthafool2dS-8rLYsZ3HkbQ-I0gjKA";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Ticket ticket = new Ticket
                {
                    IdTicket = idticket,
                    IdUsuario = idusuario,
                    Fecha = fecha,
                    Importe = importe,
                    Producto = producto,
                    Fileanme = filename,
                    StoragePath = storagepath
                };
                string json = JsonConvert.SerializeObject(ticket);
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PostAsync(urlFlowInsert, content);
            }
        }
        public Usuario ExisteUsuario(string nombre, string password)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Nombre == nombre
                           && datos.Password == password
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return consulta.First();
            }
        }
    }
}
