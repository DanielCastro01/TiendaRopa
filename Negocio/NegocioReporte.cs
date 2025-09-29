using DAO;
using Entidades;
using System.Collections.Generic;

namespace Negocio
{
    public class NegocioReporte
    {
        private DAOReporte DaoRe = new DAOReporte();

        public Dashboard VerDashboard()
        {
            return DaoRe.VerDashboard();
        }

        public List<Reporte> Ventas(string fechainicio, string fechafin, int idventa)
        {
            return DaoRe.Ventas(fechainicio, fechafin, idventa);
        }
    }


}
