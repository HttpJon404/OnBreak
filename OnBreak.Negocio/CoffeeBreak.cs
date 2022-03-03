using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio
{
    public class CoffeeBreak
    {
        public string Numero { get; set; }
        public bool Vegetariana { get; set; }

        public bool Create(CoffeeBreak cof) //agrega al cliente a la base de datos
        {

            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.CoffeeBreak cb = new Datos.CoffeeBreak();

            try
            {
                
                cb.Numero = cof.Numero;
                cb.Vegetariana = cof.Vegetariana;


                bbdd.CoffeeBreak.Add(cb);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                bbdd.CoffeeBreak.Remove(cb);
                return false;
            }
        }
    }
}
