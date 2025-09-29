using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAOUsuarios
    {
        private string cadena = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ruta))
                {
                    string Query = "select * from usuarios";

                    SqlCommand cmd = new SqlCommand(Query, cn);
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                    NombreUsuario = dr["nombre_usuario"].ToString(),
                                    EmailUsuario = dr["email_usuario"].ToString(),
                                    ContrasenaUsuario = dr["contraseña_usuario"].ToString(),
                                    RolUsuario = Convert.ToBoolean(dr["admin_Usuario"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Usuario>();
            }

            return lista;
        }

        public int Registrar(Usuario Usu, out string Mensaje)
        {
            int idAutogenerado = 0;
            Mensaje = string.Empty;
            try
            {

                using (SqlConnection cn = new SqlConnection(Conexion.ruta))
                {
                    SqlCommand cmd = new SqlCommand("sp_AgregarUsuario", cn);
                    cmd.Parameters.AddWithValue("Nombre", Usu.NombreUsuario);
                    cmd.Parameters.AddWithValue("Correo", Usu.EmailUsuario);
                    cmd.Parameters.AddWithValue("Clave", Usu.ContrasenaUsuario);
                    cmd.Parameters.AddWithValue("Rol", Usu.RolUsuario);
                    cmd.Parameters.AddWithValue("Estado", Usu.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    idAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                Mensaje = ex.Message;
            }

            return idAutogenerado;
        }

        public bool Editar(Usuario Usu, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ruta))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", cn);
                    cmd.Parameters.AddWithValue("ID", Usu.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombre", Usu.NombreUsuario);
                    cmd.Parameters.AddWithValue("Correo", Usu.EmailUsuario);
                    cmd.Parameters.AddWithValue("Estado", Usu.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }

            return Resultado;

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ruta))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1)  from usuarios where id_usuario = @id", cn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    Resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }

            return Resultado;
        }

        public bool CambiarClave(int idsuario, string nuevaclave, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ruta))
                {

                    SqlCommand cmd = new SqlCommand("update usuarios set contraseña_usuario = @nuevaclave where id_usuario = @id", cn);
                    cmd.Parameters.AddWithValue("@id", idsuario);
                    cmd.Parameters.AddWithValue("@nuevaclave", nuevaclave);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    Resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }

            return Resultado;
        }

        public bool RestablecerClave(int idsuario, string clave, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ruta))
                {

                    SqlCommand cmd = new SqlCommand("update usuarios set contraseña_usuario = @clave, restablecer_usuario = 1 where id_usuario = @id", cn);
                    cmd.Parameters.AddWithValue("@id", idsuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    Resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }

            return Resultado;
        }

        //dani
        public Usuario ObtenerPorCorreo(string correo)
        {
            Usuario usuario = null;
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string query = "SELECT * FROM Usuarios WHERE email_usuario = @correo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@correo", correo);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    usuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        NombreUsuario = dr["NombreUsuario"].ToString(),
                        EmailUsuario = dr["EmailUsuario"].ToString(),
                        ContrasenaUsuario = dr["ContrasenaUsuario"].ToString(),
                        RolUsuario = Convert.ToBoolean(dr["RolUsuario"]),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
                }
            }
            return usuario;
        }

        public bool GuardarCodigoRecuperacion(string correo, string codigo)
        {
            bool Resultado = false;
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string query = @"UPDATE Usuarios 
                             SET codigo_recuperacion = @codigo, 
                             fecha_codigo = GETDATE() 
                             WHERE email_usuario = @correo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@correo", correo);

                conn.Open();
                Resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                return Resultado;
            }
        }

        public int VerificarCodigo(string correo, string codigo)
        {
            using (SqlConnection conn = new SqlConnection(cadena))
            {
                string query = @"SELECT 1 FROM Usuarios 
                             WHERE email_usuario = @correo AND 
                             codigo_recuperacion = @codigo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }


    }
}
