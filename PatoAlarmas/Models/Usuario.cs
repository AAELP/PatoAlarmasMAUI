using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatoAlarmas.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public List<Vehiculo> Vehiculos { get; set; }

        public Usuario()
        {
            Vehiculos = new List<Vehiculo>();
        }
    }
}
