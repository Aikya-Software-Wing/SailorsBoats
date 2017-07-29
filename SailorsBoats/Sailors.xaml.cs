using SailorsBoats.Models;
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

namespace SailorsBoats
{
    /// <summary>
    /// Interaction logic for Sailors.xaml
    /// </summary>
    public partial class Sailors : Window
    {
        private List<Sailor> SailorList;

        public Sailors()
        {
            InitializeComponent();
            SailorList = new List<Sailor>
            {
                new Sailor { Age = 10, Id = 1, Name = "Sailor 1", Rating = 10 },
                new Sailor { Age = 13, Id = 2, Name = "Sailor 4", Rating = 10 },
                new Sailor { Age = 15, Id = 3, Name = "Sailor 6", Rating = 10 },
                new Sailor { Age = 12, Id = 4, Name = "Sailor 3", Rating = 10 }
            };

            SailorsDataGrid.ItemsSource = SailorList;
        }
    }
}
