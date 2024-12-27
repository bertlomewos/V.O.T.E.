using System;
using System.IO;
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

        private byte[] legalCertificationData;

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog to select a file
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Read the file into a byte array
                string filePath = openFileDialog.FileName;
                FileNameTextBlock.Text = System.IO.Path.GetFileName(filePath);

                legalCertificationData = File.ReadAllBytes(filePath); // Store file as byte array
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FirstPanel.Visibility = Visibility.Hidden;
            SecondPanel.Visibility = Visibility.Visible;
        }
    }
}
