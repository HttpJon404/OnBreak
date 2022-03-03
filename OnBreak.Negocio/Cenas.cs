using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Cenas
    {

        public string Numero { get; set; }
        public int IdTipoAmbientacion { get; set; }
        public bool MusicaAmbiental { get; set; }
        public bool LocalOnBreak { get; set; }
        public bool OtroLocalOnBreak { get; set; }
        public double ValorArriendo { get; set; }

        public bool Create(Cenas ce)
        {

            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Cenas cen = new Datos.Cenas();

            try
            {
                cen.Numero = ce.Numero;
                cen.IdTipoAmbientacion = ce.IdTipoAmbientacion;
                cen.MusicaAmbiental = ce.MusicaAmbiental;
                cen.LocalOnBreak = ce.LocalOnBreak;
                cen.OtroLocalOnBreak = ce.OtroLocalOnBreak;
                cen.ValorArriendo = ce.ValorArriendo;


                bbdd.Cenas.Add(cen);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                bbdd.Cenas.Remove(cen);
                return false;
            }

        }

    }
}
