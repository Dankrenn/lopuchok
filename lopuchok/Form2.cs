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
    public partial class Form2 : Form
    {
        string FilePuth;
        DbContext dbContext = new DbContext();
        Product product;
        public Form2()
        {
            InitializeComponent();
            this.FormClosed += Close;
            UpdateInfoProduct();
        }
        public Form2(Product product)
        {
            this.product = product;
            FilePuth = product.Image;
            InitializeComponent();
            this.FormClosed += Close;
            UpdateInfoProduct();
        }

        private void Close(object sender, EventArgs e) => Application.Exit();

        private void buttonAddPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FilePuth = ofd.FileName;
                    pictureBox1.Image = Image.FromFile(FilePuth);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonShowForm1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().ShowDialog();
        }

        private void UpdateInfoProduct()
        {
            List<ProductType> list = dbContext.GetAllProductType();
            for (int i = 0; i < list.Count; i++)
            {
                comboBoxType.Items.Add(list[i].Title);
            }
            List<Material> materials = dbContext.GetAllMaterial();
            for (int i = 0; i < materials.Count; i++)
            {
                checkedListBox1.Items.Add(materials[i].Title);
            }

            if (product != null)
            {
                textBoxTitle.Text = product.Title;
                textBoxArticle.Text = product.ArticleNumber;
                textBoxDescription.Text = product.Description;
                pictureBox1.Image = Image.FromFile(product.Image);
                comboBoxType.SelectedIndex = product.ProductType.ID-1;
                for (int i = 0; i < product.Materials.Count; i++)
                {
                    for (int j = 0; j < checkedListBox1.Items.Count; j++)
                    {
                        if (product.Materials[i].Title == checkedListBox1.Items[j].ToString())
                        {
                            checkedListBox1.SetItemChecked(j, true);
                        }
                    }
                }
            }

        }

        private void buttonAddAndUpdate_Click(object sender, EventArgs e)
        {
            List<ProductType> list = dbContext.GetAllProductType();
            List<int> idMaterialsChek = new List<int>();
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                idMaterialsChek.Add(index);
            }
               

            if (product == null) 
            {
                product.ID = 0;               
            }
            else
            product.Title = textBoxTitle.Text;
            product.ArticleNumber = textBoxArticle.Text;
            product.Description = textBoxDescription.Text;
            product.Image = FilePuth;
            product.ProductType = list[comboBoxType.SelectedIndex];
            product.Materials = dbContext.GetMaterialsForId(idMaterialsChek.ToArray(), dbContext.connection);
           // dbContext.SetProductMaterial(product,dbContext.connection);
            dbContext.Save(product);
        }
    }
}
