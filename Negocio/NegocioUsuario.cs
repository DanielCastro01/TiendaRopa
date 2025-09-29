using DAO;
using Entidades;
using System.Collections.Generic;

namespace Negocio
{
    public class NegocioUsuario
    {
        private DAOUsuarios DaoUsu = new DAOUsuarios();

        public List<Usuario> Listar()
        {
            return DaoUsu.Listar();
        }

        public int Registrar(Usuario Usu, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(Usu.NombreUsuario) || string.IsNullOrWhiteSpace(Usu.NombreUsuario))
            {
                Mensaje = "El Nombre del Usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(Usu.EmailUsuario) || string.IsNullOrWhiteSpace(Usu.EmailUsuario))
            {
                Mensaje = "El Correo no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(Usu.ContrasenaUsuario) || string.IsNullOrWhiteSpace(Usu.ContrasenaUsuario))
            {
                Mensaje = "La contraseña no puede ser vacia";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                Usu.RolUsuario = true;
                string clave = Usu.ContrasenaUsuario;
                Usu.ContrasenaUsuario = NegocioRecursos.ConvertirSha64(clave);
                return DaoUsu.Registrar(Usu, out Mensaje);
            }
            else
            {
                return 0;
            }


        }

        public bool Editar(Usuario Usu, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(Usu.NombreUsuario) || string.IsNullOrWhiteSpace(Usu.NombreUsuario))
            {
                Mensaje = "El Nombre del Usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(Usu.EmailUsuario) || string.IsNullOrWhiteSpace(Usu.EmailUsuario))
            {
                Mensaje = "El Correo no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(Usu.ContrasenaUsuario) || string.IsNullOrWhiteSpace(Usu.ContrasenaUsuario))
            {
                Mensaje = "La contraseña no puede ser vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return DaoUsu.Editar(Usu, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return DaoUsu.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int idsuario, string nuevaclave, out string Mensaje)
        {
            return DaoUsu.CambiarClave(idsuario, nuevaclave, out Mensaje);
        }

        //dani

        public Usuario ObtenerPorCorreo(string correo) => DaoUsu.ObtenerPorCorreo(correo);

        public bool GuardarCodigoRecuperacion(string correo, string codigo) => DaoUsu.GuardarCodigoRecuperacion(correo, codigo);

        public int VerificarCodigoRecuperacion(string correo, string codigo) => DaoUsu.VerificarCodigo(correo, codigo);

        //fin dani

        public bool RestablecerClave(int idsuario, string clave, out string Mensaje)
        {

            Mensaje = string.Empty;
            bool resultado = DaoUsu.RestablecerClave(idsuario, clave, out Mensaje);

            if (resultado)
            {
                return true;
            }
            else
            {
                Mensaje = "No se pudo restablecer la contraseña";
                return false;
            }

        }

        public List<Usuario> VerificarUsuarioXclave(string actual)
        {
            return DaoUsu.Listar();
        }


    }
}
