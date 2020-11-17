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
        List<Variable> variables;
        Variable inicial;

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

            agregarVariables();

            String texto = "";
            Boolean seEncuentra = inicializacion();

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

        public void agregarVariables()
        {

            try { 
            if (File.Exists("gramatica.txt")){

                File.Delete("gramatica.txt");
            }
            StreamWriter archivo = File.CreateText("gramatica.txt");
            archivo.WriteLine(gramaticaIngresada.Text);
            archivo.Close();

            StreamReader lector = File.OpenText("gramatica.txt");

            int n = 0;

            String linea = lector.ReadLine();

            variables = new List<Variable>();

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

            inicial = variables[0];
            
            lector.Close();
            }
            catch
            {
                MessageBox.Show("La gramática ingresada no cumple el formato correcto");
            }


        }

        public Boolean inicializacion()
        {

            Boolean pert = false;
            String w = cadena.Text;
            int n = w.Length;
            String[,] matriz = new String[n, n];

            Char[] elementosW = new Char[w.Length];

            

            for (int i =0; i < w.Length; i++)
            {
                elementosW[i] = w.ElementAt(i);
            }

            int j = 0;
            String nombresGeneradoras = "";

            for (int i=0; i < elementosW.Length; i++)
            {
                List<Variable> generadoras =   asignarVariablesGeneradoras(elementosW[i]);

                /*    j   0     1      2       3
                 *    i
                 *  a 0   A     A      A       S
                    a 1   A     A      B
                    a 2   A     S     
                    b 3   B     
                */

                if (generadoras.Count > 0)
                {
                    
                    for(int a =0; a<generadoras.Count; a++)
                    {
                        
                        if (a < generadoras.Count - 1)
                        {
                            nombresGeneradoras += generadoras[a].getNombre() + ",";
                        }
                        else 
                        {
                            nombresGeneradoras += generadoras[a].getNombre();
                        }

                    }

                    // Se elimina la última coma ',' de la cadena nombresGeneradoras
                    int longitud = nombresGeneradoras.Length;

                    //nombresGeneradoras.Remove(longitud - 1);

                    matriz[i, j] = nombresGeneradoras;
                }
                else
                {
                    matriz[i, j] = "";
                }

                nombresGeneradoras = "";

            }

            j++;

            int c = 1;

            for (int k = 0; k < n; k++)
            {
          
                for (j = 1; j < n; j++)
                {
                    
                    for (int i=0; i < n-c; i++)
                    {
                       
                        int p1 = i + k + 1;
                        int p2 = j - k - 1;

                        String izquierdo = matriz[i, k];
                        String derecho = matriz[p1,p2];


                        // distribucion en caso que ambas variables sean mixtas
                        if (izquierdo.Length > 1 && derecho.Length > 1)
                        {
                            Console.WriteLine("ambas son mixtas");

                            String[] ladoIzq = izquierdo.Split(',');
                            String[] ladoDer = derecho.Split(',');
                            string cadenita = "";

                            for (int v = 0; v < ladoIzq.Length; v++)
                            {
                                for (int p = 0; p < ladoDer.Length; p++)
                                {
                                    if (v < ladoIzq.Length - 1)
                                    {
                                        cadenita += ladoIzq[v] + derecho[p] + ",";
                                    }
                                    else
                                    {
                                        cadenita += ladoIzq[v] + derecho[p];
                                    }

                                }
                            }

                            String[] subcadenita = cadenita.Split(',');

                            String completa = "";

                            Boolean siProduce = false;

                            for (int r = 0; r < subcadenita.Length; r++)
                            {
                                String cambio = variableGeneradora(subcadenita[r]);

                                if (!cambio.Equals(subcadenita[r]) && cambio.Contains(inicial.getNombre()))
                                    siProduce = true;

                                if (r < subcadenita.Length - 1)
                                {
                                    completa += cambio + ",";
                                }
                                else
                                {
                                    completa += cambio;
                                }

                            }
                            if (siProduce == true)
                            {
                                matriz[i, j] = completa;
                            }
                            else
                            {
                                matriz[i, j] = "";
                            }

                            Console.WriteLine("ambas son mixtas, la cadena es: " + cadenita);
                        }
                        else if (izquierdo.Length>1)// distribucion en caso que la variable de la izquierda sea mixta
                        {
                            Console.WriteLine("La izquierda es mixta");
                            string cadenita = "";

                            String[] ladoIzquierdo = izquierdo.Split(',');

                            for (int v = 0; v < ladoIzquierdo.Length; v++)
                            {
                                if (v < ladoIzquierdo.Length - 1)
                                {
                                    cadenita += ladoIzquierdo[v] + derecho + ",";
                                }
                                else
                                {
                                    cadenita += ladoIzquierdo[v] + derecho ;
                                }
                                    
                            }
                            Console.WriteLine("La izquierda es mixta la cadena es: " + cadenita);

                            String[] subcadenita = cadenita.Split(',');

                            String completa = "";

                            Boolean siProduce = false;
                            for (int r = 0; r < subcadenita.Length; r++)
                            {
                                String cambio = variableGeneradora(subcadenita[r]);


                                if (!cambio.Equals(subcadenita[r]) || cambio.Contains(inicial.getNombre()))
                                    siProduce = true;

                                if (subcadenita[r].Equals(cambio))
                                {
                                    cambio = subcadenita[r].ElementAt(0)+"";
                                }

                                if (r < subcadenita.Length - 1)
                                    {
                                        completa += cambio + ",";
                                    }
                                    else
                                    {
                                        completa += cambio;
                                    }
                                
                            }

                            if (siProduce == true)
                            {
                                matriz[i, j] = completa;
                            }
                            else
                            {
                                matriz[i, j] = "";
                            }


                        }
                        else  if (derecho.Length > 1)// distribucion en caso que la variable de la derecha sea mixta
                        {
                            string cadenita = "";
                            Console.WriteLine("La derecha es mixta");

                            String[] ladoDerecho = derecho.Split(',');

                            for (int v = 0; v < ladoDerecho.Length; v++)
                            {
                                if (v < ladoDerecho.Length - 1)
                                {
                                    cadenita += izquierdo + ladoDerecho[v] + ",";
                                }else
                                {
                                    cadenita += izquierdo + ladoDerecho[v];
                                }
                               
                            }

                            Console.WriteLine("La derecha es mixta la cadena es: " + cadenita);

                            String[] subcadenita = cadenita.Split(',');

                            String completa = "";

                            Boolean siProduce = false;

                            for(int r = 0; r<subcadenita.Length; r++)
                            {
                                String cambio = variableGeneradora(subcadenita[r]);


                                if(!cambio.Equals(subcadenita[r]) || cambio.Contains(inicial.getNombre()))
                                    siProduce = true;

                                if (subcadenita[r].Equals(cambio))
                                {
                                    cambio = subcadenita[r].ElementAt(0) + "";
                                }
                                    if (r < subcadenita.Length - 1)
                                    {
                                        completa += cambio + ",";
                                    }
                                    else
                                    {
                                        completa += cambio;
                                    }
                                
                            }

                            if (siProduce==true )
                            {
                                matriz[i, j] = completa;
                            }
                            else
                            {
                                matriz[i, j] = "-";
                            }
                           
                        }
                        else {

                            String pre_productora = izquierdo + "" + derecho;

                            String productora = variableGeneradora(pre_productora);

                            if (!pre_productora.Equals(productora) || productora.Contains(inicial.getNombre()))
                            {
                                matriz[i, j] = productora;
                            }
                            else
                            {
                                matriz[i, j] = "";
                            }
                                
                        }

                        
                    }
                    c++;
                }

            }

            String proceso = "";
            // For para imprimir la matriz y validar
            for(int i=0; i<n; i++)
            {
                String fila = "";
                for (int x = 0; x<n; x++)
                {
                    fila += matriz[i, x] + " | ";
                }

                proceso += fila +"\n";

            }

            if(matriz[0, n - 1].Contains(inicial.getNombre())){
                pert = true;
            }
            MessageBox.Show(proceso);


            return pert;
         }

        public String variableGeneradora(String produccion)
        {
            Console.WriteLine("Producción que entra es: " + produccion);
            for (int i = 0; i < variables.Count; i++)
            {
                for (int j = 0; j < variables[i].getProducciones().Count; j++)
                {
                    
                    Produccion aux = variables[i].getProducciones()[j];

                    Console.WriteLine("entra " + produccion + " y se compara con: " + aux.getProduccion());

                    if (aux.getProduccion().Equals(produccion))
                        {

                            return variables[i].getNombre();
                        } 
                }

            }

            return produccion;

        }
        public List<Variable> asignarVariablesGeneradoras(Char charsito)
        {
            Boolean seAñade = false;
            List<Variable> generadoras = new List<Variable>();

            for (int i=0; i < variables.Count; i++)
                {
                    for(int j = 0; j < variables[i].getProducciones().Count; j++)
                    {

                        if (variables[i].getProducciones()[j].getProduccion().Length ==1)
                        {
                            Char produccionUnitaria = variables[i].getProducciones()[j].getProduccion().ElementAt(0);

                            if (charsito==produccionUnitaria)
                            {
                            
                            generadoras.Add(variables[i]);
                            }
                        }
                    }

                }
            return generadoras;

        }

        private void gramaticaIngresada_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
