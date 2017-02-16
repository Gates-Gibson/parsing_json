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

        public class Item
        {
            public int id { get; set; }
            public string label { get; set; }
        }

        public class Menu
        {
            public string header { get; set; }
            public List<Item> items { get; set; }
            public int sum { get; set; }
        }

        public class RootObject
        {
            public Menu menu { get; set; }

        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            string fileText;
            string fileLocation;
            if (textBox1.Text != "")
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
                    var stuff = JsonConvert.DeserializeObject<List<RootObject>>(fileText);
                    foreach (var menu in stuff)
                    {
                        var total = 0;
                        foreach (var item in menu.menu.items)
                        {
                            if (item != null && item.label != null)
                            {
                                total += item.id;
                            }

                        }
                        menu.menu.sum = total;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
