using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodShare
{
    public partial class testq1 : Form
    {
        private MySqlConnection connection;
        int a = Main.imageId;
        string userP;
        public testq1()
        {
            InitializeComponent();
            string connectionString = ("server = sql11.freemysqlhosting.net; user = sql11697088; database = sql11697088; port = 3306; password = 1cUFiClRQG");
            connection = new MySqlConnection(connectionString);
        }
        private void LoadImageFromDatabase(int id)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT description, city, status, qty, public_user, ph_number, image FROM tblitem WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    bunifuLabel1.Text = "Продукт: " + reader.GetString("description");
                    bunifuLabel2.Text = "Град: " + reader.GetString("city");
                    bunifuLabel3.Text = "Статус: " + reader.GetString("status");
                    bunifuLabel4.Text = "Количество: " + reader.GetString("qty");
                    userP = reader.GetString("public_user");
                    bunifuLabel5.Text = "Телефонен номер: " + reader.GetString("ph_number");

                    byte[] imageData = (byte[])reader["image"];
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    this.Text = bunifuLabel1.Text;
                }
                else
                {
                    MessageBox.Show("No data found for ID: " + id);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void testq1_Load(object sender, EventArgs e)
        {
            LoadImageFromDatabase(a);
        }
    }
}
