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
using OnBreak.Negocio;
using MahApps.Metro.Controls.Dialogs;

namespace OnBreak.Wpf
{
    /// <summary>
    /// Lógica de interacción para ListContrato.xaml
    /// </summary>
    public partial class ListContrato : MetroWindow
    {
        public ListContrato()
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
        private void LimpiarVentana()
        {
            txtNumero.Text = string.Empty;
            CargarRut();
            CargarTipos();
        }
        private void CargarRut()
        {
            cbRut.ItemsSource = new Contacto().ReadAll();
            cbRut.DisplayMemberPath = "RutContacto";
            cbRut.SelectedValuePath = "RutContacto";
            cbRut.SelectedIndex = -1;
        }
        private void CargarTipos()
        {
            cbTipoEvento.ItemsSource = new TipoEventos().ReadAll();
            cbTipoEvento.DisplayMemberPath = "Descripcion";
            cbTipoEvento.SelectedValuePath = "IdTipoEvento";
            cbTipoEvento.SelectedIndex = -1;
            CargarModalidades();
        }
        private void CargarModalidades()
        {
            if ((cbTipoEvento.Items.Count != 0) && (cbTipoEvento.SelectedValue != null))
            {
                cbModalidad.ItemsSource = new ModalidadServicios().ReadAllByModalidad((int)cbTipoEvento.SelectedValue);
                cbModalidad.DisplayMemberPath = "Nombre";
                cbModalidad.SelectedValuePath = "IdModalidad";
                cbModalidad.SelectedIndex = -1;
            }
            else
            {
                cbModalidad.ItemsSource = cbModalidad.ItemsSource = new ModalidadServicios().ReadAll();
                cbModalidad.DisplayMemberPath = "Nombre";
                cbModalidad.SelectedValuePath = "IdModalidad";
                cbModalidad.SelectedIndex = -1;
            }
        }
        private void CargarLista()
        {
            dgListaContratos.ItemsSource = new Contrato().ReadAll();
            dgListaContratos.Items.Refresh();
           
        }
        public void VerBtnCargar()
        {
            BtnCargar.Visibility = Visibility.Visible;
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
            if (txtNumero.Text != string.Empty)
            {
                dgListaContratos.ItemsSource = new Contrato().ReadAllByNumero(txtNumero.Text);
            }
            if (cbRut.SelectedValue != null)
            {
                string rut = cbRut.SelectedValue.ToString();
                dgListaContratos.ItemsSource = new Contrato().ReadAllByRut(rut);
            }
            if (cbTipoEvento.SelectedValue != null)
            {
                dgListaContratos.ItemsSource = new Contrato().ReadAllByTipo((int)cbTipoEvento.SelectedValue);
            }
            if (cbModalidad.SelectedValue != null)
            {
                dgListaContratos.ItemsSource = new Contrato().ReadAllByModalidad(cbModalidad.SelectedValue.ToString());
            }
            LimpiarVentana();
            dgListaContratos.Items.Refresh();
        }
        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarVentana();
            CargarLista();
        }
        private async void BtnCargar_Click(object sender, RoutedEventArgs e)
        {
            if (dgListaContratos.SelectedIndex > -1)
            {
                Contrato con = new Contrato();
                var data = dgListaContratos.SelectedItem;
                con.Numero = (dgListaContratos.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                MainWindow mw = new MainWindow();
                AdminContrato w = new AdminContrato();
                w.CargarContrato(con);
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
        private void CbTipoEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarModalidades();
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

        private void TileListCli_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            ListCliente w = new ListCliente();
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

    }
}
