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

namespace PartyOrganizer
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void cbEventsListLoaded(object sender, RoutedEventArgs e)
        {
            List<Event> eventsList = DataBase.GetEventsList();
            cbEventsList.ItemsSource = eventsList;
        }

        private void cbEventsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(Convert.ToInt32(cbEventsList.SelectedValue));
            mainWindow.Show();
            this.Close();
        }

        private void btnCreateNewEvent_Click(object sender, RoutedEventArgs e)
        {
            CreateEventWindow cvWin = new CreateEventWindow();
            cvWin.Show();
            this.Close();
        }
    }
}
