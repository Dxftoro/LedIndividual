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
        private List<NamedPrice> prices;
        private int idOrder;
        private OrderInfo orderInfo;

        public FormOrderInfo(NpgsqlConnection connection, OrderInfo orderInfo, int idOrder)
        {   
            this.connection = connection;
            this.prices = new List<NamedPrice>();
            this.idOrder = idOrder;
            this.orderInfo = orderInfo;

            LoadFuturas();
            LoadPrices();
            InitializeComponent();
        }

        private void LoadPrices()
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Named_Price_List", connection);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            adapter.Fill(dataSet);

            System.Data.DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                prices.Add(new NamedPrice(row));
            }
        }

        private void LoadFuturas()
        {
        }

        private NamedPrice GetSelectedPrice()
        {
            int index = priceBox.SelectedIndex;
            if (index >= 0) return prices[index];
            return null;
        }

        private void FormOrderInfo_Load(object sender, EventArgs e)
        {
            foreach (NamedPrice product in prices)
            {
                priceBox.Items.Add(product);
                if (orderInfo != null && product.Id == orderInfo.IdPrice)
                {
                    priceBox.SelectedIndex = priceBox.Items.Count - 1;
                }
            }

            if (orderInfo == null) return;
            countBox.Value = orderInfo.Quantity;
        }

        private void InsertFuturaInfo(NamedPrice price)
        {
            //MessageBox.Show($"id_price: {price.Id}, id_order: {idOrder}");
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Order_Info (id_price, id_order, quantity) VALUES (:id_price, :id_order, :quantity)", connection);
            command.Parameters.AddWithValue("id_price", price.Id);
            command.Parameters.AddWithValue("id_order", idOrder);
            command.Parameters.AddWithValue("quantity", countBox.Value);
            command.ExecuteNonQuery();
        }

        private void UpdateFuturaInfo(NamedPrice price)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE Order_Info SET id_price = :id_price, id_order = :id_order, quantity = :quantity WHERE id = :id", connection);
            command.Parameters.AddWithValue("id", orderInfo.Id);
            command.Parameters.AddWithValue("id_price", price.Id);
            command.Parameters.AddWithValue("id_order", idOrder);
            command.Parameters.AddWithValue("quantity", countBox.Value);
            command.ExecuteNonQuery();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            NamedPrice price = GetSelectedPrice();
            if (price == null) return;

            try
            {
                if (orderInfo == null) InsertFuturaInfo(price);
                else UpdateFuturaInfo(price);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
    }
}
