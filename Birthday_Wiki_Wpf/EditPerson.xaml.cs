using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Birthday_Wiki_Wpf
{
    /// <summary>
    /// Логика взаимодействия для EditPerson.xaml
    /// </summary>
    public partial class EditPerson : Window
    {
        public Person person;
        public EditPerson()
        {
            InitializeComponent();
        }

        private void DataGrid_groupList_Loaded(object sender, RoutedEventArgs e)
        {
            if (person != null)
            {
                textBox_Name.Text = person.Name;
                datePicker_bDate.DisplayDate = person.BirthDate.Date;
                datePicker_bDate.Text = person.BirthDate.Date.ToShortDateString();
                dataGrid_groupList.ItemsSource = person.GroupList;
                //if (person.GroupList.Count != 0)
                //{
                //    //DataGridTextColumn groupColumn = new DataGridTextColumn();
                //    //groupColumn.Header = "Группа";
                //    //groupColumn.Binding = new Binding("test");
                //    dataGrid_groupList.ItemsSource = person.GroupList;
                //}
                //else
                //{
                //    grid_GroupList.Visibility = Visibility.Collapsed;
                //    //dataGrid_groupList.Visibility = Visibility.Hidden;
                //}
            }
        }

    }
}
