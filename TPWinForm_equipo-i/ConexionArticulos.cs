using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TPWinForm_equipo_i
{
    public class ConexionArticulos
    {
            private SqlConnection conexion;
            private SqlCommand comando;
            private SqlDataReader lector;

            public SqlDataReader Lector => lector;

            public ConexionArticulos()
            //*server=(localdb)\\MSSQLLocalDB;
            //"server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
            {
                conexion = new SqlConnection("Server=localhost,1433;Database=CATALOGO_P3_DB;User Id=sa;Password=BaseDatos#2;TrustServerCertificate=True;");
                comando = new SqlCommand();
            }

            public void SetearConsulta(string consulta)
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
            }

            public void EjecutarLectura()
            {
                comando.Connection = conexion;
                try
                {
                    conexion.Open();
                    lector = comando.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void CerrarConexion()
            {
                if (lector != null)
                    lector.Close();
                conexion.Close();
            }
        }
}
