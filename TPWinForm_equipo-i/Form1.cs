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
            cargarCombosFiltro();
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
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem != null)
            {
                // Guardamos el objeto seleccionado de la fila actual de la grilla
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                // Creamos la instancia pasándole el artículo por parámetro
                VerDetalle detalle = new VerDetalle(seleccionado);
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

        //Filtro para los desplegables
        private void cargarCombosFiltro()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                // Cargar combo de Marcas
                List<Marca> listaMarcas = negocio.listarMarcas();
                comboBox1.DataSource = listaMarcas;
                comboBox1.ValueMember = "Id";
                comboBox1.DisplayMember = "Descripcion";
                comboBox1.SelectedIndex = -1; // esto deselecciona al inicio

                // Cargar combo de Categorías
                List<Categoria> listaCategorias = negocio.listarCategorias();
                comboBox2.DataSource = listaCategorias;
                comboBox2.ValueMember = "Id";
                comboBox2.DisplayMember = "Descripcion";
                comboBox2.SelectedIndex = -1;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los filtros: " + ex.Message);
            }
        }


        //éste es el boton ir
        private void button1_Click(object sender, EventArgs e)
        {
            if (listaArticulo == null) return;

            List<Articulo> listaFiltrada = listaArticulo;

            // 1. Filtramos por texto (sólo si escribió algo)
            string textoBusqueda = textBox1.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                listaFiltrada = listaFiltrada.Where(x => x.Nombre != null && x.Nombre.ToLower().Contains(textoBusqueda)).ToList();
            }

            // 2. Filtrar por Marca (sólo si hay una marca seleccionada en el combo)
            if (comboBox1.SelectedItem != null && comboBox1.SelectedIndex != -1)
            {
                Marca marcaSeleccionada = (Marca)comboBox1.SelectedItem;
                listaFiltrada = listaFiltrada.Where(x => x.Marca != null && x.Marca.Id == marcaSeleccionada.Id).ToList();
            }

            // 3. Filtrar por Categoría (sólo si hay una categoría seleccionada en el combo)
            if (comboBox2.SelectedItem != null && comboBox2.SelectedIndex != -1)
            {
                Categoria categoriaSeleccionada = (Categoria)comboBox2.SelectedItem;
                listaFiltrada = listaFiltrada.Where(x => x.Categoria != null && x.Categoria.Id == categoriaSeleccionada.Id).ToList();
            }

            // 4. Mostramos el resultado
            dataGridView1.DataSource = listaFiltrada;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // 1. Limpiamos la caja de texto
            textBox1.Clear();

            // 2. Dejamos los combos vacíos
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            // 3. Volvemos a mostrar la lista completa en la grilla
            dataGridView1.DataSource = listaArticulo;
        }
    }
}
