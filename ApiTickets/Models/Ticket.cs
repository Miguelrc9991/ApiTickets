using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTickets.Models
{
    [Table("TICKETS")]
    public class Ticket
    {
        [Key]
        [Column("IDTICKET")]
        public int IdTicket { get; set; }
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
        [Column("IMPORTE")]
        public string Importe { get; set; }
        [Column("Producto")]
        public string Producto { get; set; }
        [Column("Filename")]
        public string Fileanme { get; set; }
        [Column("StoragePath")]
        public string StoragePath { get; set; }
    }
}
