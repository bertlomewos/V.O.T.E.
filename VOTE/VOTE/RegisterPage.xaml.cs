using System;
using System.Windows;
using System.Windows.Controls;

namespace VOTE
{
    public partial class RegisterPage : Window
    {
        // Keep track of the current page
        private int currentPage = 0;

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedRole = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedRole == "Voter")
            {
                UserPanel.Visibility = Visibility.Hidden;
                VoterPanel.Visibility = Visibility.Visible;
            }
            else if(selectedRole == "Party")
            {
                UserPanel.Visibility = Visibility.Hidden;
                PartyPanel.Visibility = Visibility.Visible;
            }
        }
    }
}
