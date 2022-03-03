using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak.Negocio.Almacen
{
    public class ClienteA
    {
        public string RutCliente { get; set; }
        public string RazonSocial { get; set; }
        public string NombreContacto { get; set; }
        public string MailContacto { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string ActividadEmpresa { get; set; }
        public string TipoEmpresa { get; set; }
        public int IdTipoEmpresa { get; set; }
        public int IdActividadEmpresa { get; set; }

        public static List<ClienteA> CargarGrid(IEnumerable<Cliente> Clientes)
        {
            List<ClienteA> ListCli = new List<ClienteA>();

            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            foreach (var clibbdd in Clientes)
            {
                ClienteA cli = new ClienteA();

                cli.RutCliente = clibbdd.RutCliente;
                cli.RazonSocial = clibbdd.RazonSocial;
                cli.NombreContacto = clibbdd.NombreContacto;
                cli.MailContacto = clibbdd.MailContacto;
                cli.Direccion = clibbdd.Direccion;
                cli.Telefono = clibbdd.Telefono;
                cli.IdActividadEmpresa = clibbdd.IdActividadEmpresa;
                cli.IdTipoEmpresa = clibbdd.IdTipoEmpresa;

                foreach (var AE in bbdd.ActividadEmpresa)
                {
                    if (clibbdd.IdActividadEmpresa == AE.IdActividadEmpresa)
                    {
                        cli.ActividadEmpresa = AE.Descripcion;
                    }
                }

                foreach (var TE in bbdd.TipoEmpresa)
                {
                    if (clibbdd.IdTipoEmpresa == TE.IdTipoEmpresa)
                    {
                        cli.TipoEmpresa = TE.Descripcion;
                    }
                }

                ListCli.Add(cli);

            }

            return ListCli;
        }

        public static List<ClienteA> CargarGridBase()
        {
            List<ClienteA> ListCli = new List<ClienteA>();

            Datos.OnBreakEntities bbdd = new Datos.OnBreakEntities();

            foreach (var clibbdd in bbdd.Cliente)
            {
                ClienteA cli = new ClienteA();

                cli.RutCliente = clibbdd.RutCliente;
                cli.RazonSocial = clibbdd.RazonSocial;
                cli.NombreContacto = clibbdd.NombreContacto;
                cli.MailContacto = clibbdd.MailContacto;
                cli.Direccion = clibbdd.Direccion;
                cli.Telefono = clibbdd.Telefono;
                cli.IdTipoEmpresa = clibbdd.IdTipoEmpresa;
                cli.IdActividadEmpresa = clibbdd.IdActividadEmpresa;

                foreach (var AE in bbdd.ActividadEmpresa)
                {
                    if (clibbdd.IdActividadEmpresa == AE.IdActividadEmpresa)
                    {
                        cli.ActividadEmpresa = AE.Descripcion;
                    }
                }

                foreach (var TE in bbdd.TipoEmpresa)
                {
                    if (clibbdd.IdTipoEmpresa == TE.IdTipoEmpresa)
                    {
                        cli.TipoEmpresa = TE.Descripcion;
                    }
                }

                ListCli.Add(cli);

            }

            return ListCli;
        }
    }
}
