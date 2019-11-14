using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace nocia_manager
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<News> _customers = new ObservableCollection<News>();

        public MainWindow()
        {
            InitializeComponent();
            new FirebaseDB();
            init();
        }

        private async void init()
        {
            var db = FirebaseDB.GetInstance();
            var all_news = await db.GetAllNews();
            var mList = JsonConvert.DeserializeObject<IDictionary<string, News>>(all_news.Body);
            if (mList != null)
            {
                foreach (News data in mList.Values)
                {
                    _customers.Add(data);
                }
                NewsList.ItemsSource = _customers;
            }
        }

        private async void Upload_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            var data = new News
            {
                title = Title.Text,
                author = Author.Text,
                date = dt.ToString("yyyy年MM月dd日 HH時mm"),
                body = Body.Text
            };
            var db = FirebaseDB.GetInstance();
            var response = await db.AddNews(data);
            News result = response.ResultAs<News>();

            MessageBox.Show("Successful " + result.title);
            _customers.Add(result);
            NewsList.Items.Refresh();
        }

        private void NewsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox targetItem = (ListBox) sender;

            News data = (News)targetItem.SelectedItem;

            if (data != null)
            {
                NewsManager window = new NewsManager(targetItem);
                window.Show();
                this.Close();
                /*
                FirebaseResponse response = await client.DeleteAsync("news/" + data.title);
                MessageBox.Show("Successful");
                _customers.RemoveAt(_customers.IndexOf((News) targetItem.SelectedItem));
                NewsList.Items.Refresh();
                */
            }
        }
    }
}
