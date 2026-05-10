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
    public partial class FormClient : Form
    {
        private NpgsqlConnection connection;
        private Client client;

        public FormClient(NpgsqlConnection connection, Client client)
        {
            InitializeComponent();
            this.connection = connection;
            this.client = client;
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            if (client == null) return;
            textFio.Text = client.Fio;
            textAddress.Text = client.Address;
            textPhone.Text = client.Phone;
        }

        private void InsertClient()
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Client (name, address, phone) VALUES (:name, :address, :phone)", connection);
            command.Parameters.AddWithValue("fio", textFio.Text);
            command.Parameters.AddWithValue("address", textAddress.Text);
            command.Parameters.AddWithValue("phone", textPhone.Text);
            command.ExecuteNonQuery();
        }

        private void UpdateClient()
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE Client SET name = :name, address = :address, phone = :phone WHERE id = :id", connection);
            command.Parameters.AddWithValue("id", client.Id);
            command.Parameters.AddWithValue("fio", textFio.Text);
            command.Parameters.AddWithValue("address", textAddress.Text);
            command.Parameters.AddWithValue("phone", textPhone.Text);
            command.ExecuteNonQuery();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (client == null) InsertClient();
                else UpdateClient();
            }
            catch (Exception exc)
            {
                DialogResult result = MessageBox.Show(exc.Message);
            }
            Close();
        }
    }
}
