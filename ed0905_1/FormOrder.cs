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
    public partial class FormOrder : Form
    {
        private NpgsqlConnection connection;
        private List<Client> clients;
        private List<Product> products;
        private Order order;

        public FormOrder(NpgsqlConnection connection, Order futura)
        {
            this.connection = connection;
            this.clients = new List<Client>();
            this.products = new List<Product>();
            this.futura = futura;
            LoadClients();
            InitializeComponent();
        }
    }
}
