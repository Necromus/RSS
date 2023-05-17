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
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Window
    {

        ApplicationContext db;
        List<Products> products = new List<Products>();

        public AddOrderPage(ApplicationContext _db)
        {
            InitializeComponent();
            db = _db;

            foreach (var item in db.Products)
            {
                products.Add(new Products { productName = item.productName, companyName = item.companyName });
            }
            foreach(var item in products)
            {
                ComboBoxProduct.Items.Add(item);
            }
        }

        public class Products
        {
            public string productName { get; set; }
            public string companyName { get; set; }
            public override string ToString() => $"{productName} ({companyName})";
        }

        //Функция кнопки добавления товара
        public void AddOrderButton(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = ComboBoxProduct.SelectedItem.ToString();

                //Получение id выбранного товара
                int id = (from x in db.Products
                              where x.productName == str.Substring(0, str.IndexOf('('))
                              select x.id).First();

                Order oreder = new Order { productId = id, productCount = Int32.Parse(productCountTextBox.Text), orderStatus = "Принят", orderDataFrom = null, orderDataTo = null };

                db.Orders.Add(oreder);

                db.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Введите данные", "Ошибка");
            }

        }

    }
}
