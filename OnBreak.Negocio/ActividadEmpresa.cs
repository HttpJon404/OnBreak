using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class ActividadEmpresa
    {
        public int IdActividadEmpresa { get; set; }
        public string Descripcion { get; set; }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.ActividadEmpresa AE = bbdd.ActividadEmpresa.First(e => e.IdActividadEmpresa == IdActividadEmpresa);

                CommonBC.Syncronize(AE, this);

                return true;

            }
            catch (Exception ex)
            {

                return false;


            }
        }
        public List<ActividadEmpresa> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.ActividadEmpresa> listadoDatos = bbdd.ActividadEmpresa.ToList<Datos.ActividadEmpresa>();

                List<ActividadEmpresa> listadoNegocio = GenerarLista(listadoDatos);

                return listadoNegocio;
            }
            catch (Exception ex)
            {

                return new List<ActividadEmpresa>();
            }
        }

        private List<ActividadEmpresa> GenerarLista(List<Datos.ActividadEmpresa> listadoDatos)
        {
            List<ActividadEmpresa> listadoAE = new List<ActividadEmpresa>();
            foreach (Datos.ActividadEmpresa Dato in listadoDatos)
            {
                ActividadEmpresa negocio = new ActividadEmpresa();
                CommonBC.Syncronize(Dato, negocio);

                listadoAE.Add(negocio);
            }
            return listadoAE;
        }
    }

    
}