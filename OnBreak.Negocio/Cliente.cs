using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnBreak.Datos;

namespace OnBreak.Negocio
{
    public class Cliente
    {
        public string RutCliente { get; set; }
        public string RazonSocial { get; set; }
        public string NombreContacto { get; set; }
        public string MailContacto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdActividadEmpresa { get; set; }
        public int IdTipoEmpresa { get; set; }

        public Cliente()
        {
            this.Init();
        }

        private void Init()
        {
            RutCliente = string.Empty;
            RazonSocial = string.Empty;
            NombreContacto = string.Empty;
            MailContacto = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
            IdActividadEmpresa = -1;
            IdTipoEmpresa = -1;
            

        }

        //Método para valir que el cliente exista
        public bool ExisteCliente(string Rut)
        {
            Datos.OnBreakEntities ddbb = new Datos.OnBreakEntities();

            try
            {
                Datos.Cliente client = ddbb.Cliente.First(c => c.RutCliente == Rut);

                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }


        public bool Create(Cliente cli) //agrega al cliente a la base de datos
        {

            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();
            Datos.Cliente client = new Datos.Cliente();

            try
            {
                client.RutCliente = cli.RutCliente;
                client.RazonSocial = cli.RazonSocial;
                client.NombreContacto = cli.NombreContacto;
                client.MailContacto = cli.MailContacto;
                client.Direccion = cli.Direccion;
                client.Telefono = cli.Telefono;
                client.IdActividadEmpresa = cli.IdActividadEmpresa;
                client.IdTipoEmpresa = cli.IdTipoEmpresa;


                bbdd.Cliente.Add(client);
                bbdd.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                bbdd.Cliente.Remove(client);
                return false;
            }

        }

        public bool Read()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.Cliente cli = bbdd.Cliente.First(e => e.RutCliente == RutCliente);

                CommonBC.Syncronize(cli, this);

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public Cliente ReadByRut(string rut)
        {
            using (Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities())
            {
                Cliente cliente = null;


                try
                {
                    var qCliente = bbdd.Cliente.Where(c => c.RutCliente == rut);

                    foreach (var flash in qCliente)
                    {


                        cliente = new Cliente
                        {
                            RutCliente = flash.RutCliente,
                            RazonSocial = flash.RazonSocial,
                            NombreContacto = flash.NombreContacto,
                            MailContacto = flash.MailContacto,
                            Direccion = flash.Direccion,
                            Telefono = flash.Telefono,
                            IdActividadEmpresa = flash.IdActividadEmpresa,
                            IdTipoEmpresa = flash.IdTipoEmpresa
                        };
                    }
                    return cliente;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }




        public bool Update()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.Cliente cli = bbdd.Cliente.First(e => e.RutCliente == RutCliente);

                CommonBC.Syncronize(this, cli);

                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Delete()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                Datos.Cliente cli = bbdd.Cliente.First(e => e.RutCliente == RutCliente);

                bbdd.Cliente.Remove(cli);

                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<Cliente> ReadAll()
        {
            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            try
            {
                List<Datos.Cliente> listadoDatos = bbdd.Cliente.ToList<Datos.Cliente>();

                List<Cliente> listadoNegocio = GenerarLista(listadoDatos);

                return listadoNegocio;
            }
            catch (Exception ex)
            {

                return new List<Cliente>();
            }
        }

        private List<Cliente> GenerarLista(List<Datos.Cliente> listadoDatos)
        {
            List<Cliente> listadoCliente = new List<Cliente>();
            foreach (Datos.Cliente Dato in listadoDatos)
            {
                Cliente negocio = new Cliente();
                CommonBC.Syncronize(Dato, negocio);

                listadoCliente.Add(negocio);
            }
            return listadoCliente;
        }

    }
}
