using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using TheArtOfDev.HtmlRenderer.Adapters;

namespace FoodShare
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("server=sql11.freemysqlhosting.net;user=sql11693979;database=sql11693979;port=3306;password=wpZrAv9L4v");
        MySqlDataAdapter adapter;
        DataTable DataTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {
            Form2 register = new Form2();
            this.Hide();
            register.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            adapter = new MySqlDataAdapter("SELECT `username`, `password` FROM `rgister` WHERE `username` = '" + bunifuTextBox1.Text + "' AND `password` = '" + bunifuTextBox2.Text + "'", connection);
            adapter.Fill(DataTable);
            if (DataTable.Rows.Count <= 0)
            {
                MessageBox.Show("Невалидна парола или потребителско име");

            }
            else
            {
                Main main = new Main();
                this.Hide();
                main.Show();
            }
        }
    }
}
