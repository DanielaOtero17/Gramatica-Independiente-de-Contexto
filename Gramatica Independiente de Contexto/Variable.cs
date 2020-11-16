using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramatica_Independiente_de_Contexto
{
    class Variable
    {

        String nombre;
        List<Produccion> producciones;

        public Variable(String nombre)
        {
            this.nombre = nombre;
            producciones = new List<Produccion>();
        }

        public List<Produccion> getProducciones()
        {
            return producciones;
        }

        public void setProducciones(List<Produccion> producciones)
        {
            this.producciones = producciones;
        }

        public String getNombre()
        {
            return nombre;
        }

        public void setNombre(String nombre)
        {
            this.nombre = nombre;
        }
    }
}
