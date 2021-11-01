using AlphawolfSoftware.Databases;
using AlphawolfSoftware.State;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreateReactFromDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainState _ms = new MainState();

        public MainWindow()
        {
            InitializeComponent();

            butCreate.IsEnabled = false;
        }

        private void butCreate_Click(object sender, RoutedEventArgs e)
        {
            UpdateState();

            ReactComponentGenerator react = new ReactComponentGenerator(_ms);
            react.Generate();

            string filename = cboTables.SelectedItem.ToString() + @".XML"; 

            react.Save(txtOutput.Text, filename);
            lblCreate.Content = "Created!";
        }

        private void UpdateState()
        {
            _ms.IsList = (bool)chkList.IsChecked;
            _ms.IsCreate = (bool)chkCreate.IsChecked;
            _ms.IsRead = (bool)chkRead.IsChecked;
            _ms.IsUpdate = (bool)chkUpdate.IsChecked;
            _ms.IsDelete = (bool)chkDelete.IsChecked;
            _ms.IsIntelligent = true;

            _ms.OutputFolder = txtOutput.Text;
            if (cboTables.SelectedIndex > -1)
            {
                _ms.Table = cboTables.SelectedItem.ToString();
            }

            _ms.ConnectionString = txtConnection.Text;
        }

        private void RefreshState()
        {
            chkList.IsChecked = _ms.IsList;
            chkCreate.IsChecked = _ms.IsCreate;
            chkRead.IsChecked = _ms.IsRead;
            chkUpdate.IsChecked = _ms.IsUpdate;
            chkDelete.IsChecked = _ms.IsDelete;
        }

        private void butConnect_Click(object sender, RoutedEventArgs e)
        {
            lblConnect.Content = "Connecting...";

            UpdateState();

            ReactComponentGenerator react = new ReactComponentGenerator(_ms);
            IList<string> list = react.GetTables();

            cboTables.ItemsSource = list;
            cboTables.SelectedIndex = 0;

            butCreate.IsEnabled = true;

            lblConnect.Content = "Connected!";
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double wdth = e.NewSize.Width - 30;
            txtConnection.Width = wdth;
            cboTables.Width = wdth;
            txtOutput.Width = wdth;
            grdFields.Width = wdth;

            double hght = e.NewSize.Height - 350;
            grdFields.Height = hght;
        }

        private void cboTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ReactComponentGenerator react = new ReactComponentGenerator(_ms);
            react.State = new MainState();

            string filename = cboTables.SelectedItem.ToString() + @".XML";
            react.Load(txtOutput.Text, filename);

            if (react.State.ListFields == null)
            {
                react.State.ListFields = react.GetTableFields(cboTables.SelectedItem.ToString());
            }
            _ms = react.State;

            RefreshState();

            grdFields.ItemsSource = null;
            grdFields.ItemsSource = react.State.ListFields.OrderBy(x => x.ORDINAL_POSITION).ToList();
        }

    }
}
