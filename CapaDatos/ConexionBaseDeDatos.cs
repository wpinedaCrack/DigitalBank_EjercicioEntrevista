using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ConexionBaseDeDatos
    {
        private string BD;
        private string Usuario;
        private string Servidor;
        private string Clave;
        private bool Seguridad;
        private static ConexionBaseDeDatos conn;

        public ConexionBaseDeDatos()
        {
            BD = "DigitalBank";
            Usuario = "SA";
            Servidor = ".";
            Clave = "Samupi7185467*";
            Seguridad = true;
        }       

        public SqlConnection ObtenerConexion()
        {
            SqlConnection cadena = new SqlConnection();

            try
            {
                if (Seguridad)
                {
                    cadena.ConnectionString = string.Format("Data Source =.; Initial Catalog = {0}; User Id = {1}; Password = {2};", BD, Usuario, Clave);
                }               
            }
            catch (Exception x)
            {
                throw x;
            }

            return cadena;
        }

        public static ConexionBaseDeDatos CrearInstancia()
        {
            if (conn == null)
            {
                conn = new ConexionBaseDeDatos();
            }
            return conn;
        }
    }
}
