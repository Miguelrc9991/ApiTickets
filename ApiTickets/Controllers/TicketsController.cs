using ApiTickets.Models;
using ApiTickets.Repositories;
using ApiTickets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private RepositoryTickets repo;
        private ServiceLogicApps service;

        public TicketsController(RepositoryTickets repo, ServiceLogicApps service)
        {
            this.service = service;

            this.repo = repo;
        }
       
        [HttpGet]
        [Authorize]
        public ActionResult<List<Ticket>> GetTickets()
        {
            return this.repo.GetTickets();
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Ticket> FindTicket(int id)
        {
            return this.repo.FindTicket(id);
        }
        
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public  IActionResult CreateUser(Usuario usuario)
        {
          this.repo.InsertUsuario
            (usuario.Nombre,usuario.Apellidos,usuario.Email,usuario.Username,usuario.Password);
            return Ok();
        }
        [HttpPost]
        [Authorize]
        [Route("[action]")]

        public async Task<IActionResult> NuevoTicket(Ticket ticket)
        {
            await this.repo.InsertarTicketAsync(ticket.IdTicket,ticket.IdUsuario,ticket.Fecha,ticket.Importe,ticket.Producto,ticket.Fileanme,ticket.StoragePath);
            return Ok();
        }
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public ActionResult<Usuario> GetPerfilUsuario()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();

            string jsonUsuario = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return usuario;
        }


    }
}
