using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Windows.Threading;
using Patrones;
using OnBreak.Negocio;
using System.Xml.Serialization;
using System.IO;

namespace OnBreak.Wpf
{
    /// <summary>
    /// Lógica de interacción para AdminContrato.xaml
    /// </summary>
    public partial class AdminContrato : MetroWindow
    {
        public AdminContrato()
        {
            InitializeComponent();
            LimpiarVentana();
            IniciarCarpeta();
            this.Loaded += TiempoDisipador;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            {
                Application.Current.Shutdown();
            }

        }
        //--------------------------------------------------------------------------
        public bool Contraste;
        CareTaker Almacena = new CareTaker();
        private string Xml;
        public void TiempoDisipador(object sender, RoutedEventArgs e)
        {
            DispatcherTimer T = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            T.Tick += Cada_Ejecuta;
            T.Start();
        }
        private int Incrementa = 0;
        private int Cont = 300;
        private void Cada_Ejecuta(object sender, EventArgs e)
        {
            Incrementa++;
            if (Incrementa == Cont)
            {
                Cont = Cont + 300;
                GuardarInfo();
            }
        }
        private async void GuardarInfo()
        {
            try
            {
                Contrato con = new Contrato()
                {
                    Creacion = (DateTime)dtpFechaInicio.SelectedDate,
                    Termino = (DateTime)dtpFechaTermino.SelectedDate,
                    RutContacto = cbRut.SelectedValue.ToString(),
                    IdModalidad = cbModalidad.SelectedValue.ToString(),
                    IdTipoEvento = (int)cbTipoEvento.SelectedValue,
                    FechaHoraInicio = (DateTime)dtpFechaInicio.SelectedDate,
                    FechaHoraTermino = (DateTime)dtpFechaTermino.SelectedDate,
                    Asistentes = Convert.ToInt32(txtAsistentes.Text),
                    PersonalAdicional = Convert.ToInt32(txtPersonal.Text),
                    ValorTotalContrato = Convert.ToDouble(txtValor.Text),
                    Observaciones = txtObservaciones.Text.ToString()

                };
                XmlSerializer Serializador = new XmlSerializer(con.GetType());
                StringWriter Writer = new StringWriter();
                Serializador.Serialize(Writer, con);
                Xml = Writer.ToString();

                Carpeta();

                await this.ShowMessageAsync("Atencion", "Contrato Respaldado Exitosamente");

            }
            catch (Exception)
            {

                await this.ShowMessageAsync("Error", "...");
            }
        }
        private void IniciarCarpeta()
        {
            string path1 = @"C:\OnBreak\";
            string path2 = System.IO.Path.Combine(@"C:\OnBreak\Contrato"); ;


            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);

            }
            else
            {

            }
            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
                File.WriteAllText(System.IO.Path.Combine(path2, "Contrato.txt"), Xml);
            }
        }
        private void Carpeta()
        {
            File.WriteAllText(System.IO.Path.Combine(@"C:\OnBreak\Contrato\Contrato.txt"), Xml);
        }
        public async void RecuerdaInfo()
        {
            try
            {
                Contrato con = new Contrato();
                XmlSerializer Seria = new XmlSerializer(con.GetType());
                StreamReader ReadTxt = new StreamReader(@"C:\OnBreak\Contrato\Contrato.txt");
                con = (Contrato)Seria.Deserialize(ReadTxt);

                dtpFechaInicio.SelectedDate = con.Creacion;
                dtpFechaTermino.SelectedDate = con.Termino;
                cbRut.SelectedValue = con.RutContacto;
                cbModalidad.SelectedValue = con.IdModalidad;
                cbTipoEvento.SelectedValue = con.IdTipoEvento;
                dtpFechaInicio.SelectedDate = con.FechaHoraInicio;
                dtpFechaTermino.SelectedDate = con.FechaHoraTermino;
                txtAsistentes.Text = con.Asistentes.ToString();
                txtPersonal.Text = con.PersonalAdicional.ToString();
                txtValor.Text = con.ValorTotalContrato.ToString();
                txtObservaciones.Text = con.Observaciones.ToString();


            }
            catch (Exception)
            {

                await this.ShowMessageAsync("Error", "...");
            }
        }
        //private async void CheckSave()
        //{
        //    string path = @"C:\OnBreak\Contrato\Contrato.txt";
        //    if (Directory.Exists(path))
        //    {
        //        RecuerdaInfo();
        //    }
        //}
        private void LimpiarVentana()
        {
            txtNumero.Text = string.Empty;
            CargarRut();
            CargarTipos();
            dtpFechaInicio.SelectedDate = DateTime.Now;
            dtpFechaTermino.SelectedDate = DateTime.Now;
            txtPersonal.Text = "0";
            txtAsistentes.Text = "0";
            txtObservaciones.Text = string.Empty;

        }
        private void CargarRut()
        {
            cbRut.ItemsSource = new Contacto().ReadAll();
            cbRut.DisplayMemberPath = "RutContacto";
            cbRut.SelectedValuePath = "RutContacto";
            cbRut.SelectedIndex = 0;
            LeerRazon();
        }
        private void CargarTipos()
        {
            cbTipoEvento.ItemsSource = new TipoEventos().ReadAll();
            cbTipoEvento.DisplayMemberPath = "Descripcion";
            cbTipoEvento.SelectedValuePath = "IdTipoEvento";
            cbTipoEvento.SelectedIndex = 0;
            CargarModalidades();
        }
        private void CargarModalidades()
        {
            if ((cbTipoEvento.Items.Count != 0) && (cbTipoEvento.SelectedValue != null))
            {
                cbModalidad.ItemsSource = new ModalidadServicios().ReadAllByModalidad((int)cbTipoEvento.SelectedValue);
                cbModalidad.DisplayMemberPath = "Nombre";
                cbModalidad.SelectedValuePath = "IdModalidad";
                cbModalidad.SelectedIndex = 0;
            }
            else
            {
                cbModalidad.ItemsSource = null;
            }
        }
        //----------------------------------------------------------------------------------------
        private void BtnAltoContraste_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            if (Contraste)
            {
                mw.OffContraste(this);
                FlyoutStyle();
                Contraste = false;
            }
            else
            {
                mw.OnContraste(this);
                FlyoutStyle();
                Contraste = true;
            }
        }
        private async void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Contrato con = new Contrato()
            {
                Numero = txtNumero.Text
            };
            if (con.Read())
            {
                cbRut.SelectedValue = con.RutContacto;
                cbTipoEvento.SelectedValue = con.IdTipoEvento;
                cbModalidad.SelectedValue = con.IdModalidad;
                dtpFechaInicio.SelectedDate = con.FechaHoraInicio;
                dtpFechaTermino.SelectedDate = con.FechaHoraTermino;
                txtAsistentes.Text = con.Asistentes.ToString();
                txtPersonal.Text = con.PersonalAdicional.ToString();
                txtObservaciones.Text = con.Observaciones;
                MessageDialogResult result =
                    await this.ShowMessageAsync("Confirmación", "Contrato Encontrado", MessageDialogStyle.Affirmative);
            }
            else
            {
                MessageDialogResult result =
                    await this.ShowMessageAsync("Error", "No se pudo encontrar el contrato.", MessageDialogStyle.Affirmative);
            }
        }
        private async void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                if (await Confirmacion("Registrar"))
                {
                    Contrato con = new Contrato()
                    {
                        Numero = DateTime.Now.ToString("yyyyMMddhhmm"),
                        RutContacto = cbRut.SelectedValue.ToString(),
                        IdModalidad = cbModalidad.SelectedValue.ToString(),
                        IdTipoEvento = (int)cbTipoEvento.SelectedValue,
                        FechaHoraInicio = (DateTime)dtpFechaInicio.SelectedDate,
                        FechaHoraTermino = (DateTime)dtpFechaTermino.SelectedDate,
                        Asistentes = Convert.ToInt32(txtAsistentes.Text),
                        PersonalAdicional = Convert.ToInt32(txtPersonal.Text),
                        ValorTotalContrato = Convert.ToDouble(txtValor.Text),
                        Observaciones = txtObservaciones.Text
                    };
                    switch (con.IdTipoEvento)
                    {
                        case 10:
                            CoffeeBreak cb = new CoffeeBreak();
                            cb.Numero = con.Numero;
                            cb.Vegetariana = (bool)chkVegetariana.IsChecked;
                            cb.Create();
                            break;
                        case 20:
                            Cocktail ct = new Cocktail();
                            ct.Numero = con.Numero;
                            ct.LeerTipoAmbientacion();
                            if (rbPersonalizada.IsChecked == true)
                            {
                                ct.IdTipoAmbientacion = 20;
                                ct.Ambientacion = (bool)rbPersonalizada.IsChecked;
                            }
                            else
                            {
                                ct.IdTipoAmbientacion = 10;
                                ct.Ambientacion = (bool)rbBasica.IsChecked;
                            }

                            ct.MusicaAmbiental = (bool)chkMusica.IsChecked;
                            ct.Create();

                            break;
                        case 30:
                            Cenas ce = new Cenas();


                            ce.Numero = con.Numero;
                            ce.LeerTipoAmbientacion();
                            if (rbPersonalizada.IsChecked == true)
                            {
                                ce.IdTipoAmbientacion = 20;

                            }
                            else
                            {
                                ce.IdTipoAmbientacion = 10;
                            }

                            ce.MusicaAmbiental = (bool)chkMusica.IsChecked;
                            if (rbOnBreak.IsChecked == true)
                            {
                                ce.Local = (bool)rbOnBreak.IsChecked;
                            }
                            else if (rbOtro.IsChecked == true)
                            {
                                ce.OtroLocal = (bool)rbOtro.IsChecked;
                                ce.ValorArriendo = Convert.ToInt16(txtArriendo.Text);
                            }
                            else
                            {
                                ce.OtroLocal = false;
                                ce.Local = false;
                            }

                            ce.Create();

                            break;
                        default:
                            break;
                    }
                    if (!con.Read())
                    {
                        if (con.FechaHoraInicio < con.FechaHoraTermino)
                        {

                            Contacto cli = new Contacto() { RutContacto = cbRut.SelectedValue.ToString() };
                            if (cli.Read())
                            {
                                if (con.Create())
                                {
                                    MessageDialogResult result = await this.ShowMessageAsync("Confirmación", "Contrato agregado correctamente.", MessageDialogStyle.Affirmative);
                                    LimpiarVentana();
                                }
                                else
                                {
                                    MessageDialogResult result = await this.ShowMessageAsync("Error", "No se pudo registrar, llene todos los datos correctamente.", MessageDialogStyle.Affirmative);
                                }
                            }
                            else
                            {
                                MessageDialogResult result = await this.ShowMessageAsync("Error", "No se pudo registrar, porque el contacto no existe.", MessageDialogStyle.Affirmative);
                            }
                        }
                        else
                        {
                            MessageDialogResult result = await this.ShowMessageAsync("Error", "No se pudo registrar, porque la fecha de termino no es mayor a fecha inicio.", MessageDialogStyle.Affirmative);
                        }
                    }
                    else
                    {
                        MessageDialogResult result = await this.ShowMessageAsync("Error", "Contrato ya existe.", MessageDialogStyle.Affirmative);
                    }

                }
            }

        }
        private async void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                if (await Confirmacion("Modificar"))
                {
                    Contrato con = new Contrato
                    {
                        Numero = txtNumero.Text,
                        IdModalidad = cbModalidad.SelectedValue.ToString(),
                        IdTipoEvento = (int)cbTipoEvento.SelectedValue,
                        FechaHoraInicio = (DateTime)dtpFechaTermino.SelectedDate,
                        FechaHoraTermino = (DateTime)dtpFechaTermino.SelectedDate,
                        Asistentes = Convert.ToInt32(txtAsistentes.Text),
                        PersonalAdicional = Convert.ToInt32(txtPersonal.Text),
                        ValorTotalContrato = double.Parse(txtValor.Text),
                        Observaciones = txtObservaciones.Text
                    };
                    if (con.Read())
                    {
                        if (con.Termino == con.Creacion)
                        {
                            switch (con.IdTipoEvento)
                            {
                                case 10:
                                    CoffeeBreak cb = new CoffeeBreak();
                                    cb.Numero = con.Numero;
                                    cb.Vegetariana = (bool)chkVegetariana.IsChecked;
                                    cb.Update();
                                    break;
                                case 20:
                                    Cocktail ct = new Cocktail();
                                    ct.Numero = con.Numero;
                                    if (rbPersonalizada.IsChecked == true) { ct.IdTipoAmbientacion = 20; }
                                    else if (rbBasica.IsChecked == true) { ct.IdTipoAmbientacion = 10; }
                                    else { ct.Ambientacion = false; }
                                    ct.MusicaAmbiental = (bool)chkMusica.IsChecked;
                                    ct.Update();
                                    break;
                                case 30:
                                    Cenas ce = new Cenas();
                                    ce.Numero = con.Numero;
                                    if (rbPersonalizada.IsChecked == true) { ce.IdTipoAmbientacion = 20; }
                                    else { ce.IdTipoAmbientacion = 10; }
                                    ce.MusicaAmbiental = (bool)chkMusica.IsChecked;
                                    if (rbOnBreak.IsChecked == true) { ce.Local = (bool)rbOnBreak.IsChecked; }
                                    else if (rbOtro.IsChecked == true)
                                    {
                                        ce.OtroLocal = (bool)rbOtro.IsChecked;
                                        ce.ValorArriendo = Convert.ToInt16(txtArriendo.Text);
                                    }
                                    ce.Update();
                                    break;
                                default:
                                    break;
                            }
                            if (con.Update())
                            {
                                LimpiarVentana();
                                MessageDialogResult result =
                                    await this.ShowMessageAsync("Confirmación", "Contrato actualizado correctamente.", MessageDialogStyle.Affirmative);
                            }
                            else
                            {
                                MessageDialogResult result =
                                    await this.ShowMessageAsync("Error", "No se pudo actualizar, llene todos los datos correctamente.", MessageDialogStyle.Affirmative);
                            }
                        }
                        else
                        {
                            MessageDialogResult result =
                                await this.ShowMessageAsync("Error", "No se pudo actualizar, este contrato ya esta terminado.", MessageDialogStyle.Affirmative);

                        }
                    }
                    else
                    {
                        MessageDialogResult result =
                            await this.ShowMessageAsync("Error", "Contrato no existe.", MessageDialogStyle.Affirmative);
                    }
                }
            }
        }
        private async void BtnTerminar_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                Contrato con = new Contrato();
                if (con.Termino != con.Creacion)
                {
                    if (await Confirmacion("Terminar"))
                    {
                        if (con.Read())
                        {
                            con.Termino = DateTime.Now;
                            if (con.Termino > con.FechaHoraTermino)
                            {
                                con.Realizado = true;
                            }
                            if (con.Update())
                            {
                                LimpiarVentana();
                                MessageDialogResult result =
                                    await this.ShowMessageAsync("Confirmación", "Contrato terminado correctamente.", MessageDialogStyle.Affirmative);
                            }
                            else
                            {
                                MessageDialogResult result =
                                    await this.ShowMessageAsync("Error", "No se pudo terminar, llene todos los datos.", MessageDialogStyle.Affirmative);
                            }
                        }
                        else
                        {
                            MessageDialogResult result =
                                    await this.ShowMessageAsync("Error", "Contrato no existe.", MessageDialogStyle.Affirmative);
                        }
                    }
                }
                else
                {
                    MessageDialogResult result =
                            await this.ShowMessageAsync("Error", "Contrato ya esta terminado.", MessageDialogStyle.Affirmative);
                }
            }
            else
            {
                MessageDialogResult result =
                    await this.ShowMessageAsync("Error", "No se pudo terminar, llene todos los datos.", MessageDialogStyle.Affirmative);
            }
        }
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarVentana();
        }
        //--------------------------------------------------------------------------
        public void CargarContrato(Contrato con)
        {
            if (con.Read())
            {
                txtNumero.Text = con.Numero;
                cbRut.SelectedValue = con.RutContacto;
                cbTipoEvento.SelectedValue = con.IdTipoEvento;
                Contacto cli = new Contacto() { RutContacto = cbRut.SelectedValue.ToString() };
                if (cli.Read())
                {
                    txtRazonSocial.Text = cli.RazonSocial;
                }
                CargarTabControls();
                CargarModalidades();

                cbModalidad.SelectedValue = con.IdModalidad;
                dtpFechaInicio.SelectedDate = con.FechaHoraInicio;
                dtpFechaTermino.SelectedDate = con.FechaHoraTermino;
                txtObservaciones.Text = con.Observaciones;
                txtAsistentes.Text = con.Asistentes.ToString();
                txtPersonal.Text = con.PersonalAdicional.ToString();
                switch (con.IdTipoEvento)
                {
                    case 10:
                        CoffeeBreak cb = new CoffeeBreak() { Numero = con.Numero };
                        if (cb.Read()) { chkVegetariana.IsChecked = cb.Vegetariana; };
                        break;
                    case 20:
                        Cocktail ct = new Cocktail() { Numero = con.Numero };
                        if (ct.Read())
                        {
                            ct.IdTipoAmbientacion = 10;
                            chkMusica.IsChecked = ct.MusicaAmbiental;
                        };
                        break;
                    case 30:
                        Cenas ce = new Cenas() { Numero = con.Numero };
                        if (ce.Read())
                        {
                            ce.IdTipoAmbientacion = 10;
                            chkMusica.IsChecked = ce.MusicaAmbiental;
                            rbOnBreak.IsChecked = ce.Local;
                            rbOtro.IsChecked = ce.OtroLocal;
                            txtArriendo.Text = ce.ValorArriendo.ToString();
                        };
                        break;
                    default: break;
                }
            }
        }
        private async Task<bool> Confirmacion(string text)
        {
            bool result = false;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = text,
                NegativeButtonText = "Cancelar",
                AnimateShow = true,
                AnimateHide = false
            };
            MessageDialogResult x =
                            await this.ShowMessageAsync("Confirmación", "¿" + text + " Contrato?", MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (x == MessageDialogResult.Affirmative)
            {
                result = true;
            }
            return result;
        }
        public bool Check()
        {
            if (cbRut.SelectedValue.ToString() != string.Empty ||
            cbModalidad.SelectedIndex > -1 ||
            cbTipoEvento.SelectedIndex > -1 ||
            dtpFechaInicio.SelectedDate > DateTime.Now ||
            dtpFechaTermino.SelectedDate > DateTime.Now ||
            txtAsistentes.Text != string.Empty ||
            txtPersonal.Text != string.Empty ||
            txtValor.Text != string.Empty ||
            txtValor.Text != string.Empty ||
            txtObservaciones.Text != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //-----------------------------------------------------------------------------------------
        private void CbTipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarModalidades();
            CargarTabControls();
        }
        private void CargarTabControls()
        {
            if (cbTipoEvento.SelectedIndex != -1)
            {
                var userStrategy = (int)cbTipoEvento.SelectedValue;
                switch (userStrategy)
                {
                    case 10:
                        Tb1.Visibility = Visibility.Hidden;
                        Tb2.Visibility = Visibility.Hidden;
                        break;
                    case 20:
                        Tb1.Visibility = Visibility.Visible;
                        rbBasica.IsChecked = true;
                        Tb2.Visibility = Visibility.Hidden;
                        break;
                    case 30:
                        Tb1.Visibility = Visibility.Visible;
                        rbBasica.IsChecked = true;
                        Tb2.Visibility = Visibility.Visible;
                        break;
                    default:
                        Tb1.Visibility = Visibility.Hidden;
                        Tb2.Visibility = Visibility.Hidden;
                        break;
                }
            }
            else
            {
                Tb1.Visibility = Visibility.Hidden;
                Tb2.Visibility = Visibility.Hidden;
            }
        }
        private void CbModalidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((cbModalidad.Items.Count != 0) && (cbModalidad.SelectedValue != null))
            {
                MostrarValor();
            }
        }
        private void AccionMostrarValor(object sender, RoutedEventArgs e)
        {
            MostrarValor();
        }
        private void MostrarValor()
        {
            try
            {
                if ((cbModalidad.Items.Count != 0) && (cbModalidad.SelectedValue != null))
                {
                    //VENTANA COFFEE BREAK
                    IValorizador CoffeeEvent = new DataType();
                    EventCoffee CoffeeClient = new EventCoffee(CoffeeEvent);
                    IValorizador CocktailEvent = new DataType();
                    EventCockTail CocktailClient = new EventCockTail(CocktailEvent);
                    IValorizador CenaEvent = new DataType();
                    EventCena CenaClient = new EventCena(CenaEvent);
                    //VENTANAS
                    var userStrategy = cbModalidad.SelectedValue.ToString().ToLower();
                    string _mod = cbModalidad.SelectedValue.ToString();
                    int _asi = 0;
                    if (txtAsistentes.Text != string.Empty) { _asi = Convert.ToInt32(txtAsistentes.Text); }
                    int _per = 0;
                    if (txtPersonal.Text != string.Empty && Convert.ToInt32(txtPersonal.Text) > 0) { _per = Convert.ToInt32(txtPersonal.Text); }
                    //VENTANA VALORIZAR COFFEE BREAK
                    bool _vegetariana = (bool)chkVegetariana.IsChecked;
                    //VENTANA VALORIZAR COCKTAIL Y CENA
                    bool _basic = (bool)rbBasica.IsChecked;
                    bool _custom = (bool)rbPersonalizada.IsChecked;
                    bool _music = (bool)chkMusica.IsChecked;
                    //VENTANA VALORIZAR CENA
                    bool _local = (bool)rbOnBreak.IsChecked;
                    double _otro = 0;
                    if (txtArriendo.Text != string.Empty) { _otro = Convert.ToDouble(txtArriendo.Text); }

                    switch (userStrategy)
                    {
                        //VENTANA COFFEEBREAK
                        case "cb001":
                            txtValor.Text = CoffeeClient.GetLightValue(_mod, _asi, _per).ToString();
                            break;
                        case "cb002":
                            txtValor.Text = CoffeeClient.GetJournalValue(_mod, _asi, _per).ToString();
                            break;
                        case "cb003":
                            txtValor.Text = CoffeeClient.GetDayValue(_mod, _asi, _per).ToString();
                            break;//VENTANA COFFEEBREAK
                                  //ventana valorizar cocktail   
                        case "co001":
                            txtValor.Text = CocktailClient.GetQuickValue(_mod, _asi, _per, _basic, _custom, _music).ToString();
                            break;
                        case "co002":
                            txtValor.Text = CocktailClient.GetAmbientValue(_mod, _asi, _per, _basic, _custom, _music).ToString();
                            break;//VALORIZAR COCKTAIL
                                  //VALORIZAR CENA
                        case "ce001":
                            txtValor.Text = CenaClient.GetEjecutivaValue(_mod, _asi, _per, _basic, _custom, _music, _local, _otro).ToString();
                            break;
                        case "ce002":
                            txtValor.Text = CenaClient.GetCelebracionValue(_mod, _asi, _per, _basic, _custom, _music, _local, _otro).ToString();
                            break;//VALORIZAR CENA
                        default:
                            txtValor.Text = "0";
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        //-----------------------------------------------------------------------------------------
        private void FlyoutStyle()
        {
            Flyout.Theme = FlyoutTheme.Adapt;
            Flyout.Theme = FlyoutTheme.Accent;

        }
        private void BtnFlyout_Click(object sender, RoutedEventArgs e)
        {
            Flyout.Theme = FlyoutTheme.Accent;
            Flyout.IsOpen = true;
        }
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Hide();
            if (Contraste == true)
            {
                w.OnContraste(w);
                w.Contraste = true;
            }
        }
        private void TileAdminCli_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            AdminCliente w = new AdminCliente();
            w.Show();
            Hide();
            if (Contraste)
            {
                mw.OnContraste(w);
                w.Contraste = true;
            }
        }
        private void TileListCli_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            ListCliente w = new ListCliente();
            w.Show();
            this.Hide();
            if (Contraste == true)
            {
                mw.OnContraste(w);
                w.Contraste = true;
            }
        }
        private void TileListCon_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            ListContrato w = new ListContrato();
            w.Show();
            this.Hide();
            w.VerBtnCargar();
            if (Contraste == true)
            {
                mw.OnContraste(w);
                w.Contraste = true;
            }
        }
        //-----------------------------------------------------------------------------------------
        private void KeyDownTel(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Add)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void KeyDownNum(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void KeyDownRut(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.K)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        //-----------------------------------------------------------------------------------------
        private void TxtArriendo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((bool)rbOtro.IsChecked)
            {
                MostrarValor();
            }
        }
        private void BtnRepaldar_Click(object sender, RoutedEventArgs e)
        {
            GuardarInfo();
        }
        private async void DtpFechaTermino_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpFechaTermino.SelectedDate < dtpFechaInicio.SelectedDate)
            {
                dtpFechaTermino.SelectedDate = DateTime.Now;
                await this.ShowMessageAsync("Error!!", "La fecha de termino no puede ser inferior a la fecha de inicio...");
            }
        }
        private void ChkMusica_Unchecked(object sender, RoutedEventArgs e)
        {
            MostrarValor();
        }
        private void AccionMostrarValor(object sender, TextChangedEventArgs e)
        {
            MostrarValor();
        }
        private void AccionMostrarValor1(object sender, TextChangedEventArgs e)
        {
            MostrarValor();
        }
        private void CbRut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LeerRazon();
        }
        private void LeerRazon()
        {
            if (cbRut.SelectedValue != null)
            {
                Contacto cli = new Contacto() { RutContacto = cbRut.SelectedValue.ToString() };
                if (cli.Read())
                {
                    txtRazonSocial.Text = cli.RazonSocial;
                }
            }
        }
        //-----------------------------------------------------------------------------------------
    }
}