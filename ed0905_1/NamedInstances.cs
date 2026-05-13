using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ed0905_1
{
    public class NamedPrice
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Value { get; set; }

        public NamedPrice(string productName, int price)
        {
            this.ProductName = productName;
            this.Value = price;
        }

        public NamedPrice(DataRow row)
        {
            var cells = row.ItemArray;
            this.Id = Convert.ToInt32(cells[0]);
            this.ProductName = cells[1].ToString();
            this.Value = Convert.ToInt32(cells[2]);
        }

        public override string ToString()
        {
            return $"{Id}: {ProductName}, цена = {Value}";
        }
    }

    public class NamedOrder
    {
        public int Id { get; set; }
        public string ClientFio { get; set; }
        public DateTime OrderDate { get; set; }

        public NamedOrder(string clientFio, string orderDate)
        {
            this.ClientFio = clientFio;
            this.OrderDate = DateTime.Parse(orderDate);
        }

        public NamedOrder(DataRow row)
        {
            var cells = row.ItemArray;
            this.Id = ((int)cells[0]);
            this.ClientFio = cells[1].ToString();
            this.OrderDate = DateTime.Parse(cells[2].ToString());
        }

        public override string ToString()
        {
            return $"{Id}: {ClientFio}, дата оформ. = {OrderDate.ToShortDateString()}";
        }
    }

}
