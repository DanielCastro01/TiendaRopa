namespace Entidades
{
    public class Temporada
    {
        private int _id_Temporada;
        private string _Nombre;
        private bool _Estado;

        public Temporada() { }

        public int Id_Temporada { get => _id_Temporada; set => _id_Temporada = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public bool Estado { get => _Estado; set => _Estado = value; }

    }
}
