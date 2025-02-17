using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTIT_NLPtranslate
{
    public partial class menu_Language : Form
    {
        Translate_Interface translate;
        int setLanguage;
        private string connectionString = "Data Source=LAPTOP-U6Q95GDG\\ZHENG;Initial Catalog=Languages;Integrated Security=True";
        public menu_Language(Translate_Interface translate, int setLanguage)
        {
            InitializeComponent();
            this.translate = translate;
            this.setLanguage = setLanguage;
        }

        private void fl_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void fl_panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menu_Language_Load(object sender, EventArgs e)
        {
            LoadLanguages();
        }

        private void LoadLanguages()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id_nation, name_nation FROM languages";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string idNation = reader["id_nation"].ToString();
                        string nameNation = reader["name_nation"].ToString();
                        Button btn = new Button();
                        Cursor = Cursors.Hand;
                        btn.Name = idNation;
                        btn.Text = nameNation;
                        btn.Width = 150;
                        btn.Height = 50;
                        btn.Click += new EventHandler(LanguageButton_Click);
                        fl_panelMenu.Controls.Add(btn);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }
        private void LanguageButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                if (setLanguage == 1)
                {
                    translate.setTextLangue1(clickedButton.Name, clickedButton.Text);
                }
                else
                {
                    translate.setTextLangue2(clickedButton.Name, clickedButton.Text);
                }
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
