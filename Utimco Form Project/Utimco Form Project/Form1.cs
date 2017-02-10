using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Utimco_Form_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            string fileText;
            string fileLocation;
            var str = "{ hello: 'world', places: ['Africa', 'America', 'Asia', 'Australia'] }";
            if (textBox1.Text !="")
            {
                ofd.InitialDirectory = textBox1.Text;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                fileLocation = ofd.FileName;
                var fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    fileText = streamReader.ReadToEnd();
                    var json = JsonConvert.DeserializeObject(fileText);
                    var jsonObject = JArray.Parse(json.ToString());
                    foreach (JToken child in jsonObject.Children())
                    {
                        string test = (string)jsonObject.SelectToken("menu.items");
                        /*foreach (JToken a in child.Children())
                        {
                            foreach (JToken b in a.Children())
                            {
                                    string menu = (string)b.SelectToken("menu[0].items");
                            }
                        }*/
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
