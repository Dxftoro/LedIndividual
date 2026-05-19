using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ed0905_1
{
    public abstract class BaseDataAdapter
    {
        protected string tableName;
        protected System.Data.DataTable dataTable;
        protected System.Data.DataSet dataSet;

        public BaseDataAdapter()
        {
            this.dataTable = new System.Data.DataTable();
            this.dataSet = new System.Data.DataSet();
        }

        public abstract void Setup(NpgsqlConnection connection, DataGridView view);

        public string GetTableName() { return tableName; }
        public System.Data.DataTable GetDataTable() { return dataTable; }

        protected NpgsqlDataAdapter FillBy(string query, NpgsqlConnection connection)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
            dataSet.Reset();
            adapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            //MessageBox.Show("" + dataTable.Rows.Count);
            return adapter;
        }

        protected NpgsqlDataAdapter FillBy(NpgsqlCommand command)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            dataSet.Reset();
            adapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];
            return adapter;
        }
    }

    public abstract class TableDataAdapter : BaseDataAdapter
    {
        public TableDataAdapter(string tableName)
        {
            this.tableName = tableName;
        }

        public override void Setup(NpgsqlConnection connection, DataGridView view)
        {
            NpgsqlDataAdapter adapter = FillBy($"SELECT * FROM {tableName}", connection);
            OnSetup(adapter, connection, view);
        }

        public void DeleteByDataRow(NpgsqlConnection connection, DataRow row, string referencedTable = null)
        {
            string query = "DELETE FROM " + (referencedTable == null ? tableName : referencedTable) + " WHERE ID = :id";
            //MessageBox.Show(query);

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("id", Convert.ToInt32(row.ItemArray[0]));
            command.ExecuteNonQuery();
        }

        public abstract void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view);
        public abstract Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row);
    }

    public class ClientDataAdapter : TableDataAdapter
    {
        public ClientDataAdapter() : base("Client")
        {
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "ФИО";
            view.Columns[2].HeaderText = "Адрес";
            view.Columns[3].HeaderText = "Телефон";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormClient(connection, null);

            Client client = new Client(
                row.Cells[1].Value.ToString(),
                row.Cells[2].Value.ToString(),
                row.Cells[3].Value.ToString()
            );
            client.Id = Convert.ToInt32(row.Cells[0].Value);

            return new FormClient(connection, client);
        }
    }

    public class ProductDataAdapter : TableDataAdapter
    {
        public ProductDataAdapter() : base("Product")
        {
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "Название";
            view.Columns[2].HeaderText = "Ед. измерения";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormProduct(connection, null);
            Product product = new Product(
                row.Cells[1].Value.ToString(),
                row.Cells[2].Value.ToString()
            );
            product.Id = Convert.ToInt32(row.Cells[0].Value);

            return new FormProduct(connection, product);
        }
    }

    public class OrderDataAdapter : TableDataAdapter
    {
        public OrderDataAdapter() : base("Order_1")
        {
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "ID клиента";
            view.Columns[2].HeaderText = "Дата оформления";
            view.Columns[3].HeaderText = "Дата доставки";
            view.Columns[4].HeaderText = "Сумма";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormOrder(connection, null);

            DataRowView rowView = row.DataBoundItem as DataRowView;
            Order futura = new Order(rowView.Row);

            return new FormOrder(connection, futura);
        }
    }

    public class OrderInfoDataAdapter : TableDataAdapter
    {
        public OrderInfoDataAdapter() : base("Order_Info")
        {
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "ID цены";
            view.Columns[2].HeaderText = "ID заказа";
            view.Columns[3].HeaderText = "Количество";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormOrderInfo(connection, null, 0);

            DataRowView rowView = row.DataBoundItem as DataRowView;
            OrderInfo orderInfo = new OrderInfo(rowView.Row);

            return new FormOrderInfo(connection, orderInfo, 0);
        }
    }

    public class PriceListDataAdapter : TableDataAdapter
    {
        public PriceListDataAdapter() : base("Price_List")
        {
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "ID Product";
            view.Columns[2].HeaderText = "Price";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormPrice(connection, null);

            DataRowView rowView = row.DataBoundItem as DataRowView;
            Price price = new Price(rowView.Row);

            return new FormPrice(connection, price);
        }
    }

    public abstract class NamedDataAdapter : TableDataAdapter
    {
        public string ReferencedTable { get; set; }

        public NamedDataAdapter(string namedTable, string referencedTable) : base(namedTable)
        {
            this.ReferencedTable = referencedTable;
        }
    }

    public class NamedPriceListDataAdapter : NamedDataAdapter
    {
        public NamedPriceListDataAdapter() : base("Named_Price_List", "Price_List")
        {
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "Название";
            view.Columns[2].HeaderText = "Цена";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormPrice(connection, null);

            DataRowView rowView = row.DataBoundItem as DataRowView;
            int priceId = Convert.ToInt32(rowView.Row.ItemArray[0]);

            NpgsqlCommand command = new NpgsqlCommand("select * from Price_List where id = :id", connection);
            command.Parameters.AddWithValue(":id", priceId);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            Price price = new Price(table.Rows[0]);
            return new FormPrice(connection, price);
        }
    }

    public class NamedOrderDataAdapter : NamedDataAdapter
    {
        public NamedOrderDataAdapter() : base("Named_Order", "Order_1")
        {
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "Заказчик";
            view.Columns[2].HeaderText = "Дата оформления";
            view.Columns[3].HeaderText = "Дата доставки";
            view.Columns[4].HeaderText = "Сумма";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormOrder(connection, null);

            DataRowView rowView = row.DataBoundItem as DataRowView;
            int orderId = Convert.ToInt32(rowView.Row.ItemArray[0]);

            NpgsqlCommand command = new NpgsqlCommand("select * from Order_1 where id = :id", connection);
            command.Parameters.AddWithValue(":id", orderId);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            Order order = new Order(table.Rows[0]);
            return new FormOrder(connection, order);
        }
    }

    public class NamedOrderInfoDataAdapter : NamedDataAdapter
    {
        public int IdFilter { get; set; }

        public NamedOrderInfoDataAdapter() : base("Named_Order_Info", "Order_Info")
        {
        }

        public void Setup(NpgsqlConnection connection, DataGridView view, int idFilter)
        {
            this.IdFilter = idFilter;
            NpgsqlDataAdapter adapter = FillBy($"SELECT * FROM {tableName} WHERE id_order = {this.IdFilter}", connection);
            OnSetup(adapter, connection, view);
        }

        public override void OnSetup(NpgsqlDataAdapter adapter, NpgsqlConnection connection, DataGridView view)
        {
            view.DataSource = dataTable;
            view.Columns[0].HeaderText = "ID";
            view.Columns[1].HeaderText = "ID заказа";
            view.Columns[2].HeaderText = "Название";
            view.Columns[3].HeaderText = "Цена";
            view.Columns[4].HeaderText = "Количество";
            view.Columns[5].HeaderText = "Заказчик";
        }

        public override Form CreateInstanceForm(NpgsqlConnection connection, DataGridViewRow row)
        {
            if (row == null) return new FormOrderInfo(connection, null, IdFilter);

            DataRowView rowView = row.DataBoundItem as DataRowView;
            int orderInfoId = Convert.ToInt32(rowView.Row.ItemArray[0]);

            NpgsqlCommand command = new NpgsqlCommand("select * from Order_Info where id = :id", connection);
            command.Parameters.AddWithValue(":id", orderInfoId);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            OrderInfo orderInfo = new OrderInfo(table.Rows[0]);
            return new FormOrderInfo(connection, orderInfo, IdFilter);
        }
    }

    public class PriceListExportView : BaseDataAdapter
    {

        public PriceListExportView()
        {
            this.tableName = "Прайс-лист";
        }

        public override void Setup(NpgsqlConnection connection, DataGridView view = null)
        {
            string query = @"
select
	Price_List.id as №,
	Product.name as Название,
	Price_list.price as Цена
from Price_List
left join Product
	on Price_List.id = Product.id";

            FillBy(query, connection);
        }

    }

    public class ClientReportView : BaseDataAdapter
    {
        public int IdClient { get; set; }
        public DateTime OrderDate { get; set; }

        public override void Setup(NpgsqlConnection connection, DataGridView view)
        {
            string query = @"
select 
	id,
	order_date,
	total_sum
from Order_1
where
	order_1.id_client = :id_client
	and order_date <= :order_date
	and delivery_date is null";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("id_client", IdClient);
            command.Parameters.AddWithValue("order_date", OrderDate);

            FillBy(command);
            view.DataSource = dataTable;
        }

    }

}
