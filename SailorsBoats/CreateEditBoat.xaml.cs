using SailorsBoats.DAL;
using SailorsBoats.Models;
using SailorsBoats.Util;
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
    /// Interaction logic for CreateEditBoat.xaml
    /// </summary>
    public partial class CreateEditBoat : Window
    {
        private int BoatId = -1;
        private BoatDAL dal;

        public CreateEditBoat()
        {
            InitializeComponent();
            dal = BoatDAL.Instance;

            BoatId_TextBox.Text = (dal.GetAllBoats().Max(x => x.Id) + 1) + "";
            BoatId_TextBox.IsEnabled = false;
        }

        public CreateEditBoat(int BoatId) : this()
        {
            this.BoatId = BoatId;
            DisplayBoatForEdit();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSailorAndDisplayMessages(BoatId_TextBox.Text, BoatName_TextBox.Text,
                GetSelectedBoatColor()))
            {
                return;
            }

            Boat boat = GetBoatObjectFromInput();

            if (BoatId == -1)
            {
                dal.AddBoat(boat);
            }
            else
            {
                dal.UpdateBoat(BoatId, boat);
            }

            Close();
        }

        #region Helpers
        private void DisplayBoatForEdit()
        {
            Boat boat = dal.GetBoat(BoatId);
            BoatId_TextBox.Text = boat.Id + "";
            BoatName_TextBox.Text = boat.Name + "";
            SetSelectedBoatColor(boat.Color);
        }

        private string GetSelectedBoatColor()
        {
            if(BoatColor_RadioButton1.IsChecked.Value)
            {
                return (string)BoatColor_RadioButton1.Content;
            }

            if (BoatColor_RadioButton2.IsChecked.Value)
            {
                return (string)BoatColor_RadioButton2.Content;
            }

            if (BoatColor_RadioButton3.IsChecked.Value)
            {
                return (string)BoatColor_RadioButton3.Content;
            }

            if (BoatColor_RadioButton4.IsChecked.Value)
            {
                return (string)BoatColor_RadioButton4.Content;
            }

            if (BoatColor_RadioButton5.IsChecked.Value)
            {
                return (string)BoatColor_RadioButton5.Content;
            }

            if (BoatColor_RadioButton6.IsChecked.Value)
            {
                return (string)BoatColor_RadioButton6.Content;
            }

            return "";
        }

        private void SelectRadioButtonIfContentMatchesString(RadioButton button, string content)
        {
            if((string)button.Content == content)
            {
                button.IsChecked = true;
            }
        }

        private void SetSelectedBoatColor(string color)
        {
            SelectRadioButtonIfContentMatchesString(BoatColor_RadioButton1, color);
            SelectRadioButtonIfContentMatchesString(BoatColor_RadioButton2, color);
            SelectRadioButtonIfContentMatchesString(BoatColor_RadioButton3, color);
            SelectRadioButtonIfContentMatchesString(BoatColor_RadioButton4, color);
            SelectRadioButtonIfContentMatchesString(BoatColor_RadioButton5, color);
            SelectRadioButtonIfContentMatchesString(BoatColor_RadioButton6, color);
        }

        private Boat GetBoatObjectFromInput()
        {
            int id = int.Parse(BoatId_TextBox.Text);
            string name = BoatName_TextBox.Text;
            string color = GetSelectedBoatColor();

            Boat boat = new Boat
            {
                Id = id,
                Name = name,
                Color = color
            };
            return boat;
        }

        private void ClearAllValidationMessages()
        {
            BoatId_ValidationLabel.Content = "";
            BoatName_ValidationLabel.Content = "";
            BoatColor_ValidationLabel.Content = "";
        }

        private bool ValidateSailorAndDisplayMessages(string id, string name, string color)
        {
            ClearAllValidationMessages();
            bool allPropertiesValid = true;
            string errorMessage, displayMessage = Constants.ValidationMessageHeader + "\n";

            if (!BoatValidator.IsIdValid(id, out errorMessage))
            {
                allPropertiesValid = false;
                displayMessage  += errorMessage + "\n";
            }

            if (!BoatValidator.IsNameValid(name, out errorMessage))
            {
                allPropertiesValid = false;
                displayMessage += errorMessage + "\n";
            }

            if (!BoatValidator.IsColorValid(color, out errorMessage))
            {
                allPropertiesValid = false;
                displayMessage += errorMessage + "\n";
            }

            MessageBox.Show(this, displayMessage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            return allPropertiesValid;
        }
        #endregion
    }
}
