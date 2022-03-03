using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using OnBreak.Negocio;
using OnBreak.Negocio.Almacen;
using System.Text.RegularExpressions;
using MahApps.Metro;
using OnBreak.Negocio.Memento;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Threading;

namespace OnBreakWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Contrato contrato = new Contrato();
        ModalidadServicio modalidad = new ModalidadServicio();
        TipoEvento tipo = new TipoEvento();
        bool conAux = false;
        bool cliAux = false;
        public MainWindow()
        {
            InitializeComponent();
            InicioGrid();
            CbActividadEmp();
            CbTipoEmp();
            CbTipoEmpLis();
            CbActividadEmpresaLis();
            MostrarClientes();
            conAux = false;
            cliAux = false;
            CargarTiposEventosLista();
            CargarTiposEventos();
            txtEstado.IsReadOnly = true;
            txtTermino.IsReadOnly = true;
            txtCreacion.IsReadOnly = true;
            txtNombreEmpresa.IsReadOnly = true;
            txtValorBase.IsReadOnly = true;
            txtValorTotal.IsReadOnly = true;
            txtPersonalBase.IsReadOnly = true;
            RecuperarContrato();
            LogosNormal();
        }

       

        private async void RecuperarContrato()
        {
            //ruta del archivo
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = path + @"..\..\..\OnBreak.Negocio\Backup\Contrato.xml";

            if (File.Exists(path))
            {
                MessageDialogResult resultado = await this.ShowMessageAsync("Información", "Se encontró un respaldo de un contrato.¿Desea recuparlo?."
                    , MessageDialogStyle.AffirmativeAndNegative);

                if (resultado == MessageDialogResult.Affirmative)
                {
                    //Main.Content = new Contratos(true);
                    //logo.Visibility = Visibility.Hidden;
                    Height = 552.2;
                    Width = 1308.132;
                    Title = "OnBreak - Administración de Contratos";
                    GridAdmContratos.Visibility = Visibility.Visible;
                    GridInicio.Visibility = Visibility.Hidden;
                    GridAdmCli.Visibility = Visibility.Hidden;
                    FlyListadoClientes.IsOpen = false;
                    FlyListadoContratos.IsOpen = false;
                    txtRutCon.IsReadOnly = false;
                    LimpiarCliente();
                    LimpiarContrato();

                    RestaurarRespaldo();
                }
                else
                {
                    File.Delete(path);
                }
            }
        }


        private void InicioGrid()
        {
            Title = "OnBreak - Eventos Empresariales";
            Height = 603.4;
            Width = 1116.732;
            GridInicio.Visibility = Visibility.Visible;
            GridAdmCli.Visibility = Visibility.Hidden;
            GridAdmContratos.Visibility = Visibility.Hidden;
            FlyListadoClientes.IsOpen = false;
            dgContratos.Visibility = Visibility.Hidden;
            GridColores.Visibility = Visibility.Hidden;


        }

        public void GridClientes()
        {
            DgClientes.Columns[0].Header = "Rut";
            DgClientes.Columns[0].DisplayIndex = 0;
            DgClientes.Columns[1].Header = "Razón Social";
            DgClientes.Columns[1].DisplayIndex = 1;
            DgClientes.Columns[2].Header = "Nombre Contacto";
            DgClientes.Columns[2].DisplayIndex = 2;
            DgClientes.Columns[3].Header = "E-Mail";
            DgClientes.Columns[3].DisplayIndex = 3;
            DgClientes.Columns[4].Header = "Dirección";
            DgClientes.Columns[4].DisplayIndex = 4;
            DgClientes.Columns[5].Header = "Teléfono";
            DgClientes.Columns[5].DisplayIndex = 5;
            DgClientes.Columns[6].Header = "Actividad Empresa";
            DgClientes.Columns[6].DisplayIndex = 6;
            DgClientes.Columns[7].Header = "Tipo Empresa";
            DgClientes.Columns[7].DisplayIndex = 7;
            DgClientes.Columns[8].Visibility = Visibility.Hidden;
            DgClientes.Columns[9].Visibility = Visibility.Hidden;
        }

        public void GridContratos()
        {
            dgContratos.Columns[0].Header = "Número Contrato";
            dgContratos.Columns[0].DisplayIndex = 0;
            dgContratos.Columns[1].Header = "Fecha de Creación";
            dgContratos.Columns[1].DisplayIndex = 1;
            dgContratos.Columns[2].Header = "Fecha de Término";
            dgContratos.Columns[2].DisplayIndex = 2;
            dgContratos.Columns[3].Header = "Rut";
            dgContratos.Columns[3].DisplayIndex = 3;
            dgContratos.Columns[4].Header = "Modalidad";
            dgContratos.Columns[4].DisplayIndex = 4;
            dgContratos.Columns[5].Header = "Tipo de Evento";
            dgContratos.Columns[5].DisplayIndex = 5;
            dgContratos.Columns[6].Header = "Hora de Inicio";
            dgContratos.Columns[6].DisplayIndex = 6;
            dgContratos.Columns[7].Header = "Hora de Termino";
            dgContratos.Columns[7].DisplayIndex = 7;
            dgContratos.Columns[8].Header = "Total de Asistentes";
            dgContratos.Columns[8].DisplayIndex = 8;
            dgContratos.Columns[9].Header = "Total Personal Adicional";
            dgContratos.Columns[9].DisplayIndex = 9;
            dgContratos.Columns[10].Header = "Realizado";
            dgContratos.Columns[10].DisplayIndex = 10;
            dgContratos.Columns[11].Header = "Valor del Contrato";
            dgContratos.Columns[11].DisplayIndex = 11;
        }



        private void MostrarClientes()
        {
            DgClientes.ItemsSource = ClienteA.CargarGridBase();

        }


        private void LimpiarCliente()
        {
            txtRutCliente.Clear();
            txtNombreCliente.Clear();
            txtDireccionCli.Clear();
            cboActividadEmpCli.SelectedIndex = -1;
            txtRazonSocialCli.Clear();
            txtEmailCli.Clear();
            txtTelefonoCli.Clear();
            cboTipoEmpresaCli.SelectedIndex = -1;
            MostrarClientes();
            txtRutCliente.IsReadOnly = false;
            /*MostrarContratos();*/
        }


        public void CbTipoEmp()
        {
            TipoEmpresa TE = new TipoEmpresa();
            cboTipoEmpresaCli.ItemsSource = TE.ReadAll();
            cboTipoEmpresaCli.SelectedValuePath = "IdTipoEmpresa";
            cboTipoEmpresaCli.DisplayMemberPath = "Descripcion";
            cboTipoEmpresaCli.SelectedIndex = -1;
        }

        public void CbActividadEmp()
        {
            ActividadEmpresa AE = new ActividadEmpresa();
            cboActividadEmpCli.ItemsSource = AE.ReadAll();
            cboActividadEmpCli.SelectedValuePath = "IdActividadEmpresa";
            cboActividadEmpCli.DisplayMemberPath = "Descripcion";
            cboActividadEmpCli.SelectedIndex = -1;
        }

        public void CbTipoEmpLis()
        {
            TipoEmpresa TE = new TipoEmpresa();
            cboTipoEmpresaLis.ItemsSource = TE.ReadAll();
            cboTipoEmpresaLis.SelectedValuePath = "IdTipoEmpresa";
            cboTipoEmpresaLis.DisplayMemberPath = "Descripcion";
            cboTipoEmpresaLis.SelectedIndex = -1;
        }

        public void CbActividadEmpresaLis()
        {
            ActividadEmpresa AE = new ActividadEmpresa();
            cboActividadEmpresaLis.ItemsSource = AE.ReadAll();
            cboActividadEmpresaLis.SelectedValuePath = "IdActividadEmpresa";
            cboActividadEmpresaLis.DisplayMemberPath = "Descripcion";
            cboActividadEmpresaLis.SelectedIndex = -1;
        }


        private void BtnAdmClientes_Click(object sender, RoutedEventArgs e)
        {

            GridAdmCli.Visibility = Visibility.Visible;
            GridInicio.Visibility = Visibility.Hidden;
            GridAdmContratos.Visibility = Visibility.Hidden;
            FlyListadoContratos.IsOpen = false;
            FlyListadoClientes.IsOpen = false;
            Height = 603.4;
            Width = 1116.732;
            Title = "OnBreak - Administración de clientes";
            LimpiarContrato();
        }

        private void BtnListadoCli_Click(object sender, RoutedEventArgs e)
        {
            Height = 603.4;
            Width = 1116.732;
            Title = "OnBreak - Listado de clientes";
            TituloListadoCli.Content = "Listado de cliente";
            FlyListadoClientes.IsOpen = true;
            cliAux = false;
            GridInicio.Visibility = Visibility.Hidden;
            GridAdmCli.Visibility = Visibility.Hidden;
            GridAdmContratos.Visibility = Visibility.Hidden;
            btnSalirLisCon.Visibility = Visibility.Hidden;
            dgContratos.Visibility = Visibility.Visible;
            CargarTabla();
            FlyListadoContratos.IsOpen = false;
            LimpiarContrato();
            LimpiarCliente();
        }

        private async void BtnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (txtRutCliente.Text != string.Empty)
            {
                Cliente cli = new Cliente();
                cli.RutCliente = txtRutCliente.Text;
                if (cli.Read())
                {
                    txtRutCliente.Text = cli.RutCliente;
                    txtNombreCliente.Text = cli.NombreContacto;
                    txtDireccionCli.Text = cli.Direccion;
                    txtRazonSocialCli.Text = cli.RazonSocial;
                    txtEmailCli.Text = cli.MailContacto;
                    txtTelefonoCli.Text = cli.Telefono;
                    cboActividadEmpCli.SelectedIndex = cli.IdActividadEmpresa;
                    cboTipoEmpresaCli.SelectedValue = cli.IdTipoEmpresa;
                }
                else
                {
                    await this.ShowMessageAsync("Error", "El cliente no se encuentra registrado.");
                }
            }
            else
            {
                await this.ShowMessageAsync("Error", "Debe ingresar un rut para la búsqueda.");
            }
        }

        private void BT_BuscarRutCon_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool FormularioLlenoClientes()
        {
            if (txtNombreCliente.Text != string.Empty && txtRazonSocialCli.Text != string.Empty &&
                    txtDireccionCli.Text != string.Empty && txtEmailCli.Text != string.Empty
                    && txtRutCliente.Text != string.Empty && txtTelefonoCli.Text != string.Empty &&
                    cboActividadEmpCli.SelectedIndex != -1 && cboTipoEmpresaCli.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void BT_AgregarCli_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();



            if (cli.ExisteCliente(txtRutCliente.Text) == false)
            {
                if (FormularioLlenoClientes() == true)
                {
                    cli.RutCliente = txtRutCliente.Text;
                    cli.NombreContacto = txtNombreCliente.Text;
                    cli.Direccion = txtDireccionCli.Text;
                    cli.RazonSocial = txtRazonSocialCli.Text;
                    cli.MailContacto = txtEmailCli.Text;
                    cli.Telefono = txtTelefonoCli.Text;
                    cli.IdActividadEmpresa = Convert.ToInt32(cboActividadEmpCli.SelectedValue);
                    cli.IdTipoEmpresa = Convert.ToInt32(cboTipoEmpresaCli.SelectedValue);
                    LimpiarCliente();

                    try
                    {

                        cli.Create(cli);
                        await this.ShowMessageAsync("Información", "Cliente agregado correctamente.");
                        DgClientes.ItemsSource = ClienteA.CargarGridBase();

                    }
                    catch (Exception ex)
                    {

                        await this.ShowMessageAsync("Información", "No se pudo agregar el cliente.");
                    }
                }
                else
                {
                    await this.ShowMessageAsync("Error", "Para agregar un cliente debe llenar todos los datos del formulario");
                }
            }
            else
            {
                await this.ShowMessageAsync("Error", "El cliente que intentas agregar ya existe.");
            }




        }


        private async void BT_EliminarCli_Click(object sender, RoutedEventArgs e)
        {
            {
                if (txtRutCliente.Text != string.Empty)
                {

                    MessageDialogResult resultado = await this.ShowMessageAsync("Confirmación", "¿Estás seguro que deseas eliminar a este cliente? "
                    , MessageDialogStyle.AffirmativeAndNegative);

                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        Cliente cli = new Cliente
                        {
                            RutCliente = txtRutCliente.Text
                        };
                        if (cli.Delete())
                        {
                            await this.ShowMessageAsync("Aviso", "El cliente ha sido eliminado.");
                            LimpiarCliente();
                        }
                        else
                        {
                            await this.ShowMessageAsync("Error", "No se pudo eliminar el cliente.");
                        }
                    }


                }
                else
                {
                    await this.ShowMessageAsync("Error", "Para eliminar un cliente debe ingresar su rut.");
                }

            }
        }

        private void cboTipoEmpresaCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboActividadEmpCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DgClientes_AutoGeneratedColumns(object sender, EventArgs e)
        {
            DgClientes.IsReadOnly = true;
            GridClientes();

        }

        private async void BT_ActualizarCli_Click(object sender, RoutedEventArgs e)
        {
            {
                if (txtRutCliente.Text != string.Empty)
                {
                    Cliente cli = new Cliente();
                    cli.RutCliente = txtRutCliente.Text;
                    cli.NombreContacto = txtNombreCliente.Text;
                    cli.Direccion = txtDireccionCli.Text;
                    cli.RazonSocial = txtRazonSocialCli.Text;
                    cli.MailContacto = txtEmailCli.Text;
                    cli.Telefono = txtTelefonoCli.Text;
                    cli.IdActividadEmpresa = Convert.ToInt32(cboActividadEmpCli.SelectedValue);
                    cli.IdTipoEmpresa = Convert.ToInt32(cboTipoEmpresaCli.SelectedValue);
                    if (cli.Update())
                    {
                        await this.ShowMessageAsync("Aviso", "El cliente ha sido modificado correctamente.");
                        LimpiarCliente();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Error", "No se pudo modificar el cliente.");
                    }
                }
                else
                {
                    await this.ShowMessageAsync("Error", "Debe ingresar un rut.");
                }

            }
        }

        private void BT_LimpiarFiltroLis_Click(object sender, RoutedEventArgs e)
        {
            txtRutLis.Text = String.Empty;
            cboActividadEmpresaLis.SelectedIndex = -1;
            cboTipoEmpresaLis.SelectedIndex = -1;
        }

        private void OnlyNumbersRut(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9-K-]+").IsMatch(e.Text);
        }

        private void BtnBuscarClienteLis_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();
            if (txtRutLis.Text != string.Empty)
            {


                cli.RutCliente = txtRutLis.Text;
                if (cli.Read())
                {
                    var Resultado = from Cliente in cli.ReadAll()
                                    where Cliente.RutCliente == txtRutLis.Text
                                    select Cliente;

                    var R = ClienteA.CargarGrid(Resultado);

                    DgClientes.ItemsSource = null;
                    DgClientes.ItemsSource = R;

                }
            }
            else
            {
                DgClientes.ItemsSource = ClienteA.CargarGridBase();
            }
        }

        private void DgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        //-----------------------------------CONTRATOS-----------------------------------------------------------------------//


        //-----------------------------------CONTRATOS-----------------------------------------------------------------------//


        public MainWindow(Contrato contrato, object materia)
        {
            InitializeComponent();
            CargarTiposEventos();
        }



        //abrir pestaña de administración de clientes


        private void BtnAdmContratos_Click(object sender, RoutedEventArgs e)
        {
            Height = 603.4;
            Width = 1308.132;
            Title = "OnBreak - Administración de Contratos";
            GridAdmContratos.Visibility = Visibility.Visible;
            GridInicio.Visibility = Visibility.Hidden;
            GridAdmCli.Visibility = Visibility.Hidden;
            FlyListadoClientes.IsOpen = false;
            FlyListadoContratos.IsOpen = false;
            txtRutCon.IsReadOnly = false;
            LimpiarCliente();
            LimpiarContrato();
            GridCenas.Visibility = Visibility.Hidden;
            GridCoffeBreak.Visibility = Visibility.Hidden;
            GridCocktail.Visibility = Visibility.Hidden;
            rbBasica.IsEnabled = false;
            rbPer.IsEnabled = false;
        }

        private void LimpiarContrato()
        {
            txtRutCliente.IsReadOnly = false;
            txtNombreEmpresa.Text = string.Empty;
            txtRutCon.Text = string.Empty;
            txtCreacion.Text = string.Empty;
            txtNumAsistentes.Text = string.Empty;
            txtNumContrato.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            cboModalidadServicio.SelectedIndex = 0;
            cboTipoEvento.SelectedIndex = 0;
            txtPersonalAdicional.Text = string.Empty;
            txtTermino.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtValorBase.Text = string.Empty;
            txtValorTotal.Text = string.Empty;
            txtPersonalBase.Text = string.Empty;
            dpFechaInicio.SelectedDate = null;
            dpFechaTermino.SelectedDate = null;
            ckAmbientacion.IsChecked = false;
            ckMusicaAmb.IsChecked = false;
            ckMusicaCli.IsChecked = false;
            rbPer.IsChecked = false;
            rbBasica.IsChecked = false;
            ckVegetariana.IsChecked = false;
            ckAmbientacion.IsChecked = false;
            ckMusicaCen.IsChecked = false;
            rbBasicaCen.IsChecked = true;
            rbLocalCen.IsChecked = false;
            rbOtroLocalCen.IsChecked = false;
            txtValorArriendo.Text = string.Empty;

        }

        public int ConvertirTipoInverso(int numero)
        {
            switch (numero)
            {
                case 10: return 1;
                case 20: return 2;
                case 30: return 3;
                case 40: return 4;
                case 50: return 5;
            }
            return 0;
        }




        private void CargarTabla()
        {
            List<dynamic> contratos = new List<dynamic>();
            Contrato contrato = new Contrato();
            foreach (var flash in contrato.ReadAll())
            {
                var objContrato = new
                {
                    NumerodeContrato = flash.NumeroContrato,
                    Creacion = flash.Creacion,
                    Termino = flash.Termino,
                    RutdelCliente = flash.RutCliente,
                    Modalidad = flash.Modalidad.Nombre,
                    Tipo = flash.Tipo.Descripcion,
                    FechayHoradeInicio = flash.FechaHoraInicio,
                    FechayHoradeTermino = flash.FechaHoraTermino,
                    Asistentes = flash.Asistentes,
                    PersonalAdicional = flash.PersonalAdicional,
                    Realizado = flash.Realizado,
                    ValorTotaldelContrato = flash.ValorTotalContrato,
                    Observaciones = flash.Observaciones
                };
                contratos.Add(objContrato);
            }
            //Asigna la lista al dataGrid.
            dgContratos.ItemsSource = contratos;

        }

        //Método que convierte la selección del combobox en un número valido para la base de datos.
        public int ConvertirTipo(int numero)
        {
            switch (numero)
            {
                case 1: return 10;
                case 2: return 20;
                case 3: return 30;
                case 4: return 40;
            }
            return 0;
        }

        //Valida que la cantidad de asistentes sea un número valido.
        public bool ValidarAsistentes()
        {

            if (int.TryParse(txtNumAsistentes.Text, out int CantAsistentes) == true)
            {
                if (CantAsistentes >= 1 && CantAsistentes <= 300)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;

        }

        //Carga el comboBox del tipo de eventos.
        private void CargarTiposEventos()
        {
            TipoEvento tipo = new TipoEvento();
            cboTipoEvento.Items.Add("Seleccione");

            foreach (var flash in tipo.ListarTiposEventos())
            {
                cboTipoEvento.Items.Add(flash.Descripcion);

            }
            cboTipoEvento.SelectedIndex = 0;
        }


        //Carga el comboBox de modalidades cuando se selecciona un tipo de evento.
        private void CB_TipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboModalidadServicio.Items.Clear();
            CargarModalidades();
            int x = cboTipoEvento.SelectedIndex;

            switch (x)
            {
                case 1: GridCoffeBreak.Visibility = Visibility.Visible;
                        GridCocktail.Visibility = Visibility.Hidden;
                        GridCenas.Visibility = Visibility.Hidden;
                    break;
                case 2: GridCocktail.Visibility = Visibility.Visible;
                        GridCoffeBreak.Visibility = Visibility.Hidden;
                        GridCenas.Visibility = Visibility.Hidden;
                    break;
                case 3: GridCenas.Visibility = Visibility.Visible;
                        GridCocktail.Visibility = Visibility.Hidden;
                        GridCoffeBreak.Visibility = Visibility.Hidden;
                    break;
                default:
                    GridCenas.Visibility = Visibility.Hidden;
                    GridCocktail.Visibility = Visibility.Hidden;
                    GridCoffeBreak.Visibility = Visibility.Hidden;
                    break;

            }
        }

        //Calcular el valor total del contrato cuando se selecciona el personal adicional.
        private void CalculoAutoPerAdicional()
        {
            //Valorizador val = new Valorizador();

            //if (txtNumAsistentes.Text != string.Empty && cboModalidadServicio.SelectedIndex != -1 && ValidarAsistentes() == true
            //    && txtValorBase.Text != string.Empty)
            //{
            //    float ValorBase = float.Parse(txtValorBase.Text);
            //    int Asistentes = int.Parse(txtNumAsistentes.Text);
            //    int PerAdicional;

            //    if (txtPersonalAdicional.Text != string.Empty)
            //    {
            //        int.TryParse(txtPersonalAdicional.Text, out int valorx);

            //        if (valorx >= 2 && valorx <= 10 && valorx != 1)
            //        {
            //            PerAdicional = int.Parse(txtPersonalAdicional.Text);
            //            float ValorTotal = val.CalcularValorEvento(ValorBase, Asistentes, PerAdicional);

            //            txtValorTotal.Text = ValorTotal.ToString();
            //        }

            //    }
            //    else
            //    {
            //        PerAdicional = 0;
            //        float ValorTotal = val.CalcularValorEvento(ValorBase, Asistentes, PerAdicional);

            //        txtValorTotal.Text = ValorTotal.ToString();
            //    }
            //}

        }

        //Calcular valor total solo estando la cantidad de asistentes seleccionada.
        private void CalculoAutoAsistentes()
        {
            //Valorizador val = new Valorizador();

            //if (txtValorBase.Text != string.Empty && cboModalidadServicio.SelectedIndex != -1 && ValidarAsistentes() == true)
            //{
            //    float ValorBase = float.Parse(txtValorBase.Text);
            //    int Asistentes = int.Parse(txtNumAsistentes.Text);
            //    int PerAdicional;

            //    if (txtPersonalAdicional.Text != string.Empty)
            //    {
            //        int.TryParse(txtPersonalAdicional.Text, out int valorx);

            //        if (valorx >= 2 && valorx <= 10 && valorx != 1)
            //        {
            //            PerAdicional = int.Parse(txtPersonalAdicional.Text);
            //            //float ValorTotal = val.CalcularValorEvento(ValorBase, Asistentes, PerAdicional);

            //            txtValorTotal.Text = ValorTotal.ToString();
            //        }

            //    }
            //    else
            //    {
            //        PerAdicional = 0;
            //        //float ValorTotal = val.CalcularValorEvento(ValorBase, Asistentes, PerAdicional);

            //        txtValorTotal.Text = ValorTotal.ToString();
            //    }
            //}
        }

        //Valida que todos los campos del contrato estén llenos.
        private bool ContratoLleno()
        {
            if (txtNombreEmpresa.Text != string.Empty &&
            txtRutCon.Text != string.Empty &&
            txtNumAsistentes.Text != string.Empty &&
            dpFechaInicio.SelectedDate.Value != null &&
            dpFechaTermino.SelectedDate.Value != null &&
            txtObservaciones.Text != string.Empty &&
            cboModalidadServicio.SelectedIndex != -1 &&
            cboTipoEvento.SelectedIndex != -1 &&
            txtValorBase.Text != string.Empty &&
            txtValorTotal.Text != string.Empty &&
            txtPersonalBase.Text != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }



        }
        //Carga textbox de modalidad de servicio
        private void CargarModalidades()
        {
            int Tipo = ConvertirTipo(cboTipoEvento.SelectedIndex);
            ModalidadServicio modalidad = new ModalidadServicio();
            cboModalidadServicio.Items.Add("Seleccione");
            foreach (var flash in modalidad.ListarModalidades(Tipo))
            {
                cboModalidadServicio.Items.Add(flash.Nombre);

            }
            cboModalidadServicio.SelectedIndex = 0;
        }


        //AGREGAR UN CONTRATOS.
        private async void BT_AgregarCon_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            Contrato contrato = new Contrato();
            ModalidadServicio modalidad = new ModalidadServicio();
            TipoEvento tipo = new TipoEvento();
            CoffeeBreak cof = new CoffeeBreak();
            Cenas cen = new Cenas();
            Cocktail coc = new Cocktail();


            if (ContratoLleno() == true)
            {

                string NombreModalidad = (cboModalidadServicio.SelectedItem.ToString());

                //Se utiliza método para buscar la modalidad seleccionada según el comboBox
                modalidad = modalidad.BuscarModalidadNueva(NombreModalidad);

                //Se utiliza método para buscar el tipo de empresa seleccionada según el comboBox
                int TipoSeleccion = ConvertirTipo(cboTipoEvento.SelectedIndex);
                tipo = tipo.BuscarTipoEvento(TipoSeleccion);
                //Concatenar fecha y hora de inicio del evento

                //Capturar fechas del evento Inicio/termino
                DateTime FechaHoraInicio = Convert.ToDateTime(dpFechaInicio.SelectedDate.Value);
                DateTime FechaHoraTermino = Convert.ToDateTime(dpFechaTermino.SelectedDate.Value);




                contrato.NumeroContrato = DateTime.Now.ToString("yyyyMMddHHmm");
                contrato.Creacion = Convert.ToDateTime(DateTime.Now);
                contrato.Termino = DateTime.MaxValue;
                contrato.RutCliente = txtRutCon.Text;
                contrato.Modalidad = modalidad;
                contrato.Tipo = tipo;
                contrato.FechaHoraInicio = FechaHoraInicio;
                contrato.FechaHoraTermino = FechaHoraTermino;

                if (txtPersonalAdicional.Text != string.Empty)
                {
                    contrato.PersonalAdicional = int.Parse(txtPersonalAdicional.Text);
                }
                else
                {
                    contrato.PersonalAdicional = 0;
                }
                contrato.Asistentes = int.Parse(txtNumAsistentes.Text);

                contrato.Realizado = false;
                contrato.ValorTotalContrato = float.Parse(txtValorTotal.Text);
                contrato.Observaciones = txtObservaciones.Text;

                


                if (contrato.Create(contrato) == true)
                {
                    //LLENA TABLA COFFEBREAK
                    if (cboTipoEvento.SelectedIndex == 1)
                    {
                        bool veg;

                        if (ckVegetariana.IsChecked == true)
                        {
                            veg = true;

                        }
                        else
                        {
                            veg = false;
                        }

                        cof.Numero = contrato.NumeroContrato;
                        cof.Vegetariana = veg;
                        cof.Create(cof);
                    } else if (cboTipoEvento.SelectedIndex == 2)
                    //LLENA TABLA COCKTAIL
                    {

                        bool ambientacion = false;
                        int tipoambientacion = 10;
                        bool musicaambiente = false;
                        bool musicacliente = false;

                        if (ckAmbientacion.IsChecked == true)
                        {
                            ambientacion = true;
                        }
                        else
                        {
                            ambientacion = false;
                        }

                        if (rbBasica.IsChecked == true)
                        {
                            tipoambientacion = 10;
                        }
                        else if  (rbPer.IsChecked == true)
                        {
                            tipoambientacion = 20;
                        }

                        if (ckMusicaAmb.IsChecked == true)
                        {
                            musicaambiente = true;
                        }
                        else
                        {
                            musicaambiente = false;
                        }

                        if (ckMusicaCli.IsChecked == true)
                        {
                            musicacliente = true;
                        }
                        else
                        {
                            musicacliente = false;
                        }


                        coc.Numero = contrato.NumeroContrato;
                        coc.IdTipoAmbientacion = tipoambientacion;
                        coc.Ambientacion = ambientacion;
                        coc.MusicaAmbiental = musicaambiente;
                        coc.MusicaCliente = musicacliente;
                        coc.Create(coc);
                    } else if (cboTipoEvento.SelectedIndex == 3)
                        //LLENA TABLA CENAS
                    {
                        int tipoambientacioncena = 0;
                        bool musicaambientecena = false;
                        bool LocalCena = false;
                        bool OtroLocal = false;
                        double ValorLocalCena = 0;

                        if (ckMusicaCen.IsChecked == true)
                        {
                            musicaambientecena = true;
                        }
                        else { musicaambientecena = false;
                        }

                        if (rbBasicaCen.IsChecked == true)
                        {
                            tipoambientacioncena = 10;
                        }
                        else if (rbPerCen.IsChecked == true)
                        {
                            tipoambientacioncena = 20;
                        }
                        if (rbLocalCen.IsChecked == true)
                        {
                            LocalCena = true;
                            ValorLocalCena = Convert.ToDouble(txtValorArriendo.Text);
                        }
                        else
                        {
                            LocalCena = false;
                        }
                        if (rbOtroLocalCen.IsChecked == true)
                        {
                            OtroLocal = true;
                        }
                        else
                        {
                            OtroLocal = false;
                        }

                        cen.Numero = contrato.NumeroContrato;
                        cen.IdTipoAmbientacion = tipoambientacioncena;
                        cen.LocalOnBreak = LocalCena;
                        cen.OtroLocalOnBreak = OtroLocal;
                        cen.MusicaAmbiental = musicaambientecena;
                        cen.ValorArriendo = ValorLocalCena;
                        cen.Create(cen);




                    }


                    await this.ShowMessageAsync("Informacion", "Contrato agregado correctamente.");
                    LimpiarContrato();
                }
                else
                {
                    await this.ShowMessageAsync("Informacion", "No se pudo agregar el contrato");

                }
            }
            else
            {
                await this.ShowMessageAsync("Advertencia", "Para agregar un contrato debe llenar todos los datos del formulario.");
            }


        }


        //Calcula el valor total a medida que se van agregando los valores al textbox de numero de asistentes
        private void TB_NumAsistentes_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculoAutoAsistentes();
        }

        //Calcula el valor total a medida que se van agregando los valores al textbox de Personal adicional
        private void TB_PersonalAdicional_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculoAutoPerAdicional();
        }

        //método para mostrar el valor base, personal base cuando se seleccione una modalidad y en caso de que la cant de
        //asistentes Y/o personal adicional esten seleccionados calcula el valor total.
        private void CB_ModalidadServicio_LostFocus(object sender, RoutedEventArgs e)
        {
            ModalidadServicio modalidad = new ModalidadServicio();
            string NombreModalidad = cboModalidadServicio.SelectedItem.ToString();
            modalidad = modalidad.BuscarModalidadNueva(NombreModalidad);
            try
            {
                txtValorBase.Text = modalidad.ValorBase.ToString();
                txtPersonalBase.Text = modalidad.PersonalBase.ToString();
            }
            catch (Exception ex)
            {
                string z = ex.ToString();
            }
            //Calcula el valor total en caso de que llenen primero los textbox de personal adicional y asistentes.
            CalculoAutoPerAdicional();
            CalculoAutoAsistentes();
        }



        //Realiza consulta del rut que se va agregando en el textBox para luego mostrar el nombre del rut ingresado.
        private void TB_RutCon_TextChanged(object sender, TextChangedEventArgs e)
        {
            Cliente cli = new Cliente();
            if (txtRutCon.Text != string.Empty)
            {
                if (cli.ReadByRut(txtRutCon.Text) != null)
                {
                    cli = cli.ReadByRut(txtRutCon.Text);
                    txtNombreEmpresa.Text = cli.RazonSocial;
                }
                else
                {
                    txtNombreEmpresa.Text = string.Empty;
                }
            }
            else
            {
                txtNombreEmpresa.Text = string.Empty;
            }

        }



        private void DgContratos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DgContratos_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgContratos.IsReadOnly = true;
            GridContratos();
        }

        private void BtnBuscarContratoLis_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BT_LimpiarFiltroLisCon_Click(object sender, RoutedEventArgs e)
        {
            CargarTabla();
        }

        //Valida si la cantidad de asistentes está dentro de los rangos cuando se cambie de textBox.
        private async void TB_NumAsistentes_LostFocus(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtNumAsistentes.Text, out int valor);
            if (valor >= 1 && valor <= 300)
            {

            }
            else
            {
                if (txtNumAsistentes.Text != string.Empty)
                {
                    await this.ShowMessageAsync("Advertencia", "Cantidad de asistentes fuera de los rangos.");
                    txtNumAsistentes.Text = "";
                }

            }
        }
        //Valida si el personal adicional dentro de los rangos cuando se cambie de textBox.
        private async void TB_PersonalAdicional_LostFocus(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtPersonalAdicional.Text, out int valor);
            if (valor >= 2 && valor <= 10 && valor != 1)
            {

            }
            else
            {
                if (txtPersonalAdicional.Text != string.Empty)
                {
                    await this.ShowMessageAsync("Advertencia", "Cantidad de personal adicional fuera de los rangos.");
                    txtPersonalAdicional.Text = "";
                }

            }
        }

        //Calcula el valor total en caso que se ingresen primeros los valores de personal adicional y asistentes antes
        //que se seleccione el tipo y modalidad de servicio.
        private void CB_ModalidadServicio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculoAutoAsistentes();
            CalculoAutoPerAdicional();
        }


        //Permite solo números enteros en el TextBox.
        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9-]+").IsMatch(e.Text);
        }

        private void OnlyNumbersNumCon(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        //Actualizar contrato
        private async void BT_ActualizarCon_Click(object sender, RoutedEventArgs e)
        {
            if (ContratoLleno() == true)
            {
                ModalidadServicio modalidad = new ModalidadServicio();
                TipoEvento tipo = new TipoEvento();

                string NombreModalidad = (cboModalidadServicio.SelectedItem.ToString());

                //Se utiliza método para buscar la modalidad seleccionada según el comboBox
                //Se utiliza método para buscar la modalidad seleccionada según el comboBox
                modalidad = modalidad.BuscarModalidadNueva(NombreModalidad);

                //Se utiliza método para buscar el tipo de evento seleccionada según el comboBox
                int TipoSeleccion = ConvertirTipo(cboTipoEvento.SelectedIndex);
                tipo = tipo.BuscarTipoEvento(TipoSeleccion);
                //Concatenar fecha y hora de inicio del evento
                DateTime FechaHoraInicio = Convert.ToDateTime(dpFechaInicio.SelectedDate.Value);
                DateTime FechaHoraTermino = Convert.ToDateTime(dpFechaTermino.SelectedDate.Value);

                Contrato contrato = new Contrato();


                contrato.Termino = FechaHoraTermino;

                contrato.Modalidad = modalidad;
                contrato.Tipo = tipo;
                contrato.FechaHoraInicio = FechaHoraInicio;
                contrato.FechaHoraTermino = FechaHoraTermino;


                contrato.NumeroContrato = txtNumContrato.Text;
                contrato.RutCliente = contrato.RutCliente;
                contrato.Creacion = contrato.Creacion;
                contrato.Realizado = false;



                if (txtPersonalAdicional.Text != string.Empty)
                {
                    contrato.PersonalAdicional = int.Parse(txtPersonalAdicional.Text);
                }
                else
                {
                    contrato.PersonalAdicional = 0;
                }
                contrato.Asistentes = int.Parse(txtNumAsistentes.Text);


                contrato.ValorTotalContrato = float.Parse(txtValorTotal.Text);
                contrato.Observaciones = txtObservaciones.Text;

                if (contrato.Update())
                {
                    await this.ShowMessageAsync("Información", "Contrato actualizado correctamente");
                    LimpiarContrato();
                }
                else
                {
                    await this.ShowMessageAsync("Información", "No se logró actualizar Contrato");
                }

            }
            else
            {
                await this.ShowMessageAsync("Advertencia", "Para actualizar un contrato deben estár todos los campos llenos");
            }



        }
        //Consultar contrato.
        private async void BT_BuscarNumeroCon_Click(object sender, RoutedEventArgs e)
        {
            Contrato contrato = new Contrato();
            ModalidadServicio modalidad = new ModalidadServicio();
            if (contrato.Read(txtNumContrato.Text) != null)
            {

                contrato = contrato.Read(txtNumContrato.Text);
                txtRutCliente.Text = contrato.RutCliente;
                cboTipoEvento.SelectedIndex = ConvertirTipoInverso(contrato.Tipo.Id);

                txtNumAsistentes.Text = contrato.Asistentes.ToString();
                txtPersonalAdicional.Text = contrato.PersonalAdicional.ToString();
                txtObservaciones.Text = contrato.Observaciones;
                dpFechaInicio.SelectedDate = contrato.FechaHoraInicio;
                dpFechaTermino.SelectedDate = contrato.FechaHoraTermino;
                txtCreacion.Text = contrato.Creacion.ToString("dd/MM/yyyy HH:mm");
                txtTermino.Text = contrato.Termino.ToString("dd/MM/yyyy HH:mm");
                txtRutCon.Text = contrato.RutCliente;
                txtValorTotal.Text = contrato.ValorTotalContrato.ToString();
                string NombreModalidad = cboModalidadServicio.SelectedItem.ToString();
                modalidad = modalidad.BuscarModalidadNueva(contrato.Modalidad.Nombre);
                try
                {
                    txtValorBase.Text = modalidad.ValorBase.ToString();
                    txtPersonalBase.Text = modalidad.PersonalBase.ToString();

                }
                catch (Exception ex)
                {
                    string hola = ex.ToString();
                }

                cboModalidadServicio.SelectedItem = contrato.Modalidad.Nombre;


                //----------------------------------------------------//
                if (contrato.Realizado == true)
                {
                    txtEstado.Text = "Realizado";
                }
                else
                {
                    if (contrato.Realizado == false && txtTermino.Text == "31/12/9999 23:59")
                    {
                        txtEstado.Text = "Vigente";
                    }
                    else if (contrato.Realizado == false && txtTermino.Text == "31-12-9999 23:59")
                    {
                        txtEstado.Text = "Vigente";
                    }
                    else
                    {
                        txtEstado.Text = "Cancelado";
                    }

                }



            }
            else
            {
                await this.ShowMessageAsync("Información", "No se encontró contrato registrado en la base de datos");
            }


        }

        private async void btoCancerlar_Click(object sender, RoutedEventArgs e)
        {

            if (txtEstado.Text == "Realizado")
            {
                await this.ShowMessageAsync("Información", "El contrato ya fue realizado");

            }
            else
            {
                if (txtEstado.Text == "Cancelado")
                {
                    await this.ShowMessageAsync("Información", "El contrato ya fue cancelado");
                }
                else
                {

                    MessageDialogResult resultado = await this.ShowMessageAsync("Confirmación", "¿Estás seguro que deseas cancelar este contrato? "
                    , MessageDialogStyle.AffirmativeAndNegative);

                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        if (ContratoLleno() == true)
                        {
                            ModalidadServicio modalidad = new ModalidadServicio();
                            TipoEvento tipo = new TipoEvento();

                            string NombreModalidad = (cboModalidadServicio.SelectedItem.ToString());

                            modalidad = modalidad.BuscarModalidadNueva(NombreModalidad);
                            int TipoSeleccion = ConvertirTipo(cboTipoEvento.SelectedIndex);
                            tipo = tipo.BuscarTipoEvento(TipoSeleccion);
                            DateTime FechaHoraInicio = Convert.ToDateTime(dpFechaInicio.SelectedDate.Value);
                            DateTime FechaHoraTermino = Convert.ToDateTime(dpFechaTermino.SelectedDate.Value);

                            Contrato contrato = new Contrato();


                            contrato.Termino = DateTime.Now;

                            contrato.Modalidad = modalidad;
                            contrato.Tipo = tipo;
                            contrato.FechaHoraInicio = FechaHoraInicio;
                            contrato.FechaHoraTermino = FechaHoraTermino;


                            contrato.NumeroContrato = txtNumContrato.Text;
                            contrato.RutCliente = contrato.RutCliente;
                            contrato.Creacion = contrato.Creacion;
                            contrato.Realizado = false;



                            if (txtPersonalAdicional.Text != string.Empty)
                            {
                                contrato.PersonalAdicional = int.Parse(txtPersonalAdicional.Text);
                            }
                            else
                            {
                                contrato.PersonalAdicional = 0;
                            }
                            contrato.Asistentes = int.Parse(txtNumAsistentes.Text);


                            contrato.ValorTotalContrato = float.Parse(txtValorTotal.Text);
                            contrato.Observaciones = txtObservaciones.Text;

                            if (contrato.Update())
                            {
                                await this.ShowMessageAsync("Información", "Contrato cancelado correctamente");

                            }
                            else
                            {
                                await this.ShowMessageAsync("Información", "No se logró cancelar el Contrato");
                            }
                        }


                    }
                }




            }



        }

        private async void btoTerminar_Click(object sender, RoutedEventArgs e)
        {

            if (txtEstado.Text == "Cancelado")
            {

                await this.ShowMessageAsync("Información", "Este contrato fue cancelado anteriormente.");

            }
            else
            {

                if (txtEstado.Text == "Realizado")
                {
                    await this.ShowMessageAsync("Información", "Este contrato ya fue realizado.");
                }
                else
                {
                    if (ContratoLleno() == true)
                    {
                        ModalidadServicio modalidad = new ModalidadServicio();
                        TipoEvento tipo = new TipoEvento();

                        string NombreModalidad = (cboModalidadServicio.SelectedItem.ToString());

                        modalidad = modalidad.BuscarModalidadNueva(NombreModalidad);


                        int TipoSeleccion = ConvertirTipo(cboTipoEvento.SelectedIndex);
                        tipo = tipo.BuscarTipoEvento(TipoSeleccion);
                        DateTime FechaHoraInicio = Convert.ToDateTime(dpFechaInicio.SelectedDate.Value);
                        DateTime FechaHoraTermino = Convert.ToDateTime(dpFechaTermino.SelectedDate.Value);

                        Contrato contrato = new Contrato();


                        contrato.Termino = DateTime.Now;

                        contrato.Modalidad = modalidad;
                        contrato.Tipo = tipo;
                        contrato.FechaHoraInicio = FechaHoraInicio;
                        contrato.FechaHoraTermino = FechaHoraTermino;


                        contrato.NumeroContrato = txtNumContrato.Text;
                        contrato.RutCliente = contrato.RutCliente;
                        contrato.Creacion = contrato.Creacion;
                        contrato.Realizado = true;



                        if (txtPersonalAdicional.Text != string.Empty)
                        {
                            contrato.PersonalAdicional = int.Parse(txtPersonalAdicional.Text);
                        }
                        else
                        {
                            contrato.PersonalAdicional = 0;
                        }
                        contrato.Asistentes = int.Parse(txtNumAsistentes.Text);


                        contrato.ValorTotalContrato = float.Parse(txtValorTotal.Text);
                        contrato.Observaciones = txtObservaciones.Text;

                        if (contrato.Update())
                        {
                            await this.ShowMessageAsync("Información", "Contrato terminado correctamente");

                        }
                        else
                        {
                            await this.ShowMessageAsync("Información", "No se logró terminar el Contrato");
                        }

                    }
                }

            }


        }

        //------------------------------------------------------LISTADO DE CONTRATOS---------------------------------------------//
        //------------------------------------------------------LISTADO DE CONTRATOS---------------------------------------------//

        private void TL_ListaCon_Click(object sender, RoutedEventArgs e)
        {
            Height = 603.4;
            conAux = false;
            Width = 1116.732;
            Title = "OnBreak - Listado de Contratos";
            TituloListadoCon.Content = "Listado de Contratos";
            FlyListadoContratos.IsOpen = true;
            btnSalirLisCon.Visibility = Visibility.Hidden;
            GridInicio.Visibility = Visibility.Hidden;
            GridAdmCli.Visibility = Visibility.Hidden;
            GridAdmContratos.Visibility = Visibility.Hidden;
            LimpiarContrato();
            LimpiarCliente();
            dgContratos.Visibility = Visibility.Visible;
            CargarTabla();

        }


        private void CargarTiposEventosLista()
        {
            TipoEvento tipo = new TipoEvento();
            cboTipoEventoLista.Items.Add("Seleccione");

            foreach (var flash in tipo.ListarTiposEventos())
            {
                cboTipoEventoLista.Items.Add(flash.Descripcion);

            }
            cboTipoEventoLista.SelectedIndex = 0;
        }


        public string SeleccionarNumContrato()
        {
            List<dynamic> contratos = new List<dynamic>();
            Contrato contrato = new Contrato();
            foreach (var flash in contrato.ReadAll())
            {
                var objContrato = new
                {
                    NumerodeContrato = flash.NumeroContrato,
                    Creacion = flash.Creacion,
                    Termino = flash.Termino,
                    RutdelCliente = flash.RutCliente,
                    Modalidad = flash.Modalidad.Nombre,
                    Tipo = flash.Tipo.Descripcion,
                    FechayHoradeInicio = flash.FechaHoraInicio,
                    FechayHoradeTermino = flash.FechaHoraTermino,
                    Asistentes = flash.Asistentes,
                    PersonalAdicional = flash.PersonalAdicional,
                    Realizado = flash.Realizado,
                    ValorTotaldelContrato = flash.ValorTotalContrato,
                    Observaciones = flash.Observaciones
                };
                contratos.Add(objContrato);
            }
            //Asigna la lista al dataGrid.
            dgContratos.ItemsSource = contratos;


            int i = 0;
            if (dgContratos.SelectedIndex != -1)
            {
                i = dgContratos.SelectedIndex;

                if (i >= 0)
                {
                    string NumContrato;
                    NumContrato = contratos[i].NumerodeContrato;
                    return NumContrato;

                }

            }
            return null;
        }


        private void DgContratos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SeleccionarNumContrato() != null)
            {
                if (conAux == true)
                {
                    string numero = SeleccionarNumContrato();



                    ModalidadServicio modalidad = new ModalidadServicio();
                    Contrato contrato = new Contrato();

                    contrato = contrato.Read(numero);


                    cboTipoEvento.SelectedIndex = ConvertirTipoInverso(contrato.Tipo.Id);

                    dpFechaInicio.SelectedDate = contrato.FechaHoraInicio;

                    txtNumContrato.Text = contrato.NumeroContrato;
                    dpFechaTermino.SelectedDate = contrato.FechaHoraTermino;

                    txtNumAsistentes.Text = contrato.Asistentes.ToString();
                    txtPersonalAdicional.Text = contrato.PersonalAdicional.ToString();
                    txtObservaciones.Text = contrato.Observaciones;
                    txtCreacion.Text = contrato.Creacion.ToString("dd/MM/yyyy HH:mm");
                    txtTermino.Text = contrato.Termino.ToString("dd/MM/yyyy HH:mm");
                    //chkRealizado.IsChecked = contrato.Realizado;
                    txtRutCon.IsReadOnly = true;
                    txtRutCon.Text = contrato.RutCliente;
                    txtValorTotal.Text = contrato.ValorTotalContrato.ToString();
                    string NombreModalidad = cboModalidadServicio.SelectedItem.ToString();
                    modalidad = modalidad.BuscarModalidadNueva(contrato.Modalidad.Nombre);
                    cboModalidadServicio.SelectedItem = contrato.Modalidad.Nombre;
                    try
                    {
                        txtValorBase.Text = modalidad.ValorBase.ToString();
                        txtPersonalBase.Text = modalidad.PersonalBase.ToString();

                    }
                    catch (Exception ex)
                    {
                        string hola = ex.ToString();
                    }




                    //----------------------------------------------------//
                    if (contrato.Realizado == true)
                    {
                        txtEstado.Text = "Realizado";
                    }
                    else
                    {
                        if (contrato.Realizado == false && txtTermino.Text == "31/12/9999 23:59")
                        {
                            txtEstado.Text = "Vigente";
                        }
                        else if (contrato.Realizado == false && txtTermino.Text == "31-12-9999 23:59")
                        {
                            txtEstado.Text = "Vigente";
                        }
                        else
                        {
                            txtEstado.Text = "Cancelado";
                        }

                    }



                    FlyListadoContratos.IsOpen = false;
                }
            }
        }


        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            FlyListadoContratos.IsOpen = true;
            TituloListadoCon.Content = "Listado de contrato";
            Title = "OnBreak - Listado de contratos Aux";
            conAux = true;
            btnSalirLisCon.Visibility = Visibility.Visible;
            dgContratos.Visibility = Visibility.Visible;
            CargarTabla();



        }

        private void BtnSalirLisCon_Click(object sender, RoutedEventArgs e)
        {
            FlyListadoContratos.IsOpen = false;
            TituloListadoCli.Content = "Administracion de contratos";
            Title = "OnBreak - Administracion de contratos";
            conAux = false;
            LimpiarContrato();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string num = SeleccionarNumContrato();
            MessageBox.Show(num);
        }

        private void txtRutCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnBuscarCliente_Click(this, new RoutedEventArgs());
            }
        }

        private void txtNumContrato_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BT_BuscarNumeroCon_Click(this, new RoutedEventArgs());
                txtRutCon.IsReadOnly = true;
                txtEstado.IsReadOnly = true;
                txtTermino.IsReadOnly = true;
                txtCreacion.IsReadOnly = true;
            }

        }

        //Aplicar filtros a la lista de contratos.
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Contrato contrato = new Contrato();
            List<dynamic> contratos = new List<dynamic>();
            int tipo = 0;
            tipo = cboTipoEventoLista.SelectedIndex;
            tipo = TipoEmpSeleccion(tipo);


            foreach (var flash in contrato.ReadAll(txtNumContratoLis.Text, txtRutContrato.Text, "", tipo))
            {
                var objContrato = new
                {
                    NumerodeContrato = flash.NumeroContrato,
                    Creacion = flash.Creacion,
                    Termino = flash.Termino,
                    RutdelCliente = flash.RutCliente,
                    Modalidad = flash.Modalidad.Nombre,
                    Tipo = flash.Tipo.Descripcion,
                    FechayHoradeInicio = flash.FechaHoraInicio,
                    FechayHoradeTermino = flash.FechaHoraTermino,
                    Asistentes = flash.Asistentes,
                    PersonalAdicional = flash.PersonalAdicional,
                    ValorTotaldelContrato = flash.ValorTotalContrato,
                    Observaciones = flash.Observaciones
                };
                contratos.Add(objContrato);
            }
            dgContratos.ItemsSource = contratos;




        }


        public int TipoEmpSeleccion(int numero)
        {
            switch (numero)
            {
                case 1: return 10;
                case 2: return 20;
                case 3: return 30;
                case 4: return 40;
                case 5: return 50;
            }
            return 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FlyListadoClientes.IsOpen = true;
            TituloListadoCli.Content = "Listado de clientes";
            Title = "OnBreak - Listado de clientes Aux";
            cliAux = true;
            DgClientes.Visibility = Visibility.Visible;
            CargarTabla();
        }


        private void DgClientes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cliAux == true)
                {
                    ClienteA c1i = (ClienteA)DgClientes.SelectedItem;
                    txtRutCliente.IsReadOnly = true;
                    txtRutCliente.Text = c1i.RutCliente;
                    txtNombreCliente.Text = c1i.NombreContacto;
                    txtDireccionCli.Text = c1i.Direccion;
                    txtRazonSocialCli.Text = c1i.RazonSocial;
                    txtEmailCli.Text = c1i.MailContacto;
                    txtTelefonoCli.Text = c1i.Telefono;
                    cboTipoEmpresaCli.SelectedValue = c1i.IdTipoEmpresa;
                    cboActividadEmpCli.SelectedValue = c1i.IdActividadEmpresa;

                    FlyListadoClientes.IsOpen = false;
                }
            }
            catch (Exception)
            {


            }

        }

        private void btoFiltrarCli_Button(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();

            cli.IdTipoEmpresa = Convert.ToInt32(cboTipoEmpresaLis.SelectedValue);
            cli.IdActividadEmpresa = Convert.ToInt32(cboActividadEmpresaLis.SelectedValue);
            try
            {
                if (cboTipoEmpresaLis.SelectedIndex != -1)
                {
                    var Resultado2 = from Cliente in cli.ReadAll()
                                     where Cliente.IdTipoEmpresa == Convert.ToInt32(cboTipoEmpresaLis.SelectedValue)
                                     select Cliente;

                    var R = ClienteA.CargarGrid(Resultado2);

                    DgClientes.ItemsSource = null;
                    DgClientes.ItemsSource = R;
                }

                if (cboActividadEmpresaLis.SelectedIndex != -1)
                {
                    var Resultado2 = from Cliente in cli.ReadAll()
                                     where Cliente.IdActividadEmpresa == Convert.ToInt32(cboActividadEmpresaLis.SelectedValue)
                                     select Cliente;

                    var R = ClienteA.CargarGrid(Resultado2);

                    DgClientes.ItemsSource = null;
                    DgClientes.ItemsSource = R;
                }

                if (cboActividadEmpresaLis.SelectedIndex != -1 && cboTipoEmpresaLis.SelectedIndex != -1)
                {
                    var Resultado2 = from Cliente in cli.ReadAll()
                                     where Cliente.IdActividadEmpresa == Convert.ToInt32(cboActividadEmpresaLis.SelectedValue)
                                     && Cliente.IdTipoEmpresa == Convert.ToInt32(cboTipoEmpresaLis.SelectedValue)
                                     select Cliente;

                    var R = ClienteA.CargarGrid(Resultado2);

                    DgClientes.ItemsSource = null;
                    DgClientes.ItemsSource = R;
                }
            }
            catch (Exception ex)
            {
                DgClientes.ItemsSource = ClienteA.CargarGridBase();
            }

        }

        //CREAR RESPALDO 
        private async void BtnRespaldar_Click(object sender, RoutedEventArgs e)
        {
            RespaldarContrato();
            await this.ShowMessageAsync("Información", "Contrato respaldado");

        }


        private void RespaldoAutomatico(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 5, 0);

            timer.Tick += (s, a) =>
            {
                RespaldarContrato();
            };
            timer.Start();
        }


        private void RespaldarContrato()
        {
            Contrato contrato = new Contrato();
            ModalidadServicio modalidad = new ModalidadServicio();
            TipoEvento tipo = new TipoEvento();

            //Datos vacios



            if (cboTipoEvento.SelectedIndex == 0)
            {
                contrato.Tipo = null;
            }
            else
            {
                contrato.Tipo = tipo;
            }

            if (cboModalidadServicio.SelectedIndex == 0)
            {
                contrato.Modalidad = null;
            }
            else
            {
                contrato.Modalidad = modalidad;
            }

            if (txtRutCon.Text == string.Empty)
            {
                contrato.RutCliente = "";
            }
            else
            {
                contrato.RutCliente = txtRutCon.Text;
            }

            if (dpFechaInicio.SelectedDate == null)
            {
                contrato.FechaHoraInicio = DateTime.MaxValue;
            }
            else
            {
                DateTime FechaHoraInicio = Convert.ToDateTime(dpFechaInicio.SelectedDate.Value);
                contrato.FechaHoraInicio = FechaHoraInicio;
            }

            if (dpFechaTermino.SelectedDate == null)
            {
                contrato.FechaHoraTermino = DateTime.MaxValue;
            }
            else
            {
                DateTime FechaHoraTermino = Convert.ToDateTime(dpFechaTermino.SelectedDate.Value);
                contrato.FechaHoraTermino = FechaHoraTermino;
            }

            if (txtObservaciones.Text == string.Empty)
            {
                contrato.Observaciones = "";
            }
            else
            {
                contrato.Observaciones = txtObservaciones.Text;
            }

            if (txtNumAsistentes.Text == string.Empty)
            {
                contrato.Asistentes = 0;
            }
            else
            {
                contrato.Asistentes = int.Parse(txtNumAsistentes.Text);
            }

            if (txtPersonalAdicional.Text == string.Empty)
            {
                contrato.PersonalAdicional = 0;
            }
            else
            {
                contrato.PersonalAdicional = int.Parse(txtPersonalAdicional.Text);
            }

            if (txtValorTotal.Text==string.Empty)
            {
                contrato.ValorTotalContrato = 0;
            }
            else
            {
                contrato.ValorTotalContrato = float.Parse(txtValorTotal.Text);
            }

            string NombreModalidad = (cboModalidadServicio.SelectedItem.ToString());

            //Se utiliza método para buscar la modalidad seleccionada según el comboBox
            modalidad = modalidad.BuscarModalidadNueva(NombreModalidad);

            //Se utiliza método para buscar el tipo de empresa seleccionada según el comboBox
            int TipoSeleccion = ConvertirTipo(cboTipoEvento.SelectedIndex);
            tipo = tipo.BuscarTipoEvento(TipoSeleccion);
            //Concatenar fecha y hora de inicio del evento

            //Capturar fechas del evento Inicio/termino
            contrato.NumeroContrato = "";
            contrato.Creacion = DateTime.MaxValue;
            contrato.Termino = DateTime.MaxValue;
            contrato.Modalidad = modalidad;
            contrato.Tipo = tipo;
            contrato.Realizado = false;

            new Caretaker().RespaldarContrato(contrato);

        }

        public void RestaurarRespaldo()
        {
            try
            {
                Contrato contrato = new Caretaker().RecuperarContrato();
                CargarContrato(contrato);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void CargarContrato(Contrato con)
        {
            ModalidadServicio modalidad = new ModalidadServicio();

            if (con.Tipo != null)
            {
                cboTipoEvento.SelectedIndex = ConvertirTipoInverso(con.Tipo.Id);
            }
            else
            {
                cboTipoEvento.SelectedIndex = 0;
            }

            txtNumAsistentes.Text = con.Asistentes.ToString();
            txtPersonalAdicional.Text = con.PersonalAdicional.ToString();
            txtObservaciones.Text = con.Observaciones;
            dpFechaInicio.SelectedDate = con.FechaHoraInicio;
            dpFechaTermino.SelectedDate = con.FechaHoraTermino;
            txtCreacion.Text = con.Creacion.ToString("dd /MM/yyyy HH:mm");
            txtTermino.Text = con.Termino.ToString("dd/MM/yyyy HH:mm");
            txtRutCon.Text = con.RutCliente;
            txtValorTotal.Text = con.ValorTotalContrato.ToString();
            string NombreModalidad = cboModalidadServicio.SelectedItem.ToString();

            if (con.Modalidad != null)
            {
                modalidad = modalidad.BuscarModalidadNueva(con.Modalidad.Nombre);
                try
                {
                    txtValorBase.Text = modalidad.ValorBase.ToString();
                    txtPersonalBase.Text = modalidad.PersonalBase.ToString();

                }
                catch (Exception ex)
                {
                    string hola = ex.ToString();
                }

                cboModalidadServicio.SelectedItem = con.Modalidad.Nombre;
            }
            else
            {
                cboModalidadServicio.SelectedIndex = 0;
            }



            //----------------------------------------------------//
            if (con.Realizado == true)
            {
                txtEstado.Text = "Realizado";
            }
            else
            {
                if (con.Realizado == false && txtTermino.Text == "31/12/9999 23:59")
                {
                    txtEstado.Text = "Vigente";
                }
                else if (con.Realizado == false && txtTermino.Text == "31-12-9999 23:59")
                {
                    txtEstado.Text = "Vigente";
                }
                else
                {
                    txtEstado.Text = "Cancelado";
                }

            }

        }

        private void CkAmbientacion_Checked(object sender, RoutedEventArgs e)
        {
            if (ckAmbientacion.IsChecked == true)
            {
                rbPer.IsEnabled = true;
                rbBasica.IsEnabled = true;
            }


        }

        private void CkAmbientacion_Unchecked(object sender, RoutedEventArgs e)
        {
            rbPer.IsEnabled = false;
            rbPer.IsChecked = false;
            rbBasica.IsEnabled = false;
            rbBasica.IsChecked = false;
        }

        private void RbOtroLocalCen_Checked(object sender, RoutedEventArgs e)
        {
            txtValorArriendo.IsEnabled = true;
        }

        private void RbLocalCen_Checked(object sender, RoutedEventArgs e)
        {
            
            txtValorArriendo.IsEnabled = false;
            txtValorArriendo.Text = string.Empty;
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            Valorizador val = new Valorizador();
            try
            {
                int x = cboTipoEvento.SelectedIndex;
            int asistentes = int.Parse(txtNumAsistentes.Text);
            int perAdicional = int.Parse(txtPersonalAdicional.Text);
            int valorBase = int.Parse(txtValorBase.Text);
            int ambientacion;
            double musica;
            double local;
                switch (x)
                {
                    case 1:

                        double total = val.CalcularCoffeBreak(valorBase, asistentes, perAdicional);
                        txtValorTotal.Text = total.ToString();
                        break;
                    case 2:

                        if (rbBasica.IsChecked == true)
                        {
                            ambientacion = 2;
                        }
                        else if (rbPer.IsChecked == true)
                        {
                            ambientacion = 5;
                        }
                        else
                        {
                            ambientacion = 0;
                        }
                        if (ckMusicaAmb.IsChecked == true)
                        {
                            musica = 1;
                        }
                        else
                        {
                            musica = 0;
                        }
                        double totalCocktail = val.CalcularCocktail(valorBase, asistentes, perAdicional, ambientacion, musica);
                        txtValorTotal.Text = totalCocktail.ToString();
                        break;
                    case 3:
                        //Ambientación
                        if (rbBasicaCen.IsChecked == true)
                        {
                            ambientacion = 3;
                        }
                        else if (rbPerCen.IsChecked == true)
                        {
                            ambientacion = 5;
                        }
                        else
                        {
                            ambientacion = 0;
                        }
                        //Musica.
                        if (ckMusicaCen.IsChecked == true)
                        {
                            musica = 1.5;
                        }
                        else
                        {
                            musica = 0;
                        }
                        //local
                        if (rbLocalCen.IsChecked == true)
                        {
                            local = 9;
                        }
                        else
                        {
                            local = double.Parse(txtValorArriendo.Text);
                        }

                        double totalCenas = val.CalcularCenas(valorBase, asistentes, perAdicional, ambientacion, musica, local);
                        txtValorTotal.Text = totalCenas.ToString();
                        break;
                    default:

                        break;
                }
            }
            catch (Exception)
            {

                int x;
            }
            
        }
        private void Contraste()
        {
            if (z == true)
            {
                ThemeManager.AddAccent("AltoContraste", new Uri("pack://application:,,,/OnBreakWPF;component/Temas/AltoContraste.xaml"));
                Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current,
                                       ThemeManager.GetAccent("AltoContraste"),
                                       theme.Item1);
                LogosContraste();
            }
            else
            {
                ThemeManager.AddAccent("NormalTema", new Uri("pack://application:,,,/OnBreakWPF;component/Temas/Normal.xaml"));

                Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current,
                                       ThemeManager.GetAccent("NormalTema"),
                                       theme.Item1);
                LogosNormal();
            }

        }
        bool z = true;
        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            Contraste();
            z = false;
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            Contraste();
            z = true;
        }


        //logos
        private void LogosNormal()
        {
            LogoContraste.Visibility = Visibility.Hidden;
            LogoContraste4.Visibility = Visibility.Hidden;
            LogoContraste2.Visibility = Visibility.Hidden;
            LogoContraste3.Visibility = Visibility.Hidden;

            LogoNormal.Visibility = Visibility.Visible;
            LogoNormal2.Visibility = Visibility.Visible;
            LogoNornal3.Visibility = Visibility.Visible;
            LogoNormal4.Visibility = Visibility.Visible;
        }

        private void LogosContraste()
        {
            LogoContraste.Visibility = Visibility.Visible;
            LogoContraste4.Visibility = Visibility.Visible;
            LogoContraste2.Visibility = Visibility.Visible;
            LogoContraste3.Visibility = Visibility.Visible;

            LogoNormal.Visibility = Visibility.Hidden;
            LogoNormal2.Visibility = Visibility.Hidden;
            LogoNornal3.Visibility = Visibility.Hidden;
            LogoNormal4.Visibility = Visibility.Hidden;
        }

 



        private void BtnColores_Click(object sender, RoutedEventArgs e)
        {
            GridColores.Visibility = Visibility.Visible;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBlanco_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.AddAccent("AltoContraste", new Uri("pack://application:,,,/OnBreakWPF;component/Temas/AltoContraste.xaml"));
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                   ThemeManager.GetAccent("AltoContraste"),
                                   theme.Item1);
            LogosContraste();
            GridColores.Visibility = Visibility.Hidden;
        }

        private void BtnNormal_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.AddAccent("NormalTema", new Uri("pack://application:,,,/OnBreakWPF;component/Temas/Normal.xaml"));

            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                   ThemeManager.GetAccent("NormalTema"),
                                   theme.Item1);
            LogosNormal();
            GridColores.Visibility = Visibility.Hidden;
        }

        private void BtnVerde_Click(object sender, RoutedEventArgs e)
        {
            //verde
            ThemeManager.AddAccent("Verde", new Uri("pack://application:,,,/OnBreakWPF;component/Temas/Verde.xaml"));
            Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                   ThemeManager.GetAccent("Verde"),
                                   theme.Item1);
            LogosContraste();
            GridColores.Visibility = Visibility.Hidden;
        }
    }
}

 
