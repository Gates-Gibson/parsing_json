﻿using System;
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

        //Calculate Total based off of labels
        public static List<RootObject> calculateTotals(List<RootObject> menus)
        {
            try
            {
                foreach (var menu in menus)
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
                return menus;
            }
            catch
            {
                foreach (var menu in menus)
                {
                    menu.error = "There was an error parsing for menu items";
                }
                return menus;
            }
        }

        //retrieve the selected file
        public static string retrieveFile(string location)
        {
            var fileStream = new FileStream(location, FileMode.Open, FileAccess.Read);
            string fileText;
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                fileText = streamReader.ReadToEnd();
            }
            return fileText;
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
            public string error { get; set; }

        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            string fileText;
            string fileLocation;
            List<RootObject> menus;
            if (textBox1.Text != "")
            {
                ofd.InitialDirectory = textBox1.Text;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                fileLocation = ofd.FileName;
                try
                {
                    fileText = retrieveFile(fileLocation);
                    menus = calculateTotals(JsonConvert.DeserializeObject<List<RootObject>>(fileText));
                    foreach (var menu in menus)
                    {
                        if (menu.error != "")
                        {
                            textBox2.Text += menu.menu.sum.ToString() + " \r\n";
                        }
                        else
                            textBox2.Text = menu.error;
                    }
                }
                catch(Exception ex)
                {
                    if(ex is JsonReaderException)
                    {
                        textBox2.Text = "There was an error deserializing";
                    }
                    else
                    {
                        textBox2.Text = "There was an error";
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
