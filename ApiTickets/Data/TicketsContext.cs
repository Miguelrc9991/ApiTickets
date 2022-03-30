using ApiTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTickets.Data
{
    public class TicketsContext:DbContext
    {
        public TicketsContext
(DbContextOptions<TicketsContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}
