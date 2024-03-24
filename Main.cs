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

namespace FoodShare
{

    public partial class Main : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;

        private PictureBox pic = new PictureBox();
        private Label city;
        private Label description;
        public Main()
        {
            InitializeComponent();
            cn = new MySqlConnection();
            cn.ConnectionString = ("server = sql11.freemysqlhosting.net; user = sql11693979; database = sql11693979; port = 3306; password = wpZrAv9L4v");
        }

        private void getData()
        {
            cn.Open();
            cm = new MySqlCommand("select image, description, city from tblitem", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                long len = dr.GetBytes(0, 0, null, 0, 0);
                byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                pic = new PictureBox();
                pic.Width = 250;
                pic.Height = 250;
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                pic.BorderStyle = BorderStyle.FixedSingle;

                city = new Label();
                city.Text = dr["city"].ToString();
                city.Width = 50;
                city.BackColor = Color.FromArgb(255, 121, 121);
                city.TextAlign = ContentAlignment.MiddleCenter;

                description = new Label();
                description.Text = dr["description"].ToString();
                description.BackColor = Color.FromArgb(255, 121, 121);
                description.TextAlign = ContentAlignment.MiddleCenter;
                description.Dock = DockStyle.Bottom;

                MemoryStream ms = new MemoryStream(array);
                Bitmap bitmap = new Bitmap(ms);   ///tblitem  `id`, `description`, `city`, `status`, `image`, `qty`
                pic.BackgroundImage = bitmap; 

                flowLayoutPanel1.Controls.Add(pic);
                pic.Controls.Add(description);
                pic.Controls.Add(city);
            }
            dr.Close();
            cn.Close();
            
        }

        private void bunifuShadowPanel1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            getData();
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png, *.gif)|*.jpg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(selectedImagePath);
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

            try
            {
                byte[] imageData;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData = ms.ToArray();
                }

                cn.Open();
                string insetQuery = ("INSERT INTO `tblitem` (`description`, `city`, `image`, `qty`) VALUES('" + bunifuTextBox1.Text + "', '" + bunifuTextBox2.Text + "', '" + imageData + "', '" + bunifuTextBox5.Text + "');");
                MySqlCommand command = new MySqlCommand(insetQuery, cn);
                command.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Record Save", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
