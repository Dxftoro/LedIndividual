using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ed0905_1
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ed { get; set; }

        public Product(string name, string ed)
        {
            this.Name = name;
            this.Ed = ed;
        }

        public Product(DataRow row)
        {
            var cells = row.ItemArray;
            this.Id = Convert.ToInt32(cells[0]);
            this.Name = cells[1].ToString();
            this.Ed = cells[2].ToString();
        }

        public override string ToString()
        {
            return $"{Id}: {Name} ({Ed})";
        }
    }

    public class Price
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int Value { get; set; }

        public Price(int idProduct, int price)
        {
            this.IdProduct = idProduct;
            this.Value = price;
        }

        public Price(DataRow row)
        {
            var cells = row.ItemArray;
            this.Id = Convert.ToInt32(cells[0]);
            this.IdProduct = Convert.ToInt32(cells[1]);
            this.Value = Convert.ToInt32(cells[2]);
        }

        public override string ToString()
        {
            return $"{Id}: ID товара = {IdProduct}, цена = {Value}";
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Client(string fio, string phone, string address)
        {
            this.Fio = fio;
            this.Phone = phone;
            this.Address = address;
        }

        public override string ToString()
        {
            return $"{Id}: {Fio}, {Phone}, {Address}";
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int TotalSum { get; set; }

        public Order(int idClient, string orderDate, string deliveryDate)
        {
            this.IdClient = idClient;
            this.OrderDate = DateTime.Parse(orderDate);
            this.DeliveryDate = deliveryDate != null 
                ? DateTime.Parse(deliveryDate) : DateTime.MinValue;
        }
        public Order(int idClient, DateTime orderDate, DateTime? deliveryDate)
        {
            this.IdClient = idClient;
            this.OrderDate = orderDate;
            this.DeliveryDate = deliveryDate != null
                ? deliveryDate : DateTime.MinValue;
        }

        public Order(DataRow row)
        {
            var cells = row.ItemArray;
            this.Id = ((int)cells[0]);
            this.IdClient = Convert.ToInt32(cells[1]);
            this.OrderDate = DateTime.Parse(cells[2].ToString());

            this.DeliveryDate = cells[3] != DBNull.Value
                ? DateTime.Parse(cells[3].ToString()) : (DateTime?)null;

            this.TotalSum = Convert.ToInt32(cells[4]);
        }

        public override string ToString()
        {
            string delivery = DeliveryDate.HasValue 
                ? DeliveryDate.Value.ToShortDateString() : "Not delivered";
            return $"{Id}: ID клиента = {IdClient}, дата оформ. = {OrderDate.ToShortDateString()}, дата дост. = {delivery}, сумма = {TotalSum}";
        }
    }

    public class OrderInfo
    {
        public int Id { get; set; }
        public int IdPrice { get; set; }
        public int IdOrder { get; set; }
        public int Quantity { get; set; }

        public OrderInfo(DataRow row)
        {
            var cells = row.ItemArray;
            this.Id = Convert.ToInt32(cells[0]);
            this.IdPrice = Convert.ToInt32(cells[1]); // !!!
            this.IdOrder = Convert.ToInt32(cells[2]); // !!!
            this.Quantity = Convert.ToInt32(cells[3]);
        }

        public override string ToString()
        {
            return $"{Id}: ID заказа = {IdOrder}, ID цены = {IdPrice}, кол-во = {Quantity}";
        }
    }
}
