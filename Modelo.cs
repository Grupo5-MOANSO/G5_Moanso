using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_Semana_02___Moanso
{
    public class Modelo
    {
        public string ModeloF { get; set; }
        public int IdModelo { get; set; }

        public override string ToString()
        {
            return ModeloF; 
        }
    }
}
