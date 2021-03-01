using System;
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
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using OnBreak.Negocio;

namespace OnBreak.Wpf
{
    /// <summary>
    /// Lógica de interacción para AdminCliente.xaml
    /// </summary>
    public partial class AdminCliente : MetroWindow
    {
        public AdminCliente()
        {
            InitializeComponent();
            LimpiarVentana();
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
        private void LimpiarVentana()
        {
            txtRut.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            CargarActividades();
            CargarTipos();
        }
        private void CargarActividades()
        {
            cbActividad.ItemsSource = new ActividadEmpresas().ReadAll();
            cbActividad.DisplayMemberPath = "Descripcion";
            cbActividad.SelectedValuePath = "IdActividadEmpresa";
            cbActividad.SelectedIndex = 0;
        }
        private void CargarTipos()
        {
            cbTipoEmpresa.ItemsSource = new TipoEmpresas().ReadAll();
            cbTipoEmpresa.DisplayMemberPath = "Descripcion";
            cbTipoEmpresa.SelectedValuePath = "IdTipoEmpresa";
            cbTipoEmpresa.SelectedIndex = 0;
        }
        //--------------------------------------------------------------------------
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
            Contacto cli = new Contacto()
            {
                RutContacto = txtRut.Text
            };
            if (cli.Read())
            {
                txtRazonSocial.Text = cli.RazonSocial;
                txtNombre.Text = cli.NombreContacto;
                txtMail.Text = cli.Correo;
                txtDireccion.Text = cli.Direccion;
                txtTelefono.Text = cli.Telefono;
                cbActividad.SelectedValue = cli.IdActividadEmpresa;
                cbTipoEmpresa.SelectedValue = cli.IdTipoEmpresa;
                MessageDialogResult result =
                    await this.ShowMessageAsync("Confirmación", "Cliente Cargado Correctamente.", MessageDialogStyle.Affirmative);
            }
            else
            {
                MessageDialogResult result =
                    await this.ShowMessageAsync("Error", "No se pudo encontrar al cliente o no existe.", MessageDialogStyle.Affirmative);
            }
        }
        private async void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                if (await MensajeConfirmacion("Registrar"))
                {
                    Contacto cli = new Contacto();

                    cli.RutContacto = txtRut.Text;
                    cli.RazonSocial = txtRazonSocial.Text;
                    cli.NombreContacto = txtNombre.Text;
                    cli.Correo = txtMail.Text;
                    cli.Direccion = txtDireccion.Text;
                    cli.Telefono = txtTelefono.Text;
                    cli.IdActividadEmpresa = (int)cbActividad.SelectedValue;
                    cli.IdTipoEmpresa = (int)cbTipoEmpresa.SelectedValue;
                    if (!cli.Read())
                    {
                        if (cli.Create())
                        {
                            LimpiarVentana();
                            MessageDialogResult result =
                                await this.ShowMessageAsync("Confirmación", "Cliente Registrado correctamente.", MessageDialogStyle.Affirmative);
                        }
                        else
                        {
                            MessageDialogResult result =
                                await this.ShowMessageAsync("Error", "No se pudo registrar, llene todos los datos correctamente.", MessageDialogStyle.Affirmative);
                        }
                    }
                    else
                    {
                        MessageDialogResult result =
                                await this.ShowMessageAsync("Error", "Cliente ya existe.", MessageDialogStyle.Affirmative);
                    }
                }
            }
            else
            {
                MessageDialogResult result =
                    await this.ShowMessageAsync("Error", "No se pudo registrar, llene todos los datos correctamente.", MessageDialogStyle.Affirmative);
            }
        }
        private async void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                if (await MensajeConfirmacion("Modificar"))
                {
                    Contacto cli = new Contacto
                    {
                        RutContacto = txtRut.Text,
                        RazonSocial = txtRazonSocial.Text,
                        NombreContacto = txtNombre.Text,
                        Correo = txtMail.Text,
                        Direccion = txtDireccion.Text,
                        Telefono = txtTelefono.Text,
                        IdActividadEmpresa = (int)cbActividad.SelectedValue,
                        IdTipoEmpresa = (int)cbTipoEmpresa.SelectedValue
                    };
                    if (cli.Read())
                    {
                        
                        if (cli.Update())
                        {
                            LimpiarVentana();
                            MessageDialogResult result =
                                await this.ShowMessageAsync("Confirmación", "Cliente modificado correctamente.", MessageDialogStyle.Affirmative);
                        }
                        else
                        {
                            MessageDialogResult result =
                                await this.ShowMessageAsync("Error", "No se pudo modificar, llene todos los datos correctamente.", MessageDialogStyle.Affirmative);
                        }
                    }
                    else
                    {
                        MessageDialogResult result =
                                await this.ShowMessageAsync("Error", "Cliente no existe.", MessageDialogStyle.Affirmative);
                    }
                }
            }
            else
            {
                MessageDialogResult result =
                    await this.ShowMessageAsync("Error", "No se pudo modificar, llene todos los datos correctamente.", MessageDialogStyle.Affirmative);
            }

        }
        private async void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
            {
                if (await MensajeConfirmacion("Eliminar"))
                {
                    Contacto cli = new Contacto()
                    {
                        RutContacto = txtRut.Text
                    };
                    if (cli.Read())
                    {
                        if (cli.Delete())
                        {
                            LimpiarVentana();
                            MessageDialogResult result =
                                await this.ShowMessageAsync("Confirmación", "Cliente eliminado correctamente.", MessageDialogStyle.Affirmative);
                        }
                        else
                        {
                            MessageDialogResult result =
                                await this.ShowMessageAsync("Error", "No se puede eliminar cliente con contratos asociados.", MessageDialogStyle.Affirmative);
                        }
                    }
                    else
                    {
                        MessageDialogResult result =
                                await this.ShowMessageAsync("Error", "Cliente no existe.", MessageDialogStyle.Affirmative);
                    }
                }
            }
            else
            {
                MessageDialogResult result =
                    await this.ShowMessageAsync("Error", "No se pudo eliminar, llene todos los datos correctamente.", MessageDialogStyle.Affirmative);
            }
        }
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarVentana();
        }
        //--------------------------------------------------------------------------
        public void CargarCliente(Contacto cli)
        {
            if (cli.Read())
            {
                txtRut.Text = cli.RutContacto;
                txtRazonSocial.Text = cli.RazonSocial;
                txtNombre.Text = cli.NombreContacto;
                txtMail.Text = cli.Correo;
                txtDireccion.Text = cli.Direccion;
                txtTelefono.Text = cli.Telefono;
                cbActividad.SelectedValue = cli.IdActividadEmpresa;
                cbTipoEmpresa.SelectedValue = cli.IdTipoEmpresa;
            }
        }
        private async Task<bool> MensajeConfirmacion(string text)
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
                            await this.ShowMessageAsync("Confirmación", "¿" + text + " Cliente?", MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (x == MessageDialogResult.Affirmative)
            {
                result = true;
            }
            return result;
        }
        public bool Check()
        {
            if (txtRut.Text != string.Empty ||
            txtRazonSocial.Text != string.Empty ||
            txtNombre.Text != string.Empty ||
            txtMail.Text != string.Empty ||
            txtDireccion.Text != string.Empty ||
            txtTelefono.Text != string.Empty ||
            cbActividad.SelectedIndex > -1 ||
            cbTipoEmpresa.SelectedIndex > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //--------------------------------------------------------------------------
        private void FlyoutStyle()
        {
            Flyout.Theme = FlyoutTheme.Adapt;
            Flyout.Theme = FlyoutTheme.Accent;

        }
        private void BtnFlyout_Click(object sender, RoutedEventArgs e)
        {
            FlyoutStyle();
            Flyout.IsOpen = true;
        }
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Hide();
            if (Contraste)
            {
                w.OnContraste(w);
                w.Contraste = true;
            }
        }
        private void TileListCli_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            ListCliente w = new ListCliente();
            w.Show();
            this.Hide();
            w.VerBtnCargar();
            if (Contraste)
            {
                mw.OnContraste(w);
                w.Contraste = true;
            }
        }
        private void TileAdminCon_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            AdminContrato w = new AdminContrato();
            w.Show();
            this.Hide();
            if (Contraste)
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
            if (Contraste)
            {
                mw.OnContraste(w);
                w.Contraste = true;
            }
        }
       // --------------------------------------------------------------------------
        private void KeyDowntel(object sender, KeyEventArgs e)
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
        private void KeyDown1(object sender, KeyEventArgs e)
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
    }
}
