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

namespace TryMultiScan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //using (MultiScanContext ctx = new MultiScanContext())
            //{
            //    Customer customer = new Customer { Name = "Walter", PageViews = 0 };
            //    ctx.Customers.Add(customer);
            //    ctx.SaveChanges();
            //}

            //using (MultiScanContext ctx = new MultiScanContext())
            //{
            //    var post = new Post { Name = "How to MultiScan", ViewCount = 0 };
            //    ctx.Posts.Add(post);
            //    ctx.SaveChanges();
            //}
        }
    }
}
