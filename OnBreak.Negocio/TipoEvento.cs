using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Datos;

namespace OnBreak.Negocio
{
    public class TipoEvento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public TipoEvento()
        {
            this.Init();
        }

        public void Init()
        {
            Id = 0;
            Descripcion = string.Empty;
        }

        //BuscarTipoEvento : Busca el tipo de evento por su id en la base de datos
        public TipoEvento BuscarTipoEvento(int id)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                try
                {
                    TipoEvento tipo = null;
                    var qTipo = bbdd.TipoEvento.Where(e => e.IdTipoEvento == id);
                    foreach (var flash in qTipo)
                    {
                        tipo = new TipoEvento
                        {
                            Id = flash.IdTipoEvento,
                            Descripcion = flash.Descripcion
                        };
                    }
                    return tipo;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        //ListarTipos : Lista los tipos de eventos registrados en la base de datos
        public List<TipoEvento> ListarTiposEventos()
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                List<TipoEvento> Eventos = new List<TipoEvento>();
                try
                {
                    var qTipoEvento = bbdd.TipoEvento;
                    foreach (var flash in qTipoEvento)
                    {
                        TipoEvento evento = new TipoEvento
                        {
                            Id = flash.IdTipoEvento,
                            Descripcion = flash.Descripcion
                        };
                        Eventos.Add(evento);
                    }
                    return Eventos;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
