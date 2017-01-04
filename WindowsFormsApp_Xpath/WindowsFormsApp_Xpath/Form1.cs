using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_Xpath
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBoxURL.Text);
            string html = getRequest(textBoxURL.Text);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            textBox1.Text = html;

            textBoxResults.Text += textBoxURL.Text;
            textBoxResults.Text += "-----------------------------------------" + System.Environment.NewLine;

            foreach (var inputnode in doc.DocumentNode.SelectNodes("//input"))
            {
                textBoxResults.Text += inputnode.XPath;
                textBoxResults.Text += System.Environment.NewLine;
            }            
        }

        
        static string getRequest(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                CookieContainer cookie = new CookieContainer();
                req.Method = "GET";
                req.CookieContainer = cookie;

                string res;
                using (Stream response = req.GetResponse().GetResponseStream())
                using (StreamReader reader1 = new StreamReader(response))
                    res = reader1.ReadToEnd();
                return res;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                //return String.Empty;
            }
        }
    }
}
