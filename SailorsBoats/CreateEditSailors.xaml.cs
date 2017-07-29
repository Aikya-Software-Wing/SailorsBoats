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
    /// Interaction logic for CreateEditSailors.xaml
    /// </summary>
    public partial class CreateEditSailors : Window
    {
        private int SailorId = -1;

        public CreateEditSailors()
        {
            InitializeComponent();
        }

        public CreateEditSailors(int SailorId) : this()
        {
            this.SailorId = SailorId;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            string id = SailorId_TextBox.Text;
            string name = SailorName_TextBox.Text;
            string rating = SailorRating_TextBox.Text;
            string age = SailorAge_TextBox.Text;


        }
    }
}
