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
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dataGridView1.DataSource = listaArticulo;

                // Opcional: Ocultar columnas que no quieras mostrar directo en la grilla
                dataGridView1.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }

       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.Imagenes);
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
                    // Imagen por defecto si no tiene URL
                    pictureBox1.Load("https://blocks.astratic.com/img/general-img-landscape.png");
                }
            }
            catch (Exception)
            {
                // Imagen por defecto si falla la carga desde la Web
                pictureBox1.Load("https://blocks.astratic.com/img/general-img-landscape.png");
            }
        }

           

        private void aGREGARToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmAltaArticulo alta = new FrmAltaArticulo();

            alta.ShowDialog();

            cargar(); // Recargar grilla al cerrar la ventana de alta
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Verificamos que haya una fila seleccionada
            if (dataGridView1.CurrentRow != null)
            {
                // Convertimos el elemento seleccionado en la fila a un objeto Articulo
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                // Llamamos a la función auxiliar para cargar su imagen
                cargarImagen(seleccionado.Imagenes);
            }
        }

        // Botón "Ver Detalle"
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Obtenemos el artículo seleccionado de la grilla
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                // Abrimos la ventana de detalle
                VerDetalle detalle = new VerDetalle();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un artículo para ver su detalle.");
            }
        }

        /*private void mODIFICARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Asumiendo que tu grilla guarda objetos de tipo 'Articulo' en su DataSource
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                // Abrimos el formulario de alta PASÁNDOLE el artículo por constructor (para modificar)
                FrmAltaArticulo modificar = new FrmAltaArticulo(seleccionado);
                modificar.ShowDialog();

                // Recargar grilla después de modificar (descomentar cuando tengas el método de carga)
                // cargarGrilla();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un artículo para modificar.");
            }
        }


        private void eLIMINARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Pedimos confirmación antes de borrar
                DialogResult respuesta = MessageBox.Show("¿Estás seguro de eliminar este artículo?", "Eliminar Artículo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                    // Aquí llamarías a tu negocio/datos para borrarlo físicamente o lógicamente:
                    // ArticuloNegocio negocio = new ArticuloNegocio();
                    // negocio.Eliminar(seleccionado.Id);

                    // Recargar grilla
                    // cargarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un artículo para eliminar.");
            }
        }*/
    }
}
