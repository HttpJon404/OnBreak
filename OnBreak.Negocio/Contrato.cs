using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Datos;


namespace OnBreak.Negocio.Memento
{
    public class Contrato
    {
        public string NumeroContrato { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime Termino { get; set; }
        public string RutCliente { get; set; }
        public ModalidadServicio Modalidad { get; set; }
        public TipoEvento Tipo { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraTermino { get; set; }
        public int Asistentes { get; set; }
        public int PersonalAdicional { get; set; }
        public bool Realizado { get; set; }
        public float ValorTotalContrato { get; set; }
        public string Observaciones { get; set; }

        public Contrato()
        {
            this.Init();
        }


        public void Init()
        {
            NumeroContrato = string.Empty;
            Creacion = DateTime.Today;
            Termino = DateTime.Today;
            RutCliente = string.Empty;
            Modalidad = null;
            Tipo = null;
            FechaHoraInicio = DateTime.Today;
            FechaHoraTermino = DateTime.Today;
            Asistentes = 0;
            PersonalAdicional = 0;
            Realizado = true;
            ValorTotalContrato = 0;
            Observaciones = string.Empty;
        }

        public bool Create(Contrato contrato)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                try
                {
                    Datos.Contrato bdContrato = new Datos.Contrato
                    {
                        //asigna valores del objeto cliente a la base de datos.(Syncronyze)
                        Numero = this.NumeroContrato,
                        Creacion = this.Creacion,
                        Termino = this.Termino,
                        RutCliente = this.RutCliente,
                        IdModalidad = this.Modalidad.Id,
                        IdTipoEvento = this.Tipo.Id,
                        FechaHoraInicio = this.FechaHoraInicio,
                        FechaHoraTermino = this.FechaHoraTermino,
                        Asistentes = this.Asistentes,
                        PersonalAdicional = this.PersonalAdicional,
                        Realizado = this.Realizado,
                        ValorTotalContrato = this.ValorTotalContrato,
                        Observaciones = this.Observaciones
                    };

                    bbdd.Contrato.Add(bdContrato);
                    bbdd.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        //Busca un contrato según el número
        public Contrato Read(string NumContrato)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                Contrato contrato = null;
                ModalidadServicio modalidad = new ModalidadServicio();
                TipoEvento tipo = new TipoEvento();

                try
                {
                    var qContrato = bbdd.Contrato.Where(c => c.Numero == NumContrato);

                    foreach (var flash in qContrato)
                    {
                        //busca la actividad empresa asociado a un cliente
                        string IdModalidad = flash.IdModalidad;
                        modalidad = modalidad.BuscarModalidad(IdModalidad);

                        //busca el tipo de empresa asociado a un cliente
                        int IdTipoEvento = flash.IdTipoEvento;
                        tipo = tipo.BuscarTipoEvento(IdTipoEvento);

                        contrato = new Contrato
                        {
                            NumeroContrato = flash.Numero,
                            Creacion = flash.Creacion,
                            Termino = flash.Termino,
                            RutCliente = flash.RutCliente,
                            Modalidad = modalidad,
                            Tipo = tipo,
                            FechaHoraInicio = flash.FechaHoraInicio,
                            FechaHoraTermino = flash.FechaHoraTermino,
                            Asistentes = flash.Asistentes,
                            PersonalAdicional = flash.PersonalAdicional,
                            Realizado = flash.Realizado,
                            ValorTotalContrato = (float)flash.ValorTotalContrato,
                            Observaciones = flash.Observaciones
                        };
                    }
                    return contrato;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }

        //Recorre
        public bool Update()
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                try
                {
                    var qContrato = bbdd.Contrato.Where(c => c.Numero == this.NumeroContrato);

                    foreach (var flash in qContrato)
                    {
                        flash.Numero = flash.Numero;
                        flash.Creacion = flash.Creacion;

                        flash.RutCliente = flash.RutCliente;
                        flash.Realizado = this.Realizado;

                        flash.ValorTotalContrato = this.ValorTotalContrato;
                        flash.FechaHoraInicio = this.FechaHoraInicio;
                        flash.FechaHoraTermino = this.FechaHoraTermino;
                        flash.Termino = this.Termino;
                        flash.IdModalidad = this.Modalidad.Id;
                        flash.IdTipoEvento = this.Tipo.Id;
                        flash.Asistentes = this.Asistentes;
                        flash.PersonalAdicional = this.PersonalAdicional;
                        flash.Observaciones = this.Observaciones;
                    }

                    bbdd.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public List<Contrato> ReadAll(string vNumeroContrato = "", string vRut = "",
        string vNombreModalidad = "", int vIdTipoEvento = 0)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                List<Contrato> contratos = new List<Contrato>();
                ModalidadServicio modalidad = new ModalidadServicio();
                TipoEvento tipo = new TipoEvento();
                try
                {
                    //consulta erronea para compatibilidad de variables
                    var qContrato = bbdd.Contrato.Where(c => c.Numero == c.Numero);

                    //comprobar busqueda por numero de contrato
                    if (vNumeroContrato != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.Numero == vNumeroContrato);
                    }

                    //comprobar busqueda por rut del cliente
                    if (vRut != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.RutCliente == vRut);
                    }

                    //comprobar busqueda por modalidad del servicio
                    if (vNombreModalidad != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.ModalidadServicio.Nombre == vNombreModalidad);
                    }

                    //comprobar busqueda por tipo de evento
                    if (vIdTipoEvento != 0)
                    {
                        qContrato = bbdd.Contrato.Where(c => c.IdTipoEvento == vIdTipoEvento);
                    }

                    //comprobar busqueda por rut del cliente y tipo de evento
                    if (vRut != "" && vIdTipoEvento != 0)
                    {
                        qContrato = bbdd.Contrato.Where(c => c.RutCliente ==
                        vRut && c.IdTipoEvento == vIdTipoEvento);
                    }

                    //comprobar busqueda por numero de contrato y rut del cliente
                    if (vNumeroContrato != "" && vRut != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.Numero ==
                        vNumeroContrato && c.RutCliente == vRut);
                    }



                    //comprobar busqueda por rut del cliente y modalidad del servicio
                    if (vRut != "" && vNombreModalidad != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.RutCliente ==
                        vRut && c.ModalidadServicio.Nombre == vNombreModalidad);
                    }

                    //comprobar busqueda por numero de contrato y tipo de evento
                    if (vNumeroContrato != "" && vIdTipoEvento != 0)
                    {
                        qContrato = bbdd.Contrato.Where(c => c.Numero ==
                        vNumeroContrato && c.IdTipoEvento == vIdTipoEvento);
                    }

                    //comprobar busqueda por numero de contrato y modalidad del servicio
                    if (vNumeroContrato != "" && vNombreModalidad != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.Numero ==
                        vNumeroContrato && c.ModalidadServicio.Nombre == vNombreModalidad);
                    }

                    //comprobar busqueda por tipo de evento y modalidad del servicio
                    if (vIdTipoEvento != 0 && vNombreModalidad != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.IdTipoEvento ==
                        vIdTipoEvento && c.ModalidadServicio.Nombre == vNombreModalidad);
                    }

                    //comprobar busqueda por numero del contrato , por rut del cliente y tipo de evento
                    if (vNumeroContrato != "" && vRut != "" && vIdTipoEvento != 0)
                    {
                        qContrato = bbdd.Contrato.Where(c => c.Numero ==
                        vNumeroContrato && c.RutCliente == vRut && c.IdTipoEvento == vIdTipoEvento);
                    }

                    //comprobar busqueda por modalidad del servicio , por rut del cliente y tipo de evento
                    if (vNombreModalidad != "" && vRut != "" && vIdTipoEvento != 0)
                    {
                        qContrato = bbdd.Contrato.Where(c => c.ModalidadServicio.Nombre ==
                        vNombreModalidad && c.RutCliente == vRut && c.IdTipoEvento == vIdTipoEvento);
                    }

                    //comprobar por numero de contrato , rut del cliente y modalidad del servicio
                    if (vNumeroContrato != "" && vRut != "" && vNombreModalidad != "")
                    {
                        qContrato = bbdd.Contrato.Where(c => c.Numero ==
                        vNumeroContrato && c.RutCliente == vRut &&
                        c.ModalidadServicio.Nombre == vNombreModalidad);
                    }

                    //comprobar por numero de contrato , rut del cliente , modalidad del servicio y tipo de evento
                    if (vNumeroContrato != "" && vRut != "" && vNombreModalidad != "" && vIdTipoEvento != 0)
                    {
                        qContrato = bbdd.Contrato.Where(c => c.Numero ==
                        vNumeroContrato && c.RutCliente == vRut &&
                        c.ModalidadServicio.Nombre == vNombreModalidad && c.IdTipoEvento == vIdTipoEvento);
                    }

                    foreach (var flash in qContrato)
                    {
                        //busca la modalidad de servicio asociada a un contrato
                        string IdModalidad = flash.IdModalidad;
                        modalidad = modalidad.BuscarModalidad(IdModalidad);

                        //busca el tipo de evento asociado a un contrato
                        int IdTipoEvento = flash.IdTipoEvento;
                        tipo = tipo.BuscarTipoEvento(IdTipoEvento);

                        Contrato contrato = new Contrato
                        {
                            NumeroContrato = flash.Numero,
                            Creacion = flash.Creacion,
                            Termino = flash.Termino,
                            RutCliente = flash.RutCliente,
                            Modalidad = modalidad,
                            Tipo = tipo,
                            FechaHoraInicio = flash.FechaHoraInicio,
                            FechaHoraTermino = flash.FechaHoraTermino,
                            Asistentes = flash.Asistentes,
                            PersonalAdicional = flash.PersonalAdicional,
                            Realizado = flash.Realizado,
                            ValorTotalContrato = (float)flash.ValorTotalContrato,
                            Observaciones = flash.Observaciones
                        };

                        contratos.Add(contrato);
                    }
                    return contratos;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public bool FinalizarCancelar()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.Contrato con = bbdd.Contrato.First(e => e.Numero == NumeroContrato);

                CommonBC.Syncronize(this, con);

                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}

