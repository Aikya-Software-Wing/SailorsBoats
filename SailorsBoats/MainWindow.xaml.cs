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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SailorsBoats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SailorsButton_Click(object sender, RoutedEventArgs e)
        {
            Sailors sailorWindow = new Sailors();
            sailorWindow.Show();
        }

        private void BoatsButton_Click(object sender, RoutedEventArgs e)
        {
            Boats boatsWindow = new Boats();
            boatsWindow.Show();
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            ReserveBoat reserveBoatWindow = new ReserveBoat();
            reserveBoatWindow.Show();
        }
    }
}
