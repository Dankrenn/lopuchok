using lopuchok.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lopuchok
{
    public class ProductControl :UserControl
    {

        public Label labelType;
        public Label labelVendorCode;
        public Label labelMaterials;
        public Label labelPrice;
        public PictureBox pictureBox1;
        public Product product;
        public Form1 listproduction;

        public ProductControl(Form1 listproduction, Product product)
        {
            InitializeComponent();
            this.listproduction = listproduction;
            this.product = product;
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelType = new System.Windows.Forms.Label();
            this.labelVendorCode = new System.Windows.Forms.Label();
            this.labelMaterials = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(23, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Gabriola", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelType.Location = new System.Drawing.Point(163, 15);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(226, 35);
            this.labelType.TabIndex = 2;
            this.labelType.Text = "Тип продукта|Наименование ";
            // 
            // labelVendorCode
            // 
            this.labelVendorCode.AutoSize = true;
            this.labelVendorCode.Font = new System.Drawing.Font("Gabriola", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelVendorCode.Location = new System.Drawing.Point(166, 78);
            this.labelVendorCode.Name = "labelVendorCode";
            this.labelVendorCode.Size = new System.Drawing.Size(61, 29);
            this.labelVendorCode.TabIndex = 3;
            this.labelVendorCode.Text = "Артикул";
            // 
            // labelMaterials
            // 
            this.labelMaterials.AutoSize = true;
            this.labelMaterials.Font = new System.Drawing.Font("Gabriola", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMaterials.Location = new System.Drawing.Point(162, 118);
            this.labelMaterials.Name = "labelMaterials";
            this.labelMaterials.Size = new System.Drawing.Size(92, 35);
            this.labelMaterials.TabIndex = 4;
            this.labelMaterials.Text = "Материалы:";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Gabriola", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrice.Location = new System.Drawing.Point(28, 143);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(96, 35);
            this.labelPrice.TabIndex = 5;
            this.labelPrice.Text = "Стоимость";
            // 
            // ProductControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(255)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.labelMaterials);
            this.Controls.Add(this.labelVendorCode);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ProductControl";
            this.Size = new System.Drawing.Size(708, 189);
            this.Click += new System.EventHandler(this.ProductControl_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private void ProductControl_Click(object sender, EventArgs e)
        {

            listproduction.Hide();
            new Form2(product).ShowDialog();
        }

    }
}
