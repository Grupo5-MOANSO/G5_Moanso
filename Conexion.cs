using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.Configuration;


namespace Laboratorio_Semana_02___Moanso
{
    internal class Conexion
    {
        SqlConnection cn;

        public void Open() 
        {
            try
            {
                string nombre_servidor = Dns.GetHostName();

                cn = new SqlConnection("Data Source= "+nombre_servidor+ "\\SQLEXPRESS;Initial Catalog=Laboratorio_2_MOANSO;Integrated Security=True;Encrypt=False");

                cn.Open();

                MessageBox.Show("Servidor " + nombre_servidor + " abierto");
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Servidor no existe en el contexto actual");
            }
            
        }

        public void Close()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    string nombre_servidor = Dns.GetHostName();
                    cn.Close();
                    MessageBox.Show("Conexión al servidor " + nombre_servidor + " cerrada exitosamente");
                }
                else
                {
                    MessageBox.Show("La conexión ya esta cerrada o no existe.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar cerrar la conexión: " + ex.Message);
            }
        }

        public SqlConnection GetConnection()
        {
            return cn;
        }

    }
}

//
Proyecto Git
//
PRUEBA_GRUPO5

//MOANS SEMANA 8
//PRUeb MODIFICACION
77 MODIFICADO 12PM
