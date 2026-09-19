using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TPWinForm_equipo_i
{
    public partial class VerDetalle : Form
    {
        private Articulo articulo;

        // Constructor base
        public VerDetalle()
        {
            InitializeComponent();
        }

        // Constructor con parámetro: llama a :this() para inicializar controles
        public VerDetalle(Articulo articulo) : this()
        {
            this.articulo = articulo;
            this.btnCerrar.Click += new System.EventHandler(btnCerrar_Click);

            // Cargar datos directamente aquí para no depender exclusivamente del evento Load
            cargarDatos();
        }

        private void VerDetalle_Load(object sender, EventArgs e)
        {
            // Se ejecuta al abrir la ventana si está enlazado en el Diseñador
            cargarDatos();
        }

        private void cargarDatos()
        {
            if (articulo != null)
            {
                Text = "Detalle de " + articulo.Nombre;

                // Asegúrate de que los nombres de los Labels coincidan con la ventana de Propiedades
                // Cambiamos "Codigo.Text" por "lblCodigo.Text"
                if (lblCodigo != null) lblCodigo.Text = "Código: " + articulo.Codigo;
                if (lblNombre != null) lblNombre.Text = "Nombre: " + articulo.Nombre;
                if (txtDescripcion != null) txtDescripcion.Text = articulo.Descripcion;
                if (lblPrecio != null) lblPrecio.Text = "Precio: $" + articulo.Precio.ToString("0.00");

                if (lblMarca != null)
                    lblMarca.Text = "Marca: " + (articulo.Marca != null ? articulo.Marca.Descripcion : "Sin Marca");

                if (lblCategoria != null)
                    lblCategoria.Text = "Categoría: " + (articulo.Categoria != null ? articulo.Categoria.Descripcion : "Sin Categoría");

                // Cargar imagen
                cargarImagenActual();
            }
        }

        private void cargarImagenActual()
        {
            try
            {
                // Verifica la lista de imágenes primero
                if (articulo.Imagenes != null && articulo.Imagenes.Count > 0 && !string.IsNullOrEmpty(articulo.Imagenes[0].ImageUrl))
                {
                    pbxImagen.Load(articulo.Imagenes[0].ImageUrl);
                }
                else
                {
                    pbxImagen.Load("https://blocks.astratic.com/img/general-img-landscape.png");
                }
            }
            catch (Exception)
            {
                // Imagen de respaldo si la URL está rota o tira error HTTP
                pbxImagen.Load("https://blocks.astratic.com/img/general-img-landscape.png");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VerDetalle_Load_1(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {

        }
    }
}