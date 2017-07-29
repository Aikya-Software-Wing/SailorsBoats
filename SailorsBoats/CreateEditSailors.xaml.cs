using SailorsBoats.DAL;
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
    /// Interaction logic for CreateEditSailors.xaml
    /// </summary>
    public partial class CreateEditSailors : Window
    {
        private int SailorId = -1;
        private SailorDAL dal;

        public CreateEditSailors()
        {
            InitializeComponent();
            dal = SailorDAL.Instance;
        }

        public CreateEditSailors(int SailorId) : this()
        {
            this.SailorId = SailorId;
            DisplaySailorForEdit();
        }

        private void DisplaySailorForEdit()
        {
            Sailor sailor = dal.GetSailor(SailorId);
            SailorId_TextBox.Text = sailor.Id + "";
            SailorAge_TextBox.Text = sailor.Age + "";
            SailorName_TextBox.Text = sailor.Name + "";
            SailorRating_TextBox.Text = sailor.Rating + "";
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(SailorId_TextBox.Text);
            string name = SailorName_TextBox.Text;
            int rating = int.Parse(SailorRating_TextBox.Text);
            int age = int.Parse(SailorAge_TextBox.Text);

            Sailor sailor = new Sailor
            {
                Age = age,
                Id = id,
                Name = name,
                Rating = rating
            };

            if (SailorId == -1)
            {
                dal.AddSailor(sailor);
            }
            else
            {
                dal.UpdateSailor(SailorId, sailor);
            }

            Close();
        }
    }
}
