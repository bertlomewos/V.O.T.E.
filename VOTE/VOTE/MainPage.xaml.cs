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
using VOTE.Model;


namespace VOTE
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        private List<Party> Parties = new List<Party>();

        public MainPage()
        {
            InitializeComponent();

            var getFromDb = new GetFromDb();
            getFromDb.GetPartiesForMainPage(Parties, PartiesContainer);

        }

        private void PartyButton_Click(object sender, RoutedEventArgs e)
        {
            PartiesScrollViewer.Visibility = Visibility.Collapsed;
            EventsScrollViewer.Visibility = Visibility.Collapsed;

            PartiesScrollViewer.Visibility = Visibility.Visible;

        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            PartiesScrollViewer.Visibility = Visibility.Collapsed;
            EventsScrollViewer.Visibility = Visibility.Collapsed;


            EventsScrollViewer.Visibility = Visibility.Visible;

        }
    }
    }
