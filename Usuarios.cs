using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_Semana_02___Moanso
{
    internal class Usuarios
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasena { get; set; }

        public Usuarios(string codigo, string nombre, string apellido, string contrasena)
        {
            Codigo = codigo;
            Nombre = nombre;
            Apellido = apellido;
            Contrasena = contrasena;
        }

        public Usuarios ValidarUsuario(string codigo, string contrasena)
        {
            string nombre_servidor = Dns.GetHostName();
            using (SqlConnection cn = new SqlConnection("Data Source= " + nombre_servidor + "\\SQLEXPRESS;Initial Catalog=Laboratorio_2_MOANSO;Integrated Security=True;Encrypt=False"))
            {
                string query = "SELECT nombre, apellido FROM Usuarios WHERE codigo = @codigo AND contraseña = @contrasena";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string nombre = reader["nombre"].ToString();
                    string apellido = reader["apellido"].ToString();
                    return new Usuarios(codigo, nombre, apellido, contrasena);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
