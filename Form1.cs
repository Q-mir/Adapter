using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WinFormsApp64
{
    public partial class Form1 : Form
    {
        private SqlConnection _connection;
        public Form1()
        {
            InitializeComponent();
            string path = $@"Data Source = K-405-5\SQLEXPRESS;
                             initial Catalog = udalenka;
                             integrated Security = true;
                            ";
            _connection = new(path);
            _connection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var command = new SqlCommand("select * from Student", _connection))
            using (var reader = command.ExecuteReader())
            {
                DataTable table = new DataTable();
                bool head = true;
                while(reader.Read())
                {
                    if(head)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string name = reader.GetName(i);
                            table.Columns.Add(name);
                        }
                        head = false;
                    }
                    DataRow row = table.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }
                    table.Rows.Add(row);
                }
                dataGridView1.DataSource = table;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Student", _connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0];
        }
    }
}