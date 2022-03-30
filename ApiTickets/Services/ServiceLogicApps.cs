using ApiTickets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiTickets.Services
{
    public class ServiceLogicApps
    {
        private MediaTypeWithQualityHeaderValue Header;
        public ServiceLogicApps()
        {
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        public async Task InsertarTicketAsync(Ticket ticket)
        {
            string urlFlowInsert =
            "https://prod-12.westeurope.logic.azure.com:443/workflows/8a7f804468b448aebf8c2b8a68ff93e3/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=hdi8mxg_PKKdndthafool2dS-8rLYsZ3HkbQ-I0gjKA";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(ticket);
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PostAsync(urlFlowInsert, content);
            }
        }

    }
}
