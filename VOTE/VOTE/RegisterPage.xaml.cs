using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VOTE.Model;

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
            else if (selectedRole == "Party")
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
            else
            {
                MessageBox.Show("No file selected.");
            }
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            FirstPanel.Visibility = Visibility.Collapsed;
            SecondPanel.Visibility = Visibility.Visible;

        }
        private void previous(object sender, RoutedEventArgs e)
        {
            FirstPanel.Visibility = Visibility.Visible;
            SecondPanel.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Voter user = new Voter(Email.Text, Password.Password, (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(), NID.Text, fname.Text, lname.Text, loc.Text);
            user.assign();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Text))
            {
                MessageBox.Show("Email is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PartyName.Text) || string.IsNullOrWhiteSpace(PartyAcronym.Text))
            {
                MessageBox.Show("Party Name and Acronym are required.");
                return;
            }

            if (legalCertificationData == null)
            {
                MessageBox.Show("Please upload the legal certification file.");
                return;
            }

            DateTime foundedDate = FoundedDate.SelectedDate ?? DateTime.MinValue;

            if (!int.TryParse(MembershipSize.Text, out int membershipSize))
            {
                MessageBox.Show("Please enter a valid membership size.");
                return;
            }

            Party party = new Party(
                Email.Text,
                Password.Password,
                (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                PartyName.Text,
                PartyAcronym.Text,
                foundedDate,
                HeadquartersLocation.Text,
                PartyLeader.Text,
                MembershipCriteria.Text,
                PartyInfo.Text,
                membershipSize,
                ElectionParticipation.Text,
                FundingSources.Text,
                legalCertificationData
            );

            party.assign();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void LoginPageBtn(object sender, MouseButtonEventArgs e)
        {
            LoginPage lp = new LoginPage();
            lp.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
