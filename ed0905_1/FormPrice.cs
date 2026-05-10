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
    public partial class FormPrice : Form
    {
        private NpgsqlConnection connection;
        private List<Product> products;
        private Price price;

        public FormPrice(NpgsqlConnection connection, Price price)
        {
            this.connection = connection;
            this.products = new List<Product>();
            this.price = price;

            LoadProducts();
            InitializeComponent();
        }

        private void LoadProducts()
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Product", connection);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            adapter.Fill(dataSet);

            System.Data.DataTable dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                products.Add(new Product(row));
            }
        }

        private Product GetSelectedProduct()
        {
            int index = productBox.SelectedIndex;
            if (index >= 0) return products[index];
            return null;
        }

        private void FormPrice_Load(object sender, EventArgs e)
        {
            foreach (Product product in products)
            {
                productBox.Items.Add(product);
                if (price != null && product.Id == price.IdProduct)
                {
                    productBox.SelectedIndex = productBox.Items.Count - 1;
                }
            }

            if (price == null) return;
            priceBox.Value = price.Value;
        }

        private void InsertFuturaInfo(Product product)
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Price_List (id_product, price) VALUES (:id_product, :price)", connection);
            command.Parameters.AddWithValue("id_product", product.Id);
            command.Parameters.AddWithValue("price", priceBox.Value);
            command.ExecuteNonQuery();
        }

        private void UpdateFuturaInfo(Product product)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE Price_List SET id_product = :id_product, price = :price WHERE id = :id", connection);
            command.Parameters.AddWithValue("id", price.Id);
            command.Parameters.AddWithValue("id_product", product.Id);
            command.Parameters.AddWithValue("price", priceBox.Value);
            command.ExecuteNonQuery();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Product product = GetSelectedProduct();

            try
            {
                if (price == null) InsertFuturaInfo(product);
                else UpdateFuturaInfo(product);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
    }
}
