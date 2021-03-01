using MahApps.Metro.Controls;
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
using MahApps.Metro.Controls.Dialogs;
using OnBreak.Negocio;

namespace OnBreak.Wpf
{
    /// <summary>
    /// Lógica de interacción para ListCliente.xaml
    /// </summary>
    public partial class ListCliente : MetroWindow
    {

        public ListCliente()
        {
            InitializeComponent();
            LimpiarVentana();
            CargarLista();
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

        public void VerBtnCargar()
        {
            BtnCargar.Visibility = Visibility.Visible;
        }

        private void LimpiarVentana()
        {
            txtRut.Text = string.Empty;
            CargarActividades();
            CargarTipos();
        }
        //--------------------------------------------------------------------------
        private void CargarActividades()
        {
            cbActividad.ItemsSource = new ActividadEmpresas().ReadAll();
            cbActividad.DisplayMemberPath = "Descripcion";
            cbActividad.SelectedValuePath = "IdActividadEmpresa";
            cbActividad.SelectedIndex = -1;
        }

        private void CargarTipos()
        {
            cbTipoEmpresa.ItemsSource = new TipoEmpresas().ReadAll();
            cbTipoEmpresa.DisplayMemberPath = "Descripcion";
            cbTipoEmpresa.SelectedValuePath = "IdTipoEmpresa";
            cbTipoEmpresa.SelectedIndex = -1;
        }

        private void CargarLista()
        {
            dgListaClientes.ItemsSource = new Contacto().ReadAll();
            dgListaClientes.Items.Refresh();
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

        private void BtnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            string rut;
            rut = txtRut.Text.ToString();
            if (rut != string.Empty)
            {
                dgListaClientes.ItemsSource = new Contacto().ReadAllByRut(rut);
                LimpiarVentana();
            }
            else if (cbActividad.SelectedIndex != -1)
            {
                dgListaClientes.ItemsSource = new Contacto().ReadAllByActividad((int)cbActividad.SelectedValue);
                LimpiarVentana();
            }
            else if (cbTipoEmpresa.SelectedIndex != -1)
            {
                dgListaClientes.ItemsSource = new Contacto().ReadAllByTipoEmpresa((int)cbTipoEmpresa.SelectedValue);
                LimpiarVentana();
            }
            dgListaClientes.Items.Refresh();
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarVentana();
            CargarLista();
        }

        private async void BtnCargar_Click(object sender, RoutedEventArgs e)
        {
            if (dgListaClientes.SelectedIndex > -1)
            {
                Contacto cl = new Contacto();
                var data = dgListaClientes.SelectedItem;
                cl.RutContacto = (dgListaClientes.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                MainWindow mw = new MainWindow();
                AdminCliente w = new AdminCliente();
                w.CargarCliente(cl);
                w.Show();
                this.Hide();
                if (Contraste)
                {
                    mw.OnContraste(w);
                    w.Contraste = true;
                }
            }
            else
            {
                MessageDialogResult result =
                                        await this.ShowMessageAsync("Atención", "Seleccione una fila", MessageDialogStyle.Affirmative);
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
            Flyout.Theme = FlyoutTheme.Accent;
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

        private void TileAdminCli_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            AdminCliente w = new AdminCliente();
            w.Show();
            this.Hide();
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

       
    }
}
