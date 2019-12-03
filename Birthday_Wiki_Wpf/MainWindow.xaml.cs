using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
using Hardcodet.Wpf.TaskbarNotification;

namespace Birthday_Wiki_Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static PersonCollection personList; // общий список
        public static PersonCollection today; // список на текущую дату
        public string balloonTitle, balloonText; // заголовок и текст пузыря
        private ObservableCollection<Person> Collection;

        public static RoutedCommand LeftClickCommand = new RoutedCommand();

        //public TaskbarIcon todayNotify;
        public MainWindow()
        {
            InitializeComponent();
            //todayNotify = new TaskbarIcon();
            // показываем пузырь в полночь, если не пуст список today
            MidnightNotifier.DayChanged += (s, e) =>
            {
                // обновляем today
                if (SetBalloonTip())
                    todayNotify.ShowBalloonTip(balloonTitle, balloonText, /*todayNotify.Icon*/BalloonIcon.Info);
            };

            //// работа с треем
            //todayNotify.Visible = false;
            // добавляем событие по двойному клику мышки, 
            // вызывая функцию notifyIcon1_MouseDoubleClick
            //this.todayNotify.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
            //// добавляем событие на изменение окна
            //this.Resize += new System.EventHandler(this.Form1_Resize);

            // создаём новую коллекцию
            personList = new PersonCollection();
            // обновляем список
            RefreshList();
            // выводим в бокс данные
            Collection = new ObservableCollection<Person>(personList.Collection);
            personListBox.Items.Clear();
            personListBox.ItemsSource = Collection;

        }

        // формирование списка сегодняшних именинников для пузыря
        private bool SetBalloonTip()
        {
            todayNotify.Icon = Properties.Resources.if_Gift_669944;

            TodayBdDataRefresh();
            if (today.Collection.Count != 0)
            {
                balloonTitle = "Сегодня!";
                balloonText = "";
                foreach (Person p in today.Collection)
                {
                    if (p != today.Collection.Last())
                        balloonText += p.Name + ", ";
                    else
                    {
                        balloonText += p.Name;
                    }
                }

                return true;
            }
            balloonTitle = "Увы...";
            balloonText = "Сегодня без именинников.";
            return false;
        }

        private void TodayBdDataRefresh() // обновление списка на сегодня
        {
            today = new PersonCollection();
            foreach (Person p in personList.Collection)
            {
                if ((p.birthDate.Day == DateTime.Now.Day) && (p.birthDate.Month == DateTime.Now.Month))
                    today.Collection.Add(p);
            }
        }

        private void TodayNotify_TrayBalloonTipClicked(object sender, RoutedEventArgs e)
        {
            // закрываем уведомление при клике по нему
            todayNotify.CloseBalloon();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // при загрузке приложения собираем текст для пузыря
            SetBalloonTip();
            // выводим пузырь
            todayNotify.ShowBalloonTip(balloonTitle, balloonText, /*todayNotify.Icon*/BalloonIcon.Info);
        }


        ////////
        ///
        /// хэндлы для двойного клика по элементу списка
        ///
        ////////

        private void LeftClickCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void LeftClickExecute(object sender, ExecutedRoutedEventArgs e)
        {
            EditPerson editPerson = new EditPerson();
            editPerson.Owner = this;
            dynamic person = personListBox.SelectedItem;
            editPerson.person = person;
            editPerson.Show();
            e.Handled = true;
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void Button_AddNew_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (personListBox.SelectedItems.Count == 1)
            {
                EditPerson editPerson = new EditPerson();
                editPerson.Owner = this;
                dynamic person = personListBox.SelectedItem;
                editPerson.person = person;
                editPerson.Show();
            }
        }

        private void RefreshList()
        {
            try
            {
                // выгружаем в неё данные из файла
                personList = DeserializePerson.Deserialize("persons.xml");
                // сортируем по дню и месяцу даты рождения
                personList.Collection.Sort((x, y) => x.birthDate.DayOfYear.CompareTo(y.birthDate.DayOfYear));
                // прошедшие помещаем в конец списка
                personList.ReplacePastToEnd();
                MessageBox.Show("Список обновлён.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так!\n\n" + ex.ToString(),
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
