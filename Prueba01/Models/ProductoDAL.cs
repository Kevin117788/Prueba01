using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Prueba01.Models
{
    public class ProductoDAL
    {
        string Conexion = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = new SqlConnection(Conexion))
            {
                string query = @"
                    SELECT p.Id, p.Nombre, c.Nombre as Categoria, p.Descripcion, p.Precio
                    FROM Productos p
                    INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Producto()
                        {
                            ID = dr["Id"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Categoria = dr["Categoria"].ToString(),
                            Descripcion = dr["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"]).ToString("0.##")
                        });
                    }
                }
            }
            return lista;
        }

        public int ObtenerOInsertarCategoria(string nombreCategoria)
        {
            int idCategoria = 0;
            using (SqlConnection con = new SqlConnection(Conexion))
            {
                string queryBuscador = "SELECT IdCategoria FROM Categorias WHERE Nombre = @Nombre";
                SqlCommand cmdBuscar = new SqlCommand(queryBuscador, con);
                cmdBuscar.Parameters.AddWithValue("@Nombre", nombreCategoria);

                con.Open();
                object res = cmdBuscar.ExecuteScalar();
                if (res != null)
                {
                    idCategoria = Convert.ToInt32(res);
                }
                else
                {
                    string queryInsert = "INSERT INTO Categorias (Nombre) OUTPUT INSERTED.IdCategoria VALUES (@Nombre)";
                    SqlCommand cmdInsert = new SqlCommand(queryInsert, con);
                    cmdInsert.Parameters.AddWithValue("@Nombre", nombreCategoria);
                    idCategoria = (int)cmdInsert.ExecuteScalar();
                }
            }
            return idCategoria;
        }

        public void AgregarProducto(Producto prod)
        {
            int idCat = ObtenerOInsertarCategoria(prod.Categoria);

            using (SqlConnection con = new SqlConnection(Conexion))
            {
                string query = "INSERT INTO Productos (Nombre, IdCategoria, Descripcion, Precio) VALUES (@Nombre, @IdCat, @Desc, @Precio)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nombre", prod.Nombre);
                cmd.Parameters.AddWithValue("@IdCat", idCat);
                cmd.Parameters.AddWithValue("@Desc", (object)prod.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Precio", Convert.ToDecimal(prod.Precio));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarProducto(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion))
            {
                string query = "DELETE FROM Productos WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}