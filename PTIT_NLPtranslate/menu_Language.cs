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
        private List<Tuple<string, string>> languages = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("af", "Afrikaans"),
            new Tuple<string, string>("sq", "Albanian"),
            new Tuple<string, string>("am", "Amharic"),
            new Tuple<string, string>("ar", "Arabic"),
            new Tuple<string, string>("hy", "Armenian"),
            new Tuple<string, string>("az", "Azerbaijani"),
            new Tuple<string, string>("eu", "Basque"),
            new Tuple<string, string>("be", "Belarusian"),
            new Tuple<string, string>("bn", "Bengali"),
            new Tuple<string, string>("bs", "Bosnian"),
            new Tuple<string, string>("bg", "Bulgarian"),
            new Tuple<string, string>("ca", "Catalan"),
            new Tuple<string, string>("ceb", "Cebuano"),
            new Tuple<string, string>("ny", "Chichewa"),
            new Tuple<string, string>("zh", "Chinese (Simplified)"),
            new Tuple<string, string>("zh-TW", "Chinese (Traditional)"),
            new Tuple<string, string>("co", "Corsican"),
            new Tuple<string, string>("hr", "Croatian"),
            new Tuple<string, string>("cs", "Czech"),
            new Tuple<string, string>("da", "Danish"),
            new Tuple<string, string>("nl", "Dutch"),
            new Tuple<string, string>("en", "English"),
            new Tuple<string, string>("eo", "Esperanto"),
            new Tuple<string, string>("et", "Estonian"),
            new Tuple<string, string>("tl", "Filipino"),
            new Tuple<string, string>("fi", "Finnish"),
            new Tuple<string, string>("fr", "French"),
            new Tuple<string, string>("fy", "Frisian"),
            new Tuple<string, string>("gl", "Galician"),
            new Tuple<string, string>("ka", "Georgian"),
            new Tuple<string, string>("de", "German"),
            new Tuple<string, string>("el", "Greek"),
            new Tuple<string, string>("gu", "Gujarati"),
            new Tuple<string, string>("ht", "Haitian Creole"),
            new Tuple<string, string>("ha", "Hausa"),
            new Tuple<string, string>("haw", "Hawaiian"),
            new Tuple<string, string>("iw", "Hebrew"),
            new Tuple<string, string>("hi", "Hindi"),
            new Tuple<string, string>("hmn", "Hmong"),
            new Tuple<string, string>("hu", "Hungarian"),
            new Tuple<string, string>("is", "Icelandic"),
            new Tuple<string, string>("ig", "Igbo"),
            new Tuple<string, string>("id", "Indonesian"),
            new Tuple<string, string>("ga", "Irish"),
            new Tuple<string, string>("it", "Italian"),
            new Tuple<string, string>("ja", "Japanese"),
            new Tuple<string, string>("jw", "Javanese"),
            new Tuple<string, string>("kn", "Kannada"),
            new Tuple<string, string>("kk", "Kazakh"),
            new Tuple<string, string>("km", "Khmer"),
            new Tuple<string, string>("ko", "Korean"),
            new Tuple<string, string>("ku", "Kurdish (Kurmanji)"),
            new Tuple<string, string>("ky", "Kyrgyz"),
            new Tuple<string, string>("lo", "Lao"),
            new Tuple<string, string>("la", "Latin"),
            new Tuple<string, string>("lv", "Latvian"),
            new Tuple<string, string>("lt", "Lithuanian"),
            new Tuple<string, string>("lb", "Luxembourgish"),
            new Tuple<string, string>("mk", "Macedonian"),
            new Tuple<string, string>("mg", "Malagasy"),
            new Tuple<string, string>("ms", "Malay"),
            new Tuple<string, string>("ml", "Malayalam"),
            new Tuple<string, string>("mt", "Maltese"),
            new Tuple<string, string>("mi", "Maori"),
            new Tuple<string, string>("mr", "Marathi"),
            new Tuple<string, string>("mn", "Mongolian"),
            new Tuple<string, string>("my", "Myanmar (Burmese)"),
            new Tuple<string, string>("ne", "Nepali"),
            new Tuple<string, string>("no", "Norwegian"),
            new Tuple<string, string>("or", "Odia (Oriya)"),
            new Tuple<string, string>("ps", "Pashto"),
            new Tuple<string, string>("fa", "Persian"),
            new Tuple<string, string>("pl", "Polish"),
            new Tuple<string, string>("pt", "Portuguese"),
            new Tuple<string, string>("pa", "Punjabi"),
            new Tuple<string, string>("ro", "Romanian"),
            new Tuple<string, string>("ru", "Russian"),
            new Tuple<string, string>("sm", "Samoan"),
            new Tuple<string, string>("gd", "Scots Gaelic"),
            new Tuple<string, string>("sr", "Serbian"),
            new Tuple<string, string>("st", "Sesotho"),
            new Tuple<string, string>("sn", "Shona"),
            new Tuple<string, string>("sd", "Sindhi"),
            new Tuple<string, string>("si", "Sinhala"),
            new Tuple<string, string>("sk", "Slovak"),
            new Tuple<string, string>("sl", "Slovenian"),
            new Tuple<string, string>("so", "Somali"),
            new Tuple<string, string>("es", "Spanish"),
            new Tuple<string, string>("su", "Sundanese"),
            new Tuple<string, string>("sw", "Swahili"),
            new Tuple<string, string>("sv", "Swedish"),
            new Tuple<string, string>("tg", "Tajik"),
            new Tuple<string, string>("ta", "Tamil"),
            new Tuple<string, string>("te", "Telugu"),
            new Tuple<string, string>("th", "Thai"),
            new Tuple<string, string>("tr", "Turkish"),
            new Tuple<string, string>("uk", "Ukrainian"),
            new Tuple<string, string>("ur", "Urdu"),
            new Tuple<string, string>("ug", "Uyghur"),
            new Tuple<string, string>("uz", "Uzbek"),
            new Tuple<string, string>("vi", "Vietnamese"),
            new Tuple<string, string>("cy", "Welsh"),
            new Tuple<string, string>("xh", "Xhosa"),
            new Tuple<string, string>("yi", "Yiddish"),
            new Tuple<string, string>("yo", "Yoruba"),
            new Tuple<string, string>("zu", "Zulu")
        };

        public menu_Language(Translate_Interface translate, int setLanguage)
        {
            InitializeComponent();
            this.translate = translate;
            this.setLanguage = setLanguage;
            foreach (var language in languages)
            {
                Button btn = new Button();
                btn.Name = language.Item1;  // id_nation
                btn.Text = language.Item2;  // name_nation
                btn.Width = 150;
                btn.Height = 50;
                btn.Click += new EventHandler(LanguageButton_Click);
                fl_panelMenu.Controls.Add(btn);
            }
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