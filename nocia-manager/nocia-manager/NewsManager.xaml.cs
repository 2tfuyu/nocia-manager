using FireSharp.Interfaces;
using FireSharp.Response;
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

namespace nocia_manager
{
    /// <summary>
    /// NewsManager.xaml の相互作用ロジック
    /// </summary>
    public partial class NewsManager : Window
    {
        News news;
        FirebaseDB db = FirebaseDB.GetInstance();

        public NewsManager(ListBox listBox)
        {
            InitializeComponent();
            News data = (News) listBox.SelectedItem;
            news = data;
            Body.Text = data.body;
            TitleText.Text = data.title;
            AuthorText.Text = data.author;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var response = await db.DeleteNews(news.title);
            MessageBox.Show("Successful");
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            var updatedNews = new News
            {
                title = TitleText.Text,
                author = AuthorText.Text,
                date = dt.ToString("yyyy年MM月dd日 HH時mm"),
                body = Body.Text
            };

            var deleteResponse = await db.DeleteNews(news.title);
            var setResponse = await db.AddNews(updatedNews);
            News result = setResponse.ResultAs<News>();

            MessageBox.Show("Successful " + result.title);
        }
    }
}
