using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_i
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            ConexionArticulos datos = new ConexionArticulos();

            try
            {
                // Realizamos el JOIN para obtener el nombre/descripción de Marcas y Categorías
                datos.SetearConsulta(@"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, 
                                             M.Id AS IdMarca, M.Descripcion AS Marca, 
                                             C.Id AS IdCategoria, C.Descripcion AS Categoria 
                                      FROM ARTICULOS A 
                                      LEFT JOIN MARCAS M ON A.IdMarca = M.Id 
                                      LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id");

                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private List<Imagen> cargarImagenes(int idArticulo)
        {
            List<Imagen> lista = new List<Imagen>();
            ConexionArticulos datos = new ConexionArticulos();

            try
            {
                datos.SetearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @idArticulo");
                // Puedes agregar un parámetro o concatenar idArticulo para filtrar
                datos.SetearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = " + idArticulo);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen img = new Imagen();
                    img.Id = (int)datos.Lector["Id"];
                    img.IdArticulo = (int)datos.Lector["IdArticulo"];
                    img.ImageUrl = (string)datos.Lector["ImagenUrl"];
                    lista.Add(img);
                }

                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
