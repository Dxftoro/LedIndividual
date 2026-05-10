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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ed0905_1
{
    public partial class FormProduct : Form
    {
        NpgsqlConnection connection;
        Product product;

        public FormProduct(NpgsqlConnection connection, Product product)
        {
            InitializeComponent();
            this.connection = connection;
            this.product = product;
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (product == null) return;
            textName.Text = product.Name;
            textEd.Text = product.Ed;
        }

        private void InsertProduct()
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Product (name, ed) VALUES (:name, :ed)", connection);
            command.Parameters.AddWithValue("name", textName.Text);
            command.Parameters.AddWithValue("ed", textEd.Text);
            command.ExecuteNonQuery();
        }

        private void UpdateProduct()
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE Product SET name = :name, ed = :ed WHERE id = :id", connection);
            command.Parameters.AddWithValue("id", product.Id);
            command.Parameters.AddWithValue("name", textName.Text);
            command.Parameters.AddWithValue("ed", textEd.Text);
            command.ExecuteNonQuery();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (product == null) InsertProduct();
                else UpdateProduct();
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message);
            }
            Close();
        }
    }
}
