using lopuchok.Classes;
using lopuchok.Classes.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lopuchok
{
    public partial class Form1 : Form
    {
        DbContext dbContext = new DbContext();
        public Form1()
        {
            InitializeComponent();
            this.FormClosed += Close;
        }

        private void Close(object sender, EventArgs e) => Application.Exit();

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Product> products = dbContext.GetAllProduct();
            UpdateForm(products);
            comboBoxSort.Items.Clear();
            comboBoxSort.Items.Add(Sort.MinTitle);
            comboBoxSort.Items.Add(Sort.MaxTitle);
            comboBoxSort.Items.Add(Sort.MinNumber);
            comboBoxSort.Items.Add(Sort.MaxNumber);
            comboBoxSort.Items.Add(Sort.MinPrice);
            comboBoxSort.Items.Add(Sort.MaxPrice);
        }

        int indexrows = 0;
        int IndexStart = 0;
        int IndexEnd = 5;
        int rows = 0;
        private void UpdateForm(List<Product> products)
        {
            
            string basePath = @"C:\Users\danii\OneDrive\Рабочий стол\демка\picture.jpg";
            flowLayoutPanel2.Controls.Clear();
            if (products.Count % 5 == 0)
            {
                rows = products.Count / 5;
            }
            else
            {
                rows = products.Count / 5 + 1;
            }

            IndexStart = indexrows * 5;
            IndexEnd = Math.Min(IndexStart + 5, products.Count);

            Label label1 = new Label();
            label1.Text = "<";
            label1.AutoSize = true;
            label1.Click += labelClick1;
            flowLayoutPanel2.Controls.Add(label1);

            Label label2 = new Label();
            label2.Text = (indexrows + 1).ToString();
            label2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(label2);

            Label label3 = new Label();
            label3.Text = ">";
            label3.AutoSize = true;
            label3.Click += labelClick3;
            flowLayoutPanel2.Controls.Add(label3);

            flowLayoutPanel1.Controls.Clear();
            for (int i = IndexStart; i < IndexEnd; i++)
            {
                ProductControl conrolProduct = new ProductControl(this, products[i]);
                conrolProduct.labelType.Text = $"{products[i].ProductType.Title} | {products[i].Title}";
                conrolProduct.labelVendorCode.Text = products[i].ArticleNumber.ToString();
                string imagePath = products[i].Image != String.Empty ? products[i].Image : basePath;
                conrolProduct.pictureBox1.Image = Image.FromFile(imagePath);
                for (int j = 0; j < products[i].Materials.Count; j++)
                {
                    conrolProduct.labelMaterials.Text += $"{products[i].Materials[j].Title} ";
                }
                flowLayoutPanel1.Controls.Add(conrolProduct);
            }
        }

        public void labelClick1(object sender, EventArgs e)
        {
            if (indexrows > 0)
            {
                indexrows--;
                Form1_Load(sender, e);
            }
        }
        public void labelClick3(object sender, EventArgs e)
        {
            if (indexrows + 1 < rows)
            {
                indexrows++;
                Form1_Load(sender, e);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           List<Product> products = dbContext.GetProductSearch(textBox1.Text, comboBoxSort.SelectedIndex);
            UpdateForm(products);
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Product> products = dbContext.GetProductSearch(textBox1.Text, comboBoxSort.SelectedIndex);
            UpdateForm(products);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form2().ShowDialog();
        }
    }
}
