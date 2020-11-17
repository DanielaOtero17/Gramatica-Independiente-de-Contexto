using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramatica_Independiente_de_Contexto
{
    public class Produccion
    {
        private String cadena;

        public Produccion(String subcadena)
        {
            this.cadena = subcadena;
        }

        public String getProduccion()
        {
            return cadena;
        }

        public void setProduccion(String subcadena)
        {
            this.cadena = subcadena;
        }


        
    }
}
