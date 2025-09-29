namespace Entidades
{
    public class Reporte
    {
        public int IdVenta { get; set; }
        public string FechaVenta { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

    }
}
