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
using System.Drawing;
using System.IO;
using ZstdSharp.Unsafe;
using Mysqlx.Crud;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Org.BouncyCastle.Crypto.Paddings;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Bunifu.UI.WinForms;
using Button = System.Windows.Forms.Button;

namespace FoodShare
{

    public partial class Main : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        FileStream fs;
        BinaryReader br;
        public static Main instance;
        string username1;
        private PictureBox pic = new PictureBox();
        private Label city;
        private Label description;
        private int lastId = 0;
        public static int imageId;
        string backupCodes;
        public Main()
        {
            InitializeComponent();
            cn = new MySqlConnection();
            cn.ConnectionString = ("server = sql11.freemysqlhosting.net; user = sql11697088; database = sql11697088; port = 3306; password = 1cUFiClRQG");
            bunifuLabel2.Text = Form1.username1;
            publick();
            getDataProfile(Form1.username1);
        }


        private void getDataProfile(string username)
        {
            try
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT username, password, city, ph_number, backupcode, ph_number FROM rgister WHERE username = @username", cn);
                cmd.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    bunifuLabel12.Text = reader.GetString("username");
                    backupCodes = reader.GetString("backupcode");
                    bunifuLabel10.Text = reader.GetString("password");
                    bunifuLabel9.Text = reader.GetString("city");
                    bunifuLabel11.Text = reader.GetString("ph_number");
                    bunifuLabel14.Text = backupCodes;
                }
                else
                {
                    MessageBox.Show("No data found for ID: " + username);
                }

                reader.Close();
            }
            catch (Exception ex)
            {   
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void publick() ///2LmuvAB2qP
        {
            MySqlConnection connection = new MySqlConnection("server = sql11.freemysqlhosting.net; user = sql11697088; database = sql11697088; port = 3306; password = 1cUFiClRQG");

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id, image, description, city FROM tblitem WHERE public_user = @public_user", connection); ///sss
                cmd.Parameters.AddWithValue("@public_user", bunifuLabel14.Text.ToString());  ////sss
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    byte[] imageBytes = (byte[])reader["image"];
                    string description1 = reader.GetString("description");
                    string city = reader.GetString("city");

                    MemoryStream ms = new MemoryStream(imageBytes);
                    Image image = Image.FromStream(ms);

                    PictureBox pic = new PictureBox();
                    pic.Image = image;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Width = 250;
                    pic.Height = 250;

                    Button infoButton = new Button();
                    infoButton.Text = "Изтриване";
                    infoButton.ForeColor = Color.White;
                    infoButton.Tag = id;
                    infoButton.Height = 40;
                    infoButton.TextAlign = ContentAlignment.MiddleCenter;
                    infoButton.Dock = DockStyle.Bottom;
                    infoButton.Click += InfoButton_Click1;

                    flowLayoutPanel2.Controls.Add(pic);
                    pic.Controls.Add(infoButton);

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

        private void InfoButton_Click1(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            imageId = (int)btn.Tag;
            DeleteDataFromDatabase(imageId);

        }

        private void DeleteDataFromDatabase(int id)
        {
            try
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM tblitem WHERE id = @id", cn);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record with ID " + id + " deleted successfully!");
                    flowLayoutPanel2.Controls.Clear();
                    publick();
                }
                else
                {
                    MessageBox.Show("No record found with ID " + id + " to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void LoadImagesFromDatabase()
        {
            MySqlConnection connection = new MySqlConnection("server = sql11.freemysqlhosting.net; user = sql11697088; database = sql11697088; port = 3306; password = 1cUFiClRQG");

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id, image, description, city FROM tblitem", connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    byte[] imageBytes = (byte[])reader["image"];
                    string description1 = reader.GetString("description");
                    string city = reader.GetString("city");

                    MemoryStream ms = new MemoryStream(imageBytes);
                    Image image = Image.FromStream(ms);

                    PictureBox pic = new PictureBox();
                    pic.Image = image;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Width = 250;
                    pic.Height = 250;

                    Button infoButton = new Button();
                    infoButton.Text = "Виж";
                    infoButton.ForeColor = Color.White;
                    infoButton.Tag = id;
                    infoButton.Height = 40;
                    infoButton.TextAlign = ContentAlignment.MiddleCenter;
                    infoButton.Dock = DockStyle.Bottom;
                    infoButton.Click += InfoButton_Click;

                    Label cityLabel = new Label();
                    cityLabel.Text = city;
                    cityLabel.Width = 50;
                    cityLabel.BackColor = Color.FromArgb(255, 121, 121);
                    cityLabel.TextAlign = ContentAlignment.MiddleCenter;

                    flowLayoutPanel1.Controls.Add(pic);
                    pic.Controls.Add(infoButton);
                    pic.Controls.Add(cityLabel);

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

        private void InfoButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            imageId = (int)btn.Tag;
            testq1 testq1 = new testq1();
            testq1.Show();
        }

        private void bunifuShadowPanel1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadImagesFromDatabase();
            LoadLastId();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            LoadImagesFromDatabase();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            bunifuPages1.PageIndex = 1;

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            bunifuPages1.PageIndex = 2;

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image files | *.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    bunifuTextBox4.Text = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;
            FileStream fs;
            BinaryReader br;
            try
            {
                if (bunifuTextBox1.Text.Length > 0 && bunifuTextBox4.Text.Length > 0)
                {
                    lastId++;
                    cn.Open();
                    string FileName = bunifuTextBox4.Text;
                    byte[] ImageData;
                    fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs);
                    ImageData = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();
                    string CmdString = "INSERT INTO tblitem(id, description, city, status, image, qty, public_user, ph_number) VALUES(@id, @description, @city, @status ,@image, @qty, @public_user, @ph_number)"; //backupCodes
                    cmd = new MySqlCommand(CmdString, cn);
                    cmd.Parameters.Add("@id", MySqlDbType.Int64, 11);
                    cmd.Parameters.Add("@description", MySqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@city", MySqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@status", MySqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@image", MySqlDbType.Blob);
                    cmd.Parameters.Add("@qty", MySqlDbType.VarChar, 255);
                    cmd.Parameters.Add("@public_user", MySqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@ph_number", MySqlDbType.VarChar, 15);
                    cmd.Parameters["@id"].Value = lastId;
                    cmd.Parameters["@description"].Value = bunifuTextBox1.Text;
                    cmd.Parameters["@city"].Value = bunifuLabel9.Text.ToString();
                    cmd.Parameters["@status"].Value = "free";
                    cmd.Parameters["@image"].Value = ImageData;
                    cmd.Parameters["@qty"].Value = bunifuTextBox5.Text;
                    cmd.Parameters["@public_user"].Value = backupCodes;
                    cmd.Parameters["@ph_number"].Value = bunifuLabel11.Text;
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        MessageBox.Show("Image saved sucessfully!");
                    }
                    cn.Close();
                }
                else
                {
                    MessageBox.Show("Incomplete data!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }

        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            bunifuPages1.PageIndex = 0;
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            bunifuPages1.PageIndex = 0;
        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuShadowPanel1_ControlAdded_1(object sender, ControlEventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPages1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            LoadImagesFromDatabase();
            bunifuPages1.PageIndex = 0;

        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            test settings = new test();
            settings.Show();
        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            bunifuPages1.PageIndex = 1;
        }

        private void bunifuButton2_Click_1(object sender, EventArgs e)
        {
            bunifuPages1.PageIndex = 2;
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton1_Click_2(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            LoadImagesFromDatabase();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            this.Hide();
            login.Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            chatBot chatBot = new chatBot();
            chatBot.Show();
        }
        private void LoadLastId()
        {
            using (MySqlConnection connection = new MySqlConnection("server = sql11.freemysqlhosting.net; user = sql11697088; database = sql11697088; port = 3306; password = 1cUFiClRQG"))
            {
                connection.Open();

                string query = "SELECT MAX(id) FROM tblitem";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    lastId = Convert.ToInt32(result);
                }
                else
                {
                    lastId = 0;
                }
            }
        }

        private void bunifuButton6_Click_1(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();
            publick();
            bunifuPages2.PageIndex = 1;
        }

        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            bunifuPages2.PageIndex = 0;
        }
    }
}
