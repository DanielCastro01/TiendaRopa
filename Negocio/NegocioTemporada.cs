using DAO;
using Entidades;
using System.Collections.Generic;

namespace Negocio
{
    public class NegocioTemporada
    {
        private DAOTemporada DaoTemp = new DAOTemporada();

        public List<Temporada> Listar()
        {
            return DaoTemp.Listar();
        }

        public int Registrar(Temporada obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El Nombre de la Temporada no puede ser vacia";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {

                return DaoTemp.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }


        }

        public bool Editar(Temporada obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El Nombre de la Temporada no puede ser vacia";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return DaoTemp.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return DaoTemp.Eliminar(id, out Mensaje);
        }
    }
}
