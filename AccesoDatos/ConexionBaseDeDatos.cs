using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos
public class ConexionBaseDeDatos
{
    private readonly string cadenaConexion;
    private SqlConnection conexion;

    public ConexionBaseDeDatos(string cadenaConexion)
    {
        this.cadenaConexion = cadenaConexion;
        this.conexion = new SqlConnection(cadenaConexion);
    }

    public bool AbrirConexion()
    {
        try
        {
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }
            return true;
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            return false;
        }
    }

    public void CerrarConexion()
    {
        if (conexion.State == ConnectionState.Open)
        {
            conexion.Close();
        }
    }

    public SqlConnection ObtenerConexion()
    {
        return conexion;
    }
}