using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class TipoEmpresa
    {
        public int IdTipoEmpresa { get; set; }
        public string Descripcion { get; set; }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.TipoEmpresa TE = bbdd.TipoEmpresa.First(e => e.IdTipoEmpresa == IdTipoEmpresa);

                CommonBC.Syncronize(TE, this);

                return true;

            }
            catch (Exception ex)
            {

                return false;


            }
        }
        public List<TipoEmpresa> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.TipoEmpresa> listadoDatos = bbdd.TipoEmpresa.ToList<Datos.TipoEmpresa>();

                List<TipoEmpresa> listadoNegocio = GenerarLista(listadoDatos);

                return listadoNegocio;
            }
            catch (Exception ex)
            {

                return new List<TipoEmpresa>();
            }
        }

        private List<TipoEmpresa> GenerarLista(List<Datos.TipoEmpresa> listadoDatos)
        {
            List<TipoEmpresa> listadoTE = new List<TipoEmpresa>();
            foreach (Datos.TipoEmpresa Dato in listadoDatos)
            {
                TipoEmpresa negocio = new TipoEmpresa();
                CommonBC.Syncronize(Dato, negocio);

                listadoTE.Add(negocio);
            }
            return listadoTE;
        }
    }
}
