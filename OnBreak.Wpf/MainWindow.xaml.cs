using System;
using System.Collections.Generic;
using System.IO;
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
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace OnBreak.Wpf
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            {
                Application.Current.Shutdown();
            }
        }

        private void TileAdmCli_Click(object sender, RoutedEventArgs e)
        {
            AdminCliente w = new AdminCliente();
            w.Show();
            this.Hide();
            if (Contraste)
            {
                OnContraste(w);
                w.Contraste = true;
            }
        }

        private void TileListCli_Click(object sender, RoutedEventArgs e)
        {
            ListCliente w = new ListCliente();
            w.Show();
            this.Hide();
            if (Contraste)
            {
                OnContraste(w);
                w.Contraste = true;
            }
        }

        private void TileALisCon_Click(object sender, RoutedEventArgs e)
        {
            ListContrato w = new ListContrato();
            w.Show();
            this.Hide();
            if (Contraste)
            {
                OnContraste(w);
                w.Contraste = true;
            }
        }

        private void TileAdmCon_Click(object sender, RoutedEventArgs e)
        {
            
            AdminContrato w = new AdminContrato();
            w.Show();
            this.Hide();
            if (Contraste)
            {
                OnContraste(w);
                w.Contraste = true;
            }
        }

        public bool Contraste = false;

        private void BtnAltoContraste_Click(object sender, RoutedEventArgs e)
        {
            if (Contraste)
            {
                OffContraste(this);
                Contraste = false;
            }
            else
            {
                OnContraste(this);
                Contraste = true;
            }
        }

        public void OffContraste(Window window)
        {
            ThemeManager.ChangeAppStyle(window,
                ThemeManager.GetAccent("Blue"),
                ThemeManager.GetAppTheme("BaseLight"));
        }

        public void OnContraste(Window window)
        {
            ThemeManager.AddAccent("AltoContraste", new Uri("pack://application:,,,/Skin/Skin.xaml"));
            ThemeManager.AddAppTheme("FondoAltoContraste", new Uri("pack://application:,,,/Skin/Tema.xaml"));
            ThemeManager.ChangeAppStyle(window,
                                        ThemeManager.GetAccent("AltoContraste"),
                                        ThemeManager.GetAppTheme("FondoAltoContraste"));
        }

        private async void BtnSalirMain_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Salir",
                NegativeButtonText = "Cancelar",
                AnimateShow = true,
                AnimateHide = false
            };

            var result = await this.ShowMessageAsync(
                "ADVERTENCIA",
                "¿Seguro que desea cerrar la aplicación?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative) Application.Current.Shutdown();
        }
    }
}
