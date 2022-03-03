using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class Cocktail
    {
        public string Numero { get; set; }
        public int IdTipoAmbientacion { get; set; }
        public bool Ambientacion { get; set; }
        public bool MusicaAmbiental { get; set; }
        public bool MusicaCliente { get; set; }

        public bool Create(Cocktail ck) //agrega al cliente a la base de datos
        {

            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Cocktail coc = new Datos.Cocktail();

            try
            {
                coc.Numero = ck.Numero;
                coc.IdTipoAmbientacion = ck.IdTipoAmbientacion;
                coc.Ambientacion = ck.Ambientacion;
                coc.MusicaAmbiental = ck.MusicaAmbiental;
                coc.MusicaCliente = ck.MusicaCliente;


                bbdd.Cocktail.Add(coc);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                bbdd.Cocktail.Remove(coc);
                return false;
            }

        }
    }
}
