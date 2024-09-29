using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_Semana_02___Moanso
{
    internal class Cliente
    {
        public string Ruc { get; set; }
        public string RazSoc { get; set; }
        public string Nombre { get; set; }
        public string NomRepresentante { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public Cliente(string ruc, string razSoc, string nombre, string nomRepresentante, string telefono, string direccion)
        {
            Ruc = ruc;
            RazSoc = razSoc;
            Nombre = nombre;
            NomRepresentante = nomRepresentante;
            Telefono = telefono;
            Direccion = direccion;
        }

        public bool InsertarCliente()
        {
            try
            {
                string nombre_servidor = Dns.GetHostName();
                using (SqlConnection cn = new SqlConnection("Data Source=" + nombre_servidor + "\\;Initial Catalog=Laboratorio_2_MOANSO;Integrated Security=True;Encrypt=False"))
                {
                    {
                        string query = "INSERT INTO Cliente (Ruc, Raz_soc, Nombre, Nom_representante, Telefono, Direccion) " +
                                       "VALUES (@Ruc, @RazSoc, @Nombre, @NomRepresentante, @Telefono, @Direccion)";

                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@Ruc", Ruc);
                        cmd.Parameters.AddWithValue("@RazSoc", RazSoc);
                        cmd.Parameters.AddWithValue("@Nombre", Nombre);
                        cmd.Parameters.AddWithValue("@NomRepresentante", NomRepresentante);
                        cmd.Parameters.AddWithValue("@Telefono", Telefono);
                        cmd.Parameters.AddWithValue("@Direccion", Direccion);

                        cn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones o errores aquí
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}
