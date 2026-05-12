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
    public partial class FormReport : Form
    {
        private NpgsqlConnection connection;
        private ClientReportView adapter;
        private List<Client> clients;

        public FormReport(NpgsqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.adapter = new ClientReportView();
            this.clients = new List<Client>();
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
                clientsBox.Items.Add(client);
            }
        }

        private Client GetSelectedClient()
        {
            int index = clientsBox.SelectedIndex;
            if (index >= 0) return clients[index];
            return null;
        }

        public void UpdateDataGrid(int idClient, DateTime orderDate)
        {
            try
            {
                adapter.IdClient = idClient;
                adapter.OrderDate = orderDate;
                adapter.Setup(connection, reportGrid);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            LoadClients();
        }

        private void buttonFetch_Click(object sender, EventArgs e)
        {
            Client client = GetSelectedClient();
            if (client == null) { MessageBox.Show("Client is not selected!"); Close(); }
            UpdateDataGrid(client.Id, dateTimeBorder.Value);
        }
    }
}
