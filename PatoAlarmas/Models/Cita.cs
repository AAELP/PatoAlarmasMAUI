using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatoAlarmas.Models
{
    public class Cita
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public string VehiculoId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
