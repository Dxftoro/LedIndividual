using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            LoadPrices();
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
                prices.Add(new Price(row));
            }
        }

        private void LoadFuturas()
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Order_1", connection);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            adapter.Fill(dataSet);

            System.Data.DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                orders.Add(new Order(row));
            }
        }

        private Price GetSelectedPrice()
        {
            int index = priceBox.SelectedIndex;
            if (index >= 0) return prices[index];
            return null;
        }

        private Order GetSelectedOrder()
        {
            int index = orderBox.SelectedIndex;
            if (index >= 0) return orders[index];
            return null;
        }

        private void FormOrderInfo_Load(object sender, EventArgs e)
        {
            foreach (Price product in prices)
            {
                priceBox.Items.Add(product);
                if (orderInfo != null && product.Id == orderInfo.IdPrice)
                {
                    priceBox.SelectedIndex = priceBox.Items.Count - 1;
                }
            }

            foreach (Order futura in orders)
            {
                orderBox.Items.Add(futura);
                if (orderInfo != null && futura.Id == orderInfo.IdOrder)
                {
                    orderBox.SelectedIndex = orderBox.Items.Count - 1;
                }
            }

            if (orderInfo == null) return;
            countBox.Value = orderInfo.Quantity;
        }

        private void InsertFuturaInfo(Price price, Order order)
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Order_Info (id_price, id_order, quantity) VALUES (:id_price, :id_order, :quantity)", connection);
            command.Parameters.AddWithValue("id_price", price.Id);
            command.Parameters.AddWithValue("id_order", order.Id);
            command.Parameters.AddWithValue("quantity", countBox.Value);
            command.ExecuteNonQuery();
        }

        private void UpdateFuturaInfo(Price price, Order order)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE Order_Info SET id_price = :id_price, id_order = :id_order, quantity = :quantity WHERE id = :id", connection);
            command.Parameters.AddWithValue("id", orderInfo.Id);
            command.Parameters.AddWithValue("id_price", price.Id);
            command.Parameters.AddWithValue("id_order", order.Id);
            command.Parameters.AddWithValue("quantity", countBox.Value);
            command.ExecuteNonQuery();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Price price = GetSelectedPrice();
            Order order = GetSelectedOrder();

            try
            {
                if (orderInfo == null) InsertFuturaInfo(price, order);
                else UpdateFuturaInfo(price, order);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
    }
}
