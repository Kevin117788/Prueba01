using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba01.Models
{
    public class UsuarioDAL
    {
        string Conexion = ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

        public Usuario ValidarUsuario(string usuario, string Contraseña)
        {
            Usuario user = null;
            using (SqlConnection con = new SqlConnection(Conexion))
            {
                string query = "select * from usuario where Usuario=@usuario AND Contraseña=@pass AND Estado=1";

               SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@pass", Contraseña);
                
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    user = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        UsuarioNombre = dr["Usuario"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        TipoUsuario = Convert.ToInt32(dr["TipoUsuario"])
                    };
                }
            }
            return user;
        }
    }
}