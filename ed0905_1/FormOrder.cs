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
        private NamedOrderInfoDataAdapter adapter;
        private bool orderChanged, initializing;

        public FormOrder(NpgsqlConnection connection, Order order)
		{
			this.connection = connection;
			this.clients = new List<Client>();
			this.order = order;
			this.adapter = new NamedOrderInfoDataAdapter();
            this.orderChanged = false;
            this.initializing = false;

			InitializeComponent();

            SetOrderChanged(false);
            LoadClients();

            dateTimeDelivr.Enabled = deliveredCheckBox.Checked;
            if (order != null)
            {
                SetOrderInfoPanelVisible(true);
                UpdateData();
            }
            else
            {
                SetOrderInfoPanelVisible(false);
            }
        }

		public void SetOrderInfoPanelVisible(bool visible)
		{
			orderInfoPanel.Visible = visible;
            orderInfoPanel.Enabled = visible;
		}

        public void SetOrderChanged(bool changed)
        {
            if (initializing == true) return;
            orderChanged = changed;
            buttonOk.Enabled = changed;
            //MessageBox.Show($"CHANGED: {changed}");
        }

        private void UpdateData()
        {
            adapter.Setup(connection, orderInfoGrid, order.Id);
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
            initializing = true;

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
			dateTimeDelivr.Value = order.DeliveryDate ?? dateTimeDelivr.MinDate;
			deliveredCheckBox.Checked = order.DeliveryDate.HasValue;

            initializing = false;
            SetOrderChanged(false);
        }

        private void InsertOrder(Client client)
		{
			NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Order_1 (id_client, order_date, delivery_date) VALUES (:id_client, :order_date, :delivery_date)", connection);
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
			NpgsqlCommand command = new NpgsqlCommand("UPDATE Order_1 SET id_client = :id_client, order_date = :order_date, delivery_date = :delivery_date WHERE id = :id", connection);
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
            if (!orderChanged) return;
			Client client = GetSelectedClient();

			try
			{
				if (order == null) InsertOrder(client);
				else UpdateOrder(client);
                SetOrderInfoPanelVisible(true);
                SetOrderChanged(false);
            }
            catch (Exception exc)
			{
				MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void deliveredCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
			dateTimeDelivr.Enabled = deliveredCheckBox.Checked;

            SetOrderChanged(true);
        }

        private void dateTimeDelivr_ValueChanged(object sender, EventArgs e)
        {
            SetOrderChanged(true);
        }

        private void dateTimeOrder_ValueChanged(object sender, EventArgs e)
        {
            SetOrderChanged(true);
        }

        private void clientsBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetOrderChanged(true);
        }

        private void clientsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            Form additionForm = adapter.CreateInstanceForm(connection, null);
            DialogResult result = additionForm.ShowDialog();
            if (result == DialogResult.OK) UpdateData();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Form additionForm = adapter.CreateInstanceForm(connection,
                orderInfoGrid.SelectedRows.Count > 0 ? orderInfoGrid.SelectedRows[0] : null);
            DialogResult result = additionForm.ShowDialog();
            if (result == DialogResult.OK) UpdateData();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in orderInfoGrid.SelectedRows)
                {
                    DataRowView rowView = row.DataBoundItem as DataRowView;
                    adapter.DeleteByDataRow(connection, rowView.Row);
                }
                UpdateData();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void orderInfoGrid_SelectionChanged(object sender, EventArgs e)
        {
            updateButton.Enabled = orderInfoGrid.SelectedRows.Count == 1;
            deleteButton.Enabled = orderInfoGrid.SelectedRows.Count > 0;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.OK;
            }
            base.OnFormClosing(e);
        }
    }
}
