using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFNegocio.Models;
using CapaDatos;
using System.Drawing.Printing;
using System.Web.UI;
using WCFNegocio.Utils;

namespace WCFNegocio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UsuarioServices" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UsuarioServices.svc o UsuarioServices.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UsuarioServices : IUsuarioServices        
    {
        //ConexionBaseDeDatos conexion = null; 
        string connectionString = string.Empty;
        public UsuarioServices()
        {
            //conexion = new ConexionBaseDeDatos();
            //connectionString = conexion.ObtenerConexion().ConnectionString;
            connectionString = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;
        }
        public bool ActualizarUsuario(Usuario usuario)
        {
            bool result = MergeUsuario(3, usuario);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> Codigo del Usuario</param>
        /// <returns></returns>
        public Usuario ConsultarUsuario(int id)
        {
            Usuario usuario = new Usuario();              

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SPCrudUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@opcion", 1));//Opcion 1 = Consultar
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.Parameters.Add(new SqlParameter("@FechaNacimiento", DateTime.Now));
                    command.Parameters.Add(new SqlParameter("@email", ""));                    
                    command.Parameters.Add(new SqlParameter("@Sexo", ""));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuario.Id = (int)reader["id"];
                            usuario.Nombre = reader["Nombre"].ToString();
                            usuario.FechaNacimiento = DateTime.Parse(reader["fechaNacimiento"].ToString());
                            usuario.Sexo = reader["sexo"].ToString();
                            usuario.Email = reader["email"].ToString();
                        }
                    }
                }
            }
            return usuario;
        }

        /// <summary>
        /// Elimina Usuario opcion 4 por defecto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EliminarUsuario(int id)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            bool result = MergeUsuario(4, usuario);

            return result;
        }
        /// <summary>
        /// Guardar Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool GuardarUsuario(Usuario usuario)
        {
            bool result = MergeUsuario(2, usuario);

            return result;
        }
        /// <summary>
        ///  Campo nombre es opcional
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<Usuario> ListartUsuarios(string nombre)
        {
            //int itemsPorPagina = 5;

            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, nombre, email, fechaNacimiento, sexo FROM dbo.Usuario";
                if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrWhiteSpace(nombre))
                {
                    query = query + " WHERE nombre LIKE '%" + nombre.ToLower() + "%'";
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = (int)reader["id"],
                            Nombre = reader["Nombre"].ToString(),
                            Email = reader["email"].ToString(),
                            FechaNacimiento = DateTime.Parse(reader["fechaNacimiento"].ToString()),
                            FechaFormateada = DateTime.Parse(reader["fechaNacimiento"].ToString()).ToString("yyyy-MM-dd"),
                            Sexo = reader["sexo"].ToString()//[0]
                        });
                    }
                }
            }                  

            return usuarios;
        }

        /// <summary>
        /// Metodo que elimina, guarda y/o actualiza Registros de Usuario
        /// </summary>
        /// <param name="opcion"> 4 Eliminar</param>
        /// <returns></returns>
        public bool MergeUsuario(int opcion, Usuario usuario = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SPCrudUsuario", connection);
            command.CommandType = CommandType.StoredProcedure;

            //command.Parameters.Add(new SqlParameter("@opcion", 0));            

            if (opcion == 4) // Eliminar
            {
                command.Parameters.Add(new SqlParameter("@opcion", opcion));
            }
            if (opcion == 2) // Guardar
            {
                command.Parameters.Add(new SqlParameter("@Passworld", Encriptar.EncriptarPassword(usuario.Passworld)));
            }

            //bool validaUsuario = Validaciones(usuario);

            command.Parameters.Add(new SqlParameter("@Id", usuario.Id));
            command.Parameters.Add(new SqlParameter("@Nombre", usuario.Nombre));
            command.Parameters.Add(new SqlParameter("@fechaNacimiento", usuario.FechaNacimiento));
            command.Parameters.Add(new SqlParameter("@email", usuario.Email));
            command.Parameters.Add(new SqlParameter("@sexo", usuario.Sexo));
            // Agregar otros parámetros
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }


        public bool Validaciones(Usuario usuario)
        {
            var validationContext = new ValidationContext(usuario, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(usuario, validationContext, validationResults, validateAllProperties: true))
            {
                return true;
            }
            else
            {
                // La validación ha fallado; maneja los errores.
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
                return false;
            }
        }
    }
}
