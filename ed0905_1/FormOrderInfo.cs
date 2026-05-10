using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ed0905_1
{
    public partial class FormOrderInfo : Form
    {
        private NpgsqlConnection connection;
        private List<Price> prices;
        private List<Order> orders;
        private OrderInfo orderInfo;

        public FormOrderInfo(NpgsqlConnection connection, OrderInfo orderInfo)
        {   
            this.connection = connection;
            this.prices = new List<Price>();
            this.orders = new List<Order>();
            this.orderInfo = orderInfo;

            LoadFuturas();
            LoadProducts();
            InitializeComponent();
        }

        private void LoadPrices()
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Price_List", connection);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            adapter.Fill(dataSet);

            System.Data.DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product(row));
            }
        }

        private void LoadFuturas()
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Order", connection);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            adapter.Fill(dataSet);

            System.Data.DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order(row));
            }
        }

        private Price GetSelectedProduct()
        {
            int index = priceBox.SelectedIndex;
            if (index >= 0) return prices[index];
            return null;
        }

        private Order GetSelectedFutura()
        {
            int index = orderBox.SelectedIndex;
            if (index >= 0) return orders[index];
            return null;
        }

    }
}
