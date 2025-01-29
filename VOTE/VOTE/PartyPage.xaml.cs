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
using System.Windows.Shapes;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using VOTE.Model;

namespace VOTE
{
    /// <summary>
    /// Interaction logic for PartyPage.xaml
    /// </summary>
    public partial class PartyPage : Window
    {
        public static string UID;
        private bool isEditMode = false; // Track whether we're in edit mode
        private byte[] legalCertificationByteArray;
        public byte[] LegalCertification { get; set; } 

        public PartyPage()
        {
            InitializeComponent();
            LoadPartyData();

        }
        private void LoadPartyData()
        {
            // Load the party data from the database and populate the UI with the data
            GetFromDb gd = new GetFromDb();
            gd.GetParties(UID);

            if (gd.partyList.Count > 0)
            {
                PartyNameLabel.Content = gd.partyList.ElementAtOrDefault(0) ?? "N/A";
                PartyAcronymLabel.Content = gd.partyList.ElementAtOrDefault(1) ?? "N/A";
                FoundLabel.Content = gd.partyList.ElementAtOrDefault(2) ?? "N/A";
                HeadquartersLocationLabel.Content = gd.partyList.ElementAtOrDefault(3) ?? "N/A";
                PartyLeaderLabel.Content = gd.partyList.ElementAtOrDefault(4) ?? "N/A";
                MembershipCriteriaLabel.Content = gd.partyList.ElementAtOrDefault(5) ?? "N/A";
                PartyInfoLabel.Content = gd.partyList.ElementAtOrDefault(6) ?? "N/A";
                MembershipSizeLabel.Content = gd.partyList.ElementAtOrDefault(7) ?? "N/A";
                ElectionParticipationLabel.Content = gd.partyList.ElementAtOrDefault(8) ?? "N/A";
                FundingSourcesLabel.Content = gd.partyList.ElementAtOrDefault(9) ?? "N/A";
                CretificationLabel.Content = gd.partyList.ElementAtOrDefault(10) ?? "N/A";

                // Populate textboxes for editing
                PartyNameTextBox.Text = gd.partyList.ElementAtOrDefault(0) ?? string.Empty;
                PartyAcronymTextBox.Text = gd.partyList.ElementAtOrDefault(1) ?? string.Empty;
                FoundedTextBox.Text = gd.partyList.ElementAtOrDefault(2) ?? string.Empty;
                HeadquartersLocationTextBox.Text = gd.partyList.ElementAtOrDefault(3) ?? string.Empty;
                PartyLeaderTextBox.Text = gd.partyList.ElementAtOrDefault(4) ?? string.Empty;
                MembershipCriteriaTextBox.Text = gd.partyList.ElementAtOrDefault(5) ?? string.Empty;
                PartyInfoTextBox.Text = gd.partyList.ElementAtOrDefault(6) ?? string.Empty;
                MembershipSizeTextBox.Text = gd.partyList.ElementAtOrDefault(7) ?? string.Empty;
                ElectionParticipationTextBox.Text = gd.partyList.ElementAtOrDefault(8) ?? string.Empty;
                FundingSourcesTextBox.Text = gd.partyList.ElementAtOrDefault(9) ?? string.Empty;

                // Handle legal certification file
                object legalCertificationObject = gd.partyList.ElementAtOrDefault(10);
                if (legalCertificationObject is byte[] byteArray && byteArray.Length > 0)
                {
                    string savePath = @"C:\LegalCertification\legal_certification.pdf";
                    try
                    {
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(savePath)); // Ensure directory exists
                        File.WriteAllBytes(savePath, byteArray);
                        MessageBox.Show("Legal Certification saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving legal certification: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    CretificationLabel.Content = "No Legal Certification Available";
                }
            }
            else
            {
                MessageBox.Show("No data available for the selected party.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEditMode)
            {
                // Save Mode: Update labels with data from textboxes
                PartyNameLabel.Content = PartyNameTextBox.Text;
                PartyAcronymLabel.Content = PartyAcronymTextBox.Text;
                HeadquartersLocationLabel.Content = HeadquartersLocationTextBox.Text;
                PartyLeaderLabel.Content = PartyLeaderTextBox.Text;
                MembershipCriteriaLabel.Content = MembershipCriteriaTextBox.Text;
                PartyInfoLabel.Content = PartyInfoTextBox.Text;
                MembershipSizeLabel.Content = MembershipSizeTextBox.Text;
                ElectionParticipationLabel.Content = ElectionParticipationTextBox.Text;
                FundingSourcesLabel.Content = FundingSourcesTextBox.Text;

                if (legalCertificationByteArray != null)
                {
                    // Save the byte array to the database 
                    LegalCertification = legalCertificationByteArray; 
                }

                SavePartyData();

                // Switch back to view mode
                EditPanel.Visibility = Visibility.Collapsed;
                InfoPanel.Visibility = Visibility.Visible;
                UpdateButton.Content = "Update";
                isEditMode = false;
            }
            else
            {
                // Edit Mode: Switch to the edit panel
                EditPanel.Visibility = Visibility.Visible;
                InfoPanel.Visibility = Visibility.Collapsed;
                UpdateButton.Content = "Save";
                isEditMode = true;
            }
        }

        private void SavePartyData()
        {
            SendToDb sd = new SendToDb();

            if (int.TryParse(MembershipSizeTextBox.Text, out int membershipSize))
            {
                int userId = sd.InsertINtoUsers(UID, "", "parties");

                // Check if the party exists by checking the PartyId or PartyName
                int partyId = CheckPartyByName(PartyNameTextBox.Text);

                if (partyId > 0) // Party exists, update it
                {
                    sd.UpdateParty(partyId, PartyNameTextBox.Text, PartyAcronymTextBox.Text, FoundedTextBox.Text,
                                   HeadquartersLocationTextBox.Text, PartyLeaderTextBox.Text, PartyInfoTextBox.Text,
                                   MembershipCriteriaTextBox.Text, membershipSize, ElectionParticipationTextBox.Text,
                                   FundingSourcesTextBox.Text, legalCertificationByteArray, userId);

                    MessageBox.Show("Party details updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else 
                {
                    
                }
            }
            else
            {
                MessageBox.Show("Membership size must be a number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public int CheckPartyByName(string partyName)
        {
            int partyId = 0;
            string query = "SELECT PartyId FROM parties WHERE PartyName = @partyName";

            using (MySqlConnection connection = new MySqlConnection(Dbconn.connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@partyName", partyName);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        partyId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking party: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return partyId;
        }


        private void Profile(object sender, RoutedEventArgs e)
        {
                    
        }

        private void LegalCertificationButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                CertificationFileText.Text = System.IO.Path.GetFileName(filePath); // Display selected file name

                // Store the file as a byte array
                legalCertificationByteArray = File.ReadAllBytes(filePath);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.Show();
        }
    }
}
