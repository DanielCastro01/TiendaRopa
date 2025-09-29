using System;

namespace Entidades
{
    public class Usuario
    {
        private int _idUsuario;
        private string _nombreUsuario;
        private string _emailUsuario;
        private string _contrasenaUsuario;
        private bool _rolUsuario;
        private bool _estado;
        private string _codigoRecuperacion;
        private DateTime? _fechaCodigo;

        public Usuario() { }

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string EmailUsuario { get => _emailUsuario; set => _emailUsuario = value; }
        public string ContrasenaUsuario { get => _contrasenaUsuario; set => _contrasenaUsuario = value; }
        public bool RolUsuario { get => _rolUsuario; set => _rolUsuario = value; }
        public bool Estado { get => _estado; set => _estado = value; }
        public string CodigoRecuperacion { get => _codigoRecuperacion; set => _codigoRecuperacion = value; }
        public DateTime? FechaCodigo { get => _fechaCodigo; set => _fechaCodigo = value; }
    }

}
