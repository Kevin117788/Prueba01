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
                // Se cambió 'usuario' por 'Usuarios' en la consulta
                string query = "select * from Usuarios where Usuario=@usuario AND Contraseña=@pass AND Estado=1";

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
        public bool RegistrarUsuario(Usuario user)
        {
            using (SqlConnection con = new SqlConnection(Conexion))
            {
                string query = "INSERT INTO Usuarios (Usuario, Correo, Contraseña, TipoUsuario, Estado) VALUES (@usuario, @correo, @contraseña, @tipoUsuario, 1)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@usuario", user.UsuarioNombre);
                cmd.Parameters.AddWithValue("@correo", user.Correo);
                cmd.Parameters.AddWithValue("@contraseña", user.Contraseña);
                cmd.Parameters.AddWithValue("@tipoUsuario", user.TipoUsuario);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}