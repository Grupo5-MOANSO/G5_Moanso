using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Laboratorio_Semana_02___Moanso
{
    internal class Producción
    {
        private SqlConnection cn;

        public Producción(SqlConnection conexion)
        {
            cn = conexion;
        }

        public void CargarEmpresas(ComboBox comboBoxEmpresas)
        {
            string query = "SELECT Nombre FROM Cliente";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                if (cn.State != System.Data.ConnectionState.Open)
                {
                    cn.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Agrega cada nombre de empresa al ComboBox
                    comboBoxEmpresas.Items.Add(reader["Nombre"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empresas: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public void CargarMarcas(ComboBox comboBoxMarcas)
        {
            string query = "SELECT Nombre FROM MarcaChasis";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                if (cn.State != System.Data.ConnectionState.Open)
                {
                    cn.Open();
                }

                SqlDataReader reader = cmd.ExecuteReader();

                comboBoxMarcas.Items.Clear();

                while (reader.Read())
                {
                    comboBoxMarcas.Items.Add(reader["Nombre"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar marcas: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
