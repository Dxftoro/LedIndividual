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
	public partial class FormOrder : Form
	{
		private NpgsqlConnection connection;
		private List<Client> clients;
		private Order order;

		public FormOrder(NpgsqlConnection connection, Order order)
		{
			this.connection = connection;
			this.clients = new List<Client>();
			this.order = order;

			LoadClients();
			InitializeComponent();

			dateTimeDelivr.Enabled = deliveredCheckBox.Checked;
		}

		private void LoadClients()
		{
			NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM Client", connection);
			System.Data.DataSet dataSet = new System.Data.DataSet();
			adapter.Fill(dataSet);

			System.Data.DataTable dataTable = dataSet.Tables[0];
			foreach (DataRow row in dataTable.Rows)
			{
				var cells = row.ItemArray;
				Client client = new Client(cells[1].ToString(), cells[2].ToString(), cells[3].ToString());
				client.Id = (int)cells[0];
				clients.Add(client);
			}
		}

		private Client GetSelectedClient()
		{
			int index = clientsBox.SelectedIndex;
			if (index >= 0) return clients[index];
			return null;
		}

		private void FormOrder_Load(object sender, EventArgs e)
		{
            foreach (Client client in clients)
            {
                clientsBox.Items.Add(client);
                if (order != null && client.Id == order.IdClient)
                {
                    clientsBox.SelectedIndex = clientsBox.Items.Count - 1;
                }
            }

            if (order == null) return;
            dateTimeOrder.Value = order.OrderDate;
            dateTimeDelivr.Value = order.DeliveryDate ?? DateTime.MinValue;
			deliveredCheckBox.Checked = order.DeliveryDate.HasValue;
        }

        private void InsertOrder(Client client)
		{
			NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Order (id_client, order_date, delivery_date) VALUES (:id_client, :order_date, :delivery_date)", connection);
			command.Parameters.AddWithValue("id_client", client.Id);
			command.Parameters.AddWithValue("order_date", dateTimeOrder.Value);

            if (deliveredCheckBox.Checked)
            {
                command.Parameters.AddWithValue("delivery_date", dateTimeDelivr.Value);
            }
            else
            {
                command.Parameters.AddWithValue("delivery_date", DBNull.Value);
            }

            command.ExecuteNonQuery();
		}

		private void UpdateOrder(Client client)
		{
			NpgsqlCommand command = new NpgsqlCommand("UPDATE Order SET id_client = :id_client, order_date = :order_date, delivery_date = :delivery_date WHERE id = :id", connection);
			command.Parameters.AddWithValue("id", order.Id);
			command.Parameters.AddWithValue("id_client", client.Id);
			command.Parameters.AddWithValue("order_date", dateTimeOrder.Value);

            if (deliveredCheckBox.Checked)
            {
                command.Parameters.AddWithValue("delivery_date", dateTimeDelivr.Value);
            }
            else
            {
                command.Parameters.AddWithValue("delivery_date", DBNull.Value);
            }

            command.ExecuteNonQuery();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			Client client = GetSelectedClient();

			try
			{
				if (order == null) InsertOrder(client);
				else UpdateOrder(client);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			Close();
		}

        private void deliveredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
			dateTimeDelivr.Enabled = deliveredCheckBox.Checked;
        }
    }
}
