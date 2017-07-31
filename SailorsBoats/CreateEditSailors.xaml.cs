using SailorsBoats.DAL;
using SailorsBoats.Models;
using SailorsBoats.Validators;
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

            SailorId_TextBox.Text = (dal.GetAllSailors().Max(x => x.Id) + 1) + "";
            SailorId_TextBox.IsEnabled = false;
        }

        public CreateEditSailors(int SailorId) : this()
        {
            this.SailorId = SailorId;
            DisplaySailorForEdit();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSailorAndDisplayMessages(SailorId_TextBox.Text, SailorName_TextBox.Text,
                SailorRating_Slider.Value+"", SailorAge_TextBox.Text))
            {
                return;
            }

            Sailor sailor = GetSailorObjectFromInput();

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

        #region Helpers
        private void DisplaySailorForEdit()
        {
            Sailor sailor = dal.GetSailor(SailorId);
            SailorId_TextBox.Text = sailor.Id + "";
            SailorAge_TextBox.Text = sailor.Age + "";
            SailorName_TextBox.Text = sailor.Name + "";
            SailorRating_Slider.Value = sailor.Rating;
        }

        private Sailor GetSailorObjectFromInput()
        {
            int id = int.Parse(SailorId_TextBox.Text);
            string name = SailorName_TextBox.Text;
            int rating = (int)SailorRating_Slider.Value;
            int age = int.Parse(SailorAge_TextBox.Text);

            Sailor sailor = new Sailor
            {
                Age = age,
                Id = id,
                Name = name,
                Rating = rating
            };
            return sailor;
        }

        private void ClearAllValidationMessages()
        {
            SailorId_ValidationLabel.Content = "";
            SailorName_ValidationLabel.Content = "";
            SailorAge_ValidationLabel.Content = "";
            SailorRating_ValidationLabel.Content = "";
        }

        private bool ValidateSailorAndDisplayMessages(string id, string name, string rating, string age)
        {
            ClearAllValidationMessages();
            bool allPropertiesValid = true;
            string errorMessage;

            if (!SailorValidator.IsIdValid(id, out errorMessage))
            {
                allPropertiesValid = false;
                SailorId_ValidationLabel.Content = errorMessage;
            }

            if (!SailorValidator.IsNameValid(name, out errorMessage))
            {
                allPropertiesValid = false;
                SailorName_ValidationLabel.Content = errorMessage;
            }

            if (!SailorValidator.IsAgeValid(age, out errorMessage))
            {
                allPropertiesValid = false;
                SailorAge_ValidationLabel.Content = errorMessage;
            }

            if (!SailorValidator.IsRatingValid(rating, out errorMessage))
            {
                allPropertiesValid = false;
                SailorRating_ValidationLabel.Content = errorMessage;
            }

            return allPropertiesValid;
        }
        #endregion
    }
}
