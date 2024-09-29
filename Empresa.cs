using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_Semana_02___Moanso
{
    public class Empresa
    {
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public string Representante { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string RazSoc { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
