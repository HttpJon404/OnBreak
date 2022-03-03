using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Datos;

namespace OnBreak.Negocio
{
    public class ModalidadServicio
    {
        public string Id { get; set; }
        public TipoEvento Tipo { get; set; }
        public string Nombre { get; set; }
        public double ValorBase { get; set; }
        public int PersonalBase { get; set; }

        public ModalidadServicio()
        {
            this.Init();
        }

        public void Init()
        {
            Id = string.Empty;
            Tipo = null;
            Nombre = string.Empty;
            ValorBase = 0;
            PersonalBase = 0;
        }

        //BuscarModalidad : Busca modalidades de servicios por su id en la base de datos
        public ModalidadServicio BuscarModalidad(string id)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {

                ModalidadServicio modalidad = null;
                TipoEvento tipo = new TipoEvento();
                try
                {
                    var qModalidad = bbdd.ModalidadServicio.Where(c => c.IdModalidad == id);

                    foreach (var flash in qModalidad)
                    {
                        //busca el tipo de evento asociado a una modalidad
                        int IdTipoEvento = flash.IdTipoEvento;
                        tipo = tipo.BuscarTipoEvento(IdTipoEvento);

                        modalidad = new ModalidadServicio
                        {
                            Id = flash.IdModalidad,
                            Tipo = tipo,
                            Nombre = flash.Nombre,
                            ValorBase = flash.ValorBase,
                            PersonalBase = flash.PersonalBase
                        };
                    }
                    return modalidad;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }

        public ModalidadServicio BuscarModalidadNueva(string nombre)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {

                ModalidadServicio modalidad = null;
                TipoEvento tipo = new TipoEvento();
                try
                {
                    var qModalidad = bbdd.ModalidadServicio.Where(c => c.Nombre == nombre);

                    foreach (var flash in qModalidad)
                    {
                        //busca el tipo de evento asociado a una modalidad
                        int IdTipoEvento = flash.IdTipoEvento;
                        tipo = tipo.BuscarTipoEvento(IdTipoEvento);

                        modalidad = new ModalidadServicio
                        {
                            Id = flash.IdModalidad,
                            Tipo = tipo,
                            Nombre = flash.Nombre,
                            ValorBase = flash.ValorBase,
                            PersonalBase = flash.PersonalBase
                        };
                    }
                    return modalidad;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }





        //ListarModalidades : lista las modalidades de servicio de la bd
        public List<ModalidadServicio> ListarModalidades(int id)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                List<ModalidadServicio> Modalidades = new List<ModalidadServicio>();
                TipoEvento tipo = new TipoEvento();
                try
                {
                    var qModalidad = bbdd.ModalidadServicio.Where(c => c.IdTipoEvento == id);
                    foreach (var flash in qModalidad)
                    {
                        //busca el tipo de evento asociado a una modalidad
                        int IdTipoEvento = id;
                        tipo = tipo.BuscarTipoEvento(id);
                        ModalidadServicio modalidad = new ModalidadServicio
                        {
                            Id = flash.IdModalidad,
                            Tipo = tipo.BuscarTipoEvento(flash.IdTipoEvento),
                            Nombre = flash.Nombre,
                            ValorBase = flash.ValorBase,
                            PersonalBase = flash.PersonalBase
                        };
                        Modalidades.Add(modalidad);
                    }
                    return Modalidades;
                }
                catch (Exception ex)
                {
                    return Modalidades;
                }
            }
        }

        public List<ModalidadServicio> ListarModalidadesCombo()
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                List<ModalidadServicio> Modalidades = new List<ModalidadServicio>();
                try
                {
                    var qModalidad = bbdd.ModalidadServicio;
                    foreach (var flash in qModalidad)
                    {
                        ModalidadServicio modalidad = new ModalidadServicio
                        {
                            Id = flash.IdModalidad,
                            Nombre = flash.Nombre,
                            PersonalBase = flash.PersonalBase,
                            ValorBase = flash.ValorBase
                        };
                        Modalidades.Add(modalidad);
                    }
                    return Modalidades;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}


