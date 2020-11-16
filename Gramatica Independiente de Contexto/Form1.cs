using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramatica_Independiente_de_Contexto
{
    public partial class VentanaPrincipal : Form
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void buscar_Click(object sender, EventArgs e)
        {
            respuesta.Text = pertenece(cadena.Text);
            if (respuesta.Text.Equals("Si"))
            {
                respuesta.ForeColor = Color.Green;
            }
            else
            {
                respuesta.ForeColor = Color.Red;
            }
            
        }

        public String pertenece(String cadenita)
        {

            String texto = "";
            Boolean seEncuentra = false;











            if (seEncuentra==true)
            {
                texto = "Si";
            }
            else
            {
                texto = "No";
            }

            return texto;

        }
    }
}
