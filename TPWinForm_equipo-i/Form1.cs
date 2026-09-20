using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_i
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                comboBox1.DataSource = negocio.listarMarcas();
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "Descripcion";
                comboBox1.SelectedIndex = -1;

                comboBox2.DataSource = negocio.listarCategorias();
                comboBox2.ValueMember = "Id";
                comboBox2.DisplayMember = "Descripcion";
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dataGridView1.DataSource = listaArticulo;

                if (dataGridView1.Columns["Id"] != null)
                    dataGridView1.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }

        private void cargarImagen(List<Imagen> imagenes)
        {
            try
            {
                if (imagenes != null && imagenes.Count > 0 && !string.IsNullOrEmpty(imagenes[0].ImageUrl))
                {
                    pictureBox1.Load(imagenes[0].ImageUrl);
                }
                else
                {
                    pictureBox1.Load("https://blocks.astratic.com/img/general-img-landscape.png");
                }
            }
            catch (Exception)
            {
                pictureBox1.Load("https://blocks.astratic.com/img/general-img-landscape.png");
            }
        }

        private void aGREGARToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo alta = new FrmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.Imagenes);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                VerDetalle detalle = new VerDetalle(seleccionado);
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un artículo para ver su detalle.");
            }
        }

        // Espacio libre para que tu compañero agregue Modificar
        private void mODIFICARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Espacio reservado para Modificar
        }

        private void eLIMINARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                DialogResult respuesta = MessageBox.Show("¿Estás seguro de eliminar a " + seleccionado.Nombre + "?", "Eliminar Artículo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.Eliminar(seleccionado.Id);

                    MessageBox.Show("Artículo eliminado correctamente.");
                    cargar();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un artículo para eliminar.");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            if (comboBox1 != null) comboBox1.SelectedIndex = -1;
            if (comboBox2 != null) comboBox2.SelectedIndex = -1;
            cargar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.listar();

            try
            {
                string textoBusqueda = textBox1.Text.Trim();
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    lista = lista.FindAll(x =>
                        (x.Nombre != null && x.Nombre.ToUpper().Contains(textoBusqueda.ToUpper())) ||
                        (x.Codigo != null && x.Codigo.ToUpper().Contains(textoBusqueda.ToUpper()))
                    );
                }

                if (comboBox1.SelectedIndex != -1 && comboBox1.SelectedItem != null)
                {
                    Marca marca = (Marca)comboBox1.SelectedItem;
                    lista = lista.FindAll(x => x.Marca != null && x.Marca.Id == marca.Id);
                }

                if (comboBox2.SelectedIndex != -1 && comboBox2.SelectedItem != null)
                {
                    Categoria categoria = (Categoria)comboBox2.SelectedItem;
                    lista = lista.FindAll(x => x.Categoria != null && x.Categoria.Id == categoria.Id);
                }

                dataGridView1.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void eLIMINARToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                DialogResult respuesta = MessageBox.Show("¿Estás seguro de eliminar a " + seleccionado.Nombre + "?", "Eliminar Artículo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.Eliminar(seleccionado.Id);

                    MessageBox.Show("Artículo eliminado correctamente.");
                    cargar();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un artículo para eliminar.");
            }
        }
    }
}
