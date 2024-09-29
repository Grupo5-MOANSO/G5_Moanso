using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_Semana_02___Moanso
{
    public partial class Form1 : Form
    {
        Conexion cn = new Conexion();
        public Form1()
        {
            InitializeComponent();
            cn.Open();
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            string contrasena = txtContraseña.Text;

            Usuarios usuario = new Usuarios(codigo, null, null, contrasena);

            usuario = usuario.ValidarUsuario(codigo, contrasena);

            if (usuario != null)
            {
                MessageBox.Show($"Bienvenido, {usuario.Nombre} {usuario.Apellido}");
                Orden_Produccion OP = new Orden_Produccion();
                OP.Show();
                this.Hide();
               
            }
            else
            {
                MessageBox.Show("Código o contraseña incorrectos");
                txtCodigo.Clear();
                txtContraseña.Clear();
            }
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
            cn.Close();
        }
    }
}
