using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_Semana_02___Moanso
{
    public partial class Orden_Produccion : Form
    {
        private string connectionString;
        Conexion cn = new Conexion();
        public Orden_Produccion()
        {
            InitializeComponent();
            string nombre_servidor = Dns.GetHostName();
            connectionString = $"Data Source={nombre_servidor}\\;Initial Catalog=Laboratorio_2_MOANSO;Integrated Security=True;Encrypt=False";

            cn.Open();
            CargarEmpresas();
            CargarMarcas();
            CargarModelos();
        }

        private void CargarModelos()
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                string query = "SELECT Modelo, Id_Modelo FROM ModeloFabricacion";
                SqlCommand cmd = new SqlCommand(query, cn);

                try
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBoxModelos.Items.Add(new Modelo
                        {
                            ModeloF = reader["Modelo"].ToString(),
                            IdModelo = Convert.ToInt32(reader["Id_Modelo"])
                        });
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar modelos: " + ex.Message);
                }
            }
        }

        private void comboBoxModelos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void CargarEstructuraMateriales(int modeloId)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                treeViewEstructura.Nodes.Clear();

                string query = @"
                    SELECT a.Nombre AS Area, s.Nombre AS SupervisorNombre, s.Apellido AS SupervisorApellido,
                    m.Nombre AS Material, em.Cantidad
                    FROM EstructuraMateriales em
                    JOIN Areas a ON em.Area = a.Nombre
                    JOIN Supervisores s ON em.SupervisorId = s.Id_supervisores
                    JOIN Materiales m ON em.Material = m.Nombre
                    WHERE em.ModeloId = @ModeloId
                    ORDER BY a.Nombre"; 

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@ModeloId", modeloId);

                try
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    TreeNode rootNode = new TreeNode("Modelo " + modeloId);
                    treeViewEstructura.Nodes.Add(rootNode);

                    while (reader.Read())
                    {
                        string area = reader["Area"].ToString();
                        string supervisor = reader["SupervisorNombre"] + " " + reader["SupervisorApellido"];
                        string material = reader["Material"].ToString();
                        string cantidad = reader["Cantidad"].ToString();

                        TreeNode areaNode = rootNode.Nodes.Cast<TreeNode>()
                                              .FirstOrDefault(node => node.Text == "1. Área: " + area);

                        if (areaNode == null)
                        {
                            areaNode = new TreeNode("1. Área: " + area);
                            rootNode.Nodes.Add(areaNode);
                        }

                        TreeNode supervisorNode = new TreeNode("1.1 Supervisor: " + supervisor);
                        areaNode.Nodes.Add(supervisorNode);

                        TreeNode materialNode = new TreeNode("1.2 Material: " + material);
                        areaNode.Nodes.Add(materialNode);

                        TreeNode cantidadNode = new TreeNode("1.3 Cantidad: " + cantidad);
                        areaNode.Nodes.Add(cantidadNode);
                    }

                    treeViewEstructura.ExpandAll();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar estructura de materiales: " + ex.Message);
                }
            }
        }




        public void CargarEmpresas()
        {
            string nombre_servidor = Dns.GetHostName();
            using (SqlConnection cn = new SqlConnection("Data Source=" + nombre_servidor + "\\;Initial Catalog=Laboratorio_2_MOANSO;Integrated Security=True;Encrypt=False"))
            {
                string query = "SELECT Nombre, Ruc, Nom_representante, Direccion, Telefono, Raz_soc FROM Cliente";
                SqlCommand cmd = new SqlCommand(query, cn);

                try
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    comboBoxEmpresas.Items.Clear();

                    while (reader.Read())
                    {
                        var empresa = new Empresa
                        {
                            Nombre = reader["Nombre"].ToString(),
                            Ruc = reader["Ruc"].ToString(),
                            Representante = reader["Nom_representante"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            RazSoc = reader["Raz_soc"].ToString()
                        };

                        comboBoxEmpresas.Items.Add(empresa);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar empresas: " + ex.Message);
                }
            }
        }


        

        public void CargarMarcas()
        {
            string nombre_servidor = Dns.GetHostName();
            using (SqlConnection cn = new SqlConnection("Data Source=" + nombre_servidor + "\\SQLEXPRESS\\;Initial Catalog=Laboratorio_2_MOANSO;Integrated Security=True;Encrypt=False"))
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente(txtRuc.Text, txtRazSoc.Text, txtEmpresa.Text, txtRepresentante.Text, txtTelefono.Text, txtDireccion.Text);
            if (cliente.InsertarCliente())
            {
                MessageBox.Show("Cliente registrado con éxito.");
            }
            else
            {
                MessageBox.Show("Error al registrar el cliente.");
            }
            CargarEmpresas();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxModelos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var selectedItem = comboBoxModelos.SelectedItem as Modelo;
            if (selectedItem != null)
            {
                int modeloId = selectedItem.IdModelo;
                CargarEstructuraMateriales(modeloId);
            }
        }

        private void ActualizarListBoxComentarios()
        {
            listBoxComentarios.Items.Clear(); 

            if (radioButtonInter.Checked)
            {
                listBoxComentarios.Items.Add("- Fabricación de la carrocería para Bus Interprovincial");
            }
            else if (radioButtonTuristico.Checked)
            {
                listBoxComentarios.Items.Add("- Fabricación de la carrocería para Bus Turístico");
            }
            else if (radioButtonUrbano.Checked)
            {
                listBoxComentarios.Items.Add("- Fabricación de la carrocería para Bus Urbano");
            }

            if (checkBox180.Checked)
            {
                listBoxComentarios.Items.Add(" - Se requiere que los asientos sean 180° / Bus cama");
            }
            if (checkBoxExtra.Checked)
            {
                listBoxComentarios.Items.Add(" - Se requiere que el bus tenga 5 asientos más");
            }
            if (checkBoxMicro.Checked)
            {
                listBoxComentarios.Items.Add(" - Se requiere que cuenta con micrófono y altavos en la cabina y en el interior");
            }
            if (checkBoxCopiloto.Checked)
            {
                listBoxComentarios.Items.Add(" - Se requiere que tenga asiento de copiloto");
            }
            if (checkBoxNeblineros.Checked)
            {
                listBoxComentarios.Items.Add(" - Se requiere que tenga faros/barra anti niebla");
            }
            if (checkBoxMonitor.Checked)
            {
                listBoxComentarios.Items.Add(" - Se requiere que cuente con monitores para la comodidad del pasajero");
            }
        }

        private void Orden_Produccion_Load(object sender, EventArgs e)
        {
            radioButtonInter.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
            radioButtonTuristico.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
            radioButtonUrbano.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();

            checkBox180.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
            checkBoxExtra.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
            checkBoxMicro.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
            checkBoxCopiloto.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
            checkBoxNeblineros.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
            checkBoxMonitor.CheckedChanged += (s, ev) => ActualizarListBoxComentarios();
        }

        private void ConfigurarDataGridView()
        {
            dataGridViewOP.Columns.Add("Empresa", "Nombre de Empresa");
            dataGridViewOP.Columns.Add("RUC", "RUC");
            dataGridViewOP.Columns.Add("MarcaChasis", "Marca de Chasis");
            dataGridViewOP.Columns.Add("ModeloFabricacion", "Modelo de Fabricación");
            dataGridViewOP.Columns.Add("TipoServicio", "Tipo de Servicio");
            dataGridViewOP.Columns.Add("Adicionales", "Adicionales");
        }

        private void AgregarFilasDataGridView()
        {
            dataGridViewOP.Rows.Clear();

            string nombreEmpresa = txtEmpresa.Text;
            string ruc = txtRuc.Text;
            string marcaChasis = comboBoxMarcas.SelectedItem?.ToString() ?? "";
            string tipoServicio = radioButtonInter.Checked ? "Interprovincial" :
                                   radioButtonTuristico.Checked ? "Turístico" :
                                   radioButtonUrbano.Checked ? "Urbano" : "";

            foreach (string comentario in listBoxComentarios.Items.Cast<string>())
            {
                dataGridViewOP.Rows.Add(nombreEmpresa, ruc, marcaChasis, tipoServicio, comentario);
            }
        }

        private void InicializarDataGridView()
        {
            ConfigurarDataGridView();
        }

        private void AgregarComentario(string adicional)
        {
            listBoxComentarios.Items.Add(adicional);
        }

        private void MostrarComentariosEnDataGridView()
        {
            dataGridViewOP.Rows.Clear();

            string nombreEmpresa = txtEmpresa.Text;
            string ruc = txtRuc.Text;
            string marcaChasis = comboBoxMarcas.SelectedItem?.ToString() ?? "";
            string tipoServicio = radioButtonInter.Checked ? "Interprovincial" :
                                   radioButtonTuristico.Checked ? "Turístico" :
                                   radioButtonUrbano.Checked ? "Urbano" : "";

            string adicionales = string.Join(Environment.NewLine, listBoxComentarios.Items.Cast<string>());

            dataGridViewOP.Rows.Add(nombreEmpresa, ruc, marcaChasis, tipoServicio, adicionales);
        }



        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            AgregarFilasDataGridView();
            MostrarComentariosEnDataGridView();
        }

        

        private void comboBoxEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedEmpresa = comboBoxEmpresas.SelectedItem as Empresa;
            if (selectedEmpresa != null)
            {
                txtEmpresa.Text = selectedEmpresa.Nombre;
                txtRuc.Text = selectedEmpresa.Ruc;
                txtRepresentante.Text = selectedEmpresa.Representante;
                txtDireccion.Text = selectedEmpresa.Direccion;
                txtTelefono.Text = selectedEmpresa.Telefono;
                txtRazSoc.Text = selectedEmpresa.RazSoc;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            txtEmpresa.Clear();
            txtRuc.Clear();
            txtRepresentante.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtRazSoc.Clear();
            dataGridViewOP.Rows.Clear();
            listBoxComentarios.Items.Clear();
            treeViewEstructura.Nodes.Clear();
            comboBoxEmpresas.Items.Clear();
            comboBoxMarcas.Items.Clear();
            comboBoxModelos.Items.Clear();
            comboBoxEmpresas.Text = "";
            comboBoxMarcas.Text = "";
            comboBoxModelos.Text = "";

            radioButtonInter.Checked = false;
            radioButtonTuristico.Checked = false;
            radioButtonUrbano.Checked = false;
            checkBox180.Checked = false;
            checkBoxCopiloto.Checked = false;
            checkBoxExtra.Checked = false;
            checkBoxMicro.Checked = false;
            checkBoxMonitor.Checked = false;
            checkBoxNeblineros.Checked = false;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Form1 F1 = new Form1();
            F1.Show();
            this.Close();
        }
    }
}
