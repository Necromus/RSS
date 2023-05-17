using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static WpfApp2.AddOrderPage;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();

        public MainWindow()
        {
            InitializeComponent();

            //Загрузка данных в таблицы DataGrid
            AcceptedStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Принят").ToList();
            StorageStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "На склад").ToList();
            SoldStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Продан").ToList();
            OrderDataGrid.ItemsSource = db.Orders.ToList();

            foreach (var item in db.Status)
            {
                ComboBoxOrder.Items.Add(item.stat);
            }

            //Подсчёт количества товара
            SumOrderCount();
        }

        public void SumOrderCount()
        {
            int count = 0;

            //Подсчёт количества товара
            foreach (var ProductCount in OrderDataGrid.Items.OfType<Order>())
            {
                count = count + ProductCount.productCount;
            }
            SumProductTextBlock.Text = "Общее количество товаров: " + count.ToString();
        }

        //Функция кнопки добавления товара
        public void AddOrderButton(object sender, RoutedEventArgs e)
        {
            AddOrderPage addOrderPage = new AddOrderPage(db);

            //Вывод модального окна
            var r = addOrderPage.ShowDialog();

            //Обновление таблицы после добавления нового товара
            if (r == true)
            {
                AcceptedStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Принят").ToList();
            }

        }

        //Флаг
        public bool flagfix = true;

        //Функция необходимая для возможности вненсения изменений дат от-до в таблице формы Принят
        public void AcceptedStatusOrderDataGridCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (flagfix)
                {
                    int numRow = e.Row.GetIndex();
                    int numCol = e.Column.DisplayIndex;

                    //Получение даты из ячейки
                    var Date = AcceptedStatusOrderDataGrid.Columns[numCol].GetCellContent(AcceptedStatusOrderDataGrid.Items[numRow]) as TextBox;

                    var orderItem = (Order)AcceptedStatusOrderDataGrid.SelectedItem;

                    //Загрузка даты от в бд
                    if (numCol == 4)
                    {
                        Order order = (from x in db.Orders
                                       where x.id == orderItem.id
                                       select x).First();

                        DateTime date = DateTime.Parse(Date.Text);

                        order.orderDataFrom = date.Date;

                        db.SaveChanges();


                        AcceptedStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Принят").ToList();
                    }

                    //Загрузка даты до в бд
                    if (numCol == 5)
                    {
                        Order order = (from x in db.Orders
                                       where x.id == orderItem.id
                                       select x).First();

                        DateTime date = DateTime.Parse(Date.Text);

                        order.orderDataTo = date.Date;

                        db.SaveChanges();


                        AcceptedStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Принят").ToList();
                    }

                    flagfix = false;
                    AcceptedStatusOrderDataGrid.CancelEdit();
                    AcceptedStatusOrderDataGrid.CancelEdit();
                    flagfix = true;
                }
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты","Ошибка");
            }
        }

        //Функция необходимая для возможности вненсения изменений дат от-до в таблице формы Склад
        public void StorageStatusOrderDataGridCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (flagfix)
                {
                    int numRow = e.Row.GetIndex();
                    int numCol = e.Column.DisplayIndex;

                    //Получение даты из ячейки
                    var Date = StorageStatusOrderDataGrid.Columns[numCol].GetCellContent(StorageStatusOrderDataGrid.Items[numRow]) as TextBox;

                    var orderItem = (Order)StorageStatusOrderDataGrid.SelectedItem;

                    //Загрузка даты от в бд
                    if (numCol == 4)
                    {
                        Order order = (from x in db.Orders
                                       where x.id == orderItem.id
                                       select x).First();

                        DateTime date = DateTime.Parse(Date.Text);

                        order.orderDataFrom = date.Date;

                        db.SaveChanges();


                        StorageStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "На склад").ToList();
                    }

                    //Загрузка даты до в бд
                    if (numCol == 5)
                    {
                        Order order = (from x in db.Orders
                                       where x.id == orderItem.id
                                       select x).First();

                        DateTime date = DateTime.Parse(Date.Text);

                        order.orderDataTo = date.Date;

                        db.SaveChanges();


                        StorageStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "На склад").ToList();
                    }

                    flagfix = false;
                    StorageStatusOrderDataGrid.CancelEdit();
                    StorageStatusOrderDataGrid.CancelEdit();
                    flagfix = true;
                }
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты", "Ошибка");
            }
        }

        //Функция необходимая для возможности вненсения изменений дат от-до в таблице формы Продан
        public void SoldStatusOrderDataGridCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (flagfix)
                {
                    int numRow = e.Row.GetIndex();
                    int numCol = e.Column.DisplayIndex;

                    //Получение даты из ячейки
                    var Date = SoldStatusOrderDataGrid.Columns[numCol].GetCellContent(SoldStatusOrderDataGrid.Items[numRow]) as TextBox;

                    var orderItem = (Order)SoldStatusOrderDataGrid.SelectedItem;

                    //Загрузка даты от в бд
                    if (numCol == 4)
                    {
                        Order order = (from x in db.Orders
                                       where x.id == orderItem.id
                                       select x).First();

                        DateTime date = DateTime.Parse(Date.Text);

                        order.orderDataFrom = date.Date;

                        db.SaveChanges();


                        SoldStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Продан").ToList();
                    }

                    //Загрузка даты до в бд
                    if (numCol == 5)
                    {
                        Order order = (from x in db.Orders
                                       where x.id == orderItem.id
                                       select x).First();

                        DateTime date = DateTime.Parse(Date.Text);

                        order.orderDataTo = date.Date;

                        db.SaveChanges();


                        SoldStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Продан").ToList();
                    }

                    flagfix = false;
                    SoldStatusOrderDataGrid.CancelEdit();
                    SoldStatusOrderDataGrid.CancelEdit();
                    flagfix = true;
                }
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты", "Ошибка");
            }
        }

        //Функция необходимая для обновления таблицы при преходе в форму Принят
        public void TabItemAccepted_Clicked(object sender, RoutedEventArgs e)
        {

            foreach (var entry in db.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            AcceptedStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Принят").ToList();
        }

        //Функция необходимая для обновления таблицы при преходе в форму Склад
        public void TabItemStorage_Clicked(object sender, RoutedEventArgs e)
        {

            foreach (var entry in db.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            StorageStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "На склад").ToList();
        }

        //Функция необходимая для обновления таблицы при преходе на форму Продан
        public void TabItemSold_Clicked(object sender, RoutedEventArgs e)
        {

            foreach (var entry in db.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            SoldStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Продан").ToList();
        }

        //Функция необходимая для возможности по нажатию правой кнопки мыши перевести товар из статуса Принят в статус На склад
        public void ChangeStatusToStorage(object sender, RoutedEventArgs e) 
        {
            //Получение id
            var orderItem = (Order)AcceptedStatusOrderDataGrid.SelectedItem;

            //Update перевод товара из статуса Принят в статус На склад
            Order order = (from x in db.Orders
                           where x.id == orderItem.id
                           select x).First();
            order.orderStatus = "На склад";
            order.orderDataFrom = null;
            order.orderDataTo = null;
            db.SaveChanges();

            AcceptedStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "Принят").ToList();
        }

        //Функция необходимая для возможности по нажатию правой кнопки мыши перевести товар из статуса На склад в статус Продан
        public void ChangeStatusToSold(object sender, RoutedEventArgs e)
        {
            //Получение id
            var orderItem = (Order)StorageStatusOrderDataGrid.SelectedItem;

            //Update перевод товара из статуса На склад в статус Продан
            Order order = (from x in db.Orders
                           where x.id == orderItem.id
                           select x).First();

            order.orderStatus = "Продан";
            order.orderDataFrom = null;
            order.orderDataTo = null;

            db.SaveChanges();

            StorageStatusOrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == "На склад").ToList();
        }

        //Функция кнопки обновления 
        public void ButtonClickUpdate(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? t1 = null;
                DateTime? t2 = null;

                //Статус все
                if (ComboBoxOrder.SelectedItem.ToString() == "Все")
                    OrderDataGrid.ItemsSource = db.Orders.ToList();
                else
                    //Все остальные статусы
                    OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == ComboBoxOrder.SelectedItem.ToString()
                    && s.orderDataFrom == t1 && s.orderDataTo == t2
                    ).ToList();

                //Вариант при отстутсвии даты от до
                if (TextBoxDateFrom.Text == "" && TextBoxDateTo.Text == "")
                {
                    t1 = null;
                    t2 = null;

                    if (ComboBoxOrder.SelectedItem.ToString() == "Все")
                        OrderDataGrid.ItemsSource = db.Orders.ToList();
                    else
                        OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == ComboBoxOrder.SelectedItem.ToString()).ToList();
                }

                //Вариант при наличии даты от и отстутсвии даты до
                if (TextBoxDateFrom.Text != "" && TextBoxDateTo.Text == "")
                {
                    t1 = DateTime.ParseExact(TextBoxDateFrom.Text, "dd-MM-yyyy", null);
                    t2 = null;

                    if (ComboBoxOrder.SelectedItem.ToString() == "Все")
                        OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderDataFrom >= t1).ToList();
                    else
                        OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == ComboBoxOrder.SelectedItem.ToString()
                        && s.orderDataFrom >= t1
                        ).ToList();
                }

                //Вариант при наличии даты до и отстутсвии даты от
                if (TextBoxDateFrom.Text == "" && TextBoxDateTo.Text != "")
                {
                    t1 = null;
                    t2 = DateTime.ParseExact(TextBoxDateTo.Text, "dd-MM-yyyy", null);

                    if (ComboBoxOrder.SelectedItem.ToString() == "Все")
                        OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderDataTo <= t2).ToList();
                    else
                        OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == ComboBoxOrder.SelectedItem.ToString()
                        && s.orderDataTo <= t2
                        ).ToList();
                }

                //Вариант при наличии даты от и до
                if (TextBoxDateFrom.Text != "" && TextBoxDateTo.Text != "")
                {
                    t1 = DateTime.ParseExact(TextBoxDateFrom.Text, "dd-MM-yyyy", null);
                    t2 = DateTime.ParseExact(TextBoxDateTo.Text, "dd-MM-yyyy", null);

                    if (ComboBoxOrder.SelectedItem.ToString() == "Все")
                        OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderDataFrom >= t1 && s.orderDataTo <= t2).ToList();
                    else
                        OrderDataGrid.ItemsSource = db.Orders.Where(s => s.orderStatus == ComboBoxOrder.SelectedItem.ToString()
                        && s.orderDataFrom >= t1 && s.orderDataTo <= t2
                        ).ToList();
                }

                //Подсчёт количества товара
                SumOrderCount();
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты", "Ошибка");
            }
        }

    }
}
