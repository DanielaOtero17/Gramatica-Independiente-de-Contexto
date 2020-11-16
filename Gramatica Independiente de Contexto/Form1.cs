using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramatica_Independiente_de_Contexto
{
    public partial class VentanaPrincipal : Form
    {
        List<Variable> variables = new List<Variable>();

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

            hacerMatriz();

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

        public void hacerMatriz()
        {
            if (File.Exists("gramatica.txt")){

                File.Delete("gramatica.txt");
            }
            StreamWriter archivo = File.CreateText("gramatica.txt");
            archivo.WriteLine(gramaticaIngresada.Text);
            archivo.Close();

            StreamReader lector = File.OpenText("gramatica.txt");

            int n = 0;

            String linea = lector.ReadLine();

            while (linea != null)
            {
                String[] array = linea.Split('-');
                Variable nueva = new Variable(array[0]);

                String[] arrayTerminales = linea.Split('>');

                String[] arrayProducciones = arrayTerminales[1].Split('|');

                for(int i =0; i < arrayProducciones.Length; i++)
                {
                    Produccion nuevaProdu = new Produccion(arrayProducciones[i]);
                    nueva.getProducciones().Add(nuevaProdu);
                }

                variables.Add(nueva);
                n++;


                linea = lector.ReadLine();
            }

            String[,] matriz = new String[n,n];
            


            lector.Close();

        }

        private void gramaticaIngresada_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
