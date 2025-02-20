using PointManagement_Application.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Google.Cloud.TextToSpeech.V1;
using System.IO;
namespace PTIT_NLPtranslate
{
    public partial class Translate_Interface : Form
    {
        private System.Threading.CancellationTokenSource cts;
        public Translate_Interface()
        {
            InitializeComponent();
            FormHelper.EnableDrag(this);
        }

        private void Translate_Interface_Load(object sender, EventArgs e)
        {

        }

        private async void txt_Origin_TextChanged(object sender, EventArgs e)
        {
            if (cts != null)
                cts.Cancel();

            cts = new System.Threading.CancellationTokenSource();
            try
            {
                await Task.Delay(500, cts.Token);

                if (!cts.Token.IsCancellationRequested)
                {
                    string translation = await TranslateAsync(txt_Origin.Text, txt_from.Text, txt_To.Text);

                    if (!txt_Translated.IsDisposed)
                    {
                        txt_Translated.Invoke((MethodInvoker)(() => txt_Translated.Text = translation));
                    }
                }
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi dịch: " + ex.Message);
            }
        }
        private async Task<string> TranslateAsync(string input, string from, string to)
        {
            try
            {
                string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={from}&tl={to}&dt=t&q={HttpUtility.UrlEncode(input)}";
                using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
                {
                    string result = await webClient.DownloadStringTaskAsync(url);
                    return ExtractTranslation(result);
                }
            }
            catch
            {
                return "Lỗi dịch!";
            }
        }

        private string ExtractTranslation(string json)
        {
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    JsonElement root = doc.RootElement;
                    if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                    {
                        JsonElement firstArray = root[0];
                        if (firstArray.ValueKind == JsonValueKind.Array)
                        {
                            string translatedText = "";
                            foreach (JsonElement item in firstArray.EnumerateArray())
                            {
                                translatedText += item[0].GetString();
                            }
                            return translatedText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Lỗi xử lý JSON: " + ex.Message;
            }
            return "Lỗi dịch!";
        }

        public void setTextLangue1(string btnName, string btnText)
        {
            txt_from.Text = btnName;
            Blanguage1.Text = btnText;
        }

        public void setTextLangue2(string btnName, string btnText)
        {
            txt_To.Text = btnName;
            Blanguage2.Text = btnText;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private async void Blanguage1_Click(object sender, EventArgs e)
        {
            menu_Language menuForm = new menu_Language(this, 1);
            int x = this.Location.X + (this.Width - menuForm.Width) / 2;
            int y = this.Location.Y + (this.Height - menuForm.Height) / 2;
            menuForm.StartPosition = FormStartPosition.Manual;
            menuForm.Location = new Point(x, y);
            var result = menuForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                menuForm.Close();
            }
            if (Blanguage1.Text != "语言 1" && Blanguage2.Text != "语言 2")
            {
                string translation = await TranslateAsync(txt_Origin.Text, txt_from.Text, txt_To.Text);
                if (!txt_Translated.IsDisposed)
                {
                    txt_Translated.Invoke((MethodInvoker)(() => txt_Translated.Text = translation));
                }
            }
        }

        private async void Blanguage2_Click(object sender, EventArgs e)
        {
            menu_Language menuForm = new menu_Language(this, 2);
            int x = this.Location.X + (this.Width - menuForm.Width) / 2;
            int y = this.Location.Y + (this.Height - menuForm.Height) / 2;
            menuForm.StartPosition = FormStartPosition.Manual;
            menuForm.Location = new Point(x, y);
            var result = menuForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                menuForm.Close();
            }

            if (Blanguage1.Text != "语言 1" && Blanguage2.Text != "语言 2")
            {
                string translation = await TranslateAsync(txt_Origin.Text, txt_from.Text, txt_To.Text);
                if (!txt_Translated.IsDisposed)
                {
                    txt_Translated.Invoke((MethodInvoker)(() => txt_Translated.Text = translation));
                }
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Translated.Text))
            {
                Clipboard.SetText(txt_Translated.Text);
            }
        }

        private async void ReadText(string text, string language)
        {

        }


        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            //nơi đây là code click vô cho đọc txt_Translated.Text
        }
    }
}
