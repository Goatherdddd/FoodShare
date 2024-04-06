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
using MySqlX.XDevAPI.Common;
using Bunifu.UI.WinForms;
using System.Security.Cryptography.X509Certificates;

namespace FoodShare
{
    public partial class Form2 : Form
    {
        public static Form2 instance;
        public BunifuTextBox tb1;
        MySqlConnection connection = new MySqlConnection("server = sql11.freemysqlhosting.net; user = sql11697088; database = sql11697088; port = 3306; password = 1cUFiClRQG");
        MySqlDataAdapter adapter;
        DataTable DataTable = new DataTable();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        StringBuilder result = new StringBuilder(10);
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];
                result.Append(randomChar);
            }
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {
            Form1 back = new Form1();
            this.Hide();
            back.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            string password = "";
            if (bunifuTextBox2.Text == bunifuTextBox3.Text)
            {
                if (8 > bunifuTextBox2.Text.Length)
                {
                    MessageBox.Show("Паролата трябва да съдръжа минимум 8 символа.");
                }
                else
                {

                    password = bunifuTextBox3.Text;
                    string insetQuery = "INSERT INTO `rgister` (`username`, `password`, `city`, `backupcode`, `ph_number`) VALUES('" + bunifuTextBox1.Text + "','" + password.ToString() + "', '" + bunifuTextBox4.Text + "', '" + result.ToString() + "', '" + bunifuTextBox5.Text + "');";
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insetQuery, connection);
                    try
                    {
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Вие успешно се регистрирахте.");
                            Form1 login = new Form1();
                            this.Hide();
                            login.Show();
                        }
                        else
                        {
                            MessageBox.Show("Моля опитайте по-късно!");
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Паролата не съвпада.");
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
