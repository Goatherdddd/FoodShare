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
        public static Form1 instance;
        public static string username1;
        private static readonly string connectionString = "server = sql11.freemysqlhosting.net; user = sql11697088; database = sql11697088; port = 3306; password = 1cUFiClRQG";
        private MySqlConnection connection;
        MySqlDataAdapter adapter;
        DataTable DataTable = new DataTable();
        public Form1()
        {
            InitializeComponent();
            connection = new MySqlConnection(connectionString);
            instance = this;
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
            string username = bunifuTextBox1.Text;
            string password = bunifuTextBox2.Text;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT `username`, `password` FROM `rgister` WHERE `username` = @username AND `password` = @password", connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            username1 = username;
                            Main mainForm = new Main();
                            Hide();
                            mainForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Невалидно потребителско име или парола");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Възникна грешка при влизане: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            test main1 = new test();
            this.Hide();
            main1.Show();
        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }

    }
}
