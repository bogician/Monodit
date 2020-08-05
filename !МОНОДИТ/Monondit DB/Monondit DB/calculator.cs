using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Monondit_DB
{
    public partial class calculator : Form
    {
        public calculator(string s123)
        {
            InitializeComponent();
            sSort = s123;
           
        }
        public static string sSort;
        DataView dv1;
        DataSet ds;
        public static string nameXMlfile = "KATALOG.XML";
        public static int x;
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void calculator_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            FileStream fsReadxml = new FileStream(nameXMlfile, FileMode.Open);
            ds.ReadXml(fsReadxml, XmlReadMode.InferTypedSchema);
            fsReadxml.Close();

            dv1 = new DataView(ds.Tables[1]);
            dv1.RowFilter = "KOD = '" + sSort + "'";
            
            dataGridView2.DataSource = dv1;
            dataGridView2.Columns[1].Width = 200;
            dataGridView2.Columns[3].HeaderText = "Процент";

            lbl.Text = "Навеска " + dataGridView2.Rows[1].Cells[0].Value.ToString();
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
             x = e.RowIndex;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double a = double.Parse(textBox1.Text);
            int n = dataGridView2.RowCount;
            n = n - 1;
            double[] proc = new double[n];
            for (int i = 0; i < n;i++)
            {
                string p = dataGridView2.Rows[i].Cells[3].Value.ToString();
                proc[i] = double.Parse(p);
               
                
            }
            double koef = a / proc[x];
            
            //listBox1.Items.Add(koef);
            double[] pereschet = new double[n+1];
            //double sum = 0;
            for (int i = 0; i<n;i++)
            {
                pereschet[i] = Math.Round((koef * proc[i]), 3);
                //sum += pereschet[i];
                //listBox1.Items.Add(pereschet[i]);
            }
           // pereschet[n] = sum;
            dataGridView2.Columns.Add("1","Навеска");
            for (int i = 0; i<n;i++)
            {
                dataGridView2.Rows[i].Cells[4].Value = pereschet[i].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Columns.Remove("1");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Bitmap bmp;
        private void button4_Click(object sender, EventArgs e)
        {
            int height = dataGridView2.Height;
            int wdth = dataGridView2.Width;
            dataGridView2.ClearSelection();
            dataGridView2.Rows[0].Cells[0].Selected = false;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Width = 500;

            lbl.BackColor = Color.White;
            dataGridView2.ScrollBars = ScrollBars.None;
            if(checkBox1.Checked == true)
            {
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Width = 430;

            }
            dataGridView2.Height = (int)(dataGridView2.RowCount * dataGridView2.RowTemplate.Height * 1.1);
            bmp = new Bitmap(dataGridView2.Width+500, dataGridView2.Height+500);
            dataGridView2.DrawToBitmap(bmp, new Rectangle(50, 100, dataGridView2.Width, dataGridView2.Height));
            lbl.DrawToBitmap(bmp, new Rectangle(300, 50, lbl.Width, lbl.Height));
            dataGridView2.Height = height;
            dataGridView2.ScrollBars = ScrollBars.Both;
            lbl.BackColor = default;
            dataGridView2.Columns[0].Visible = true;
            dataGridView2.Rows[0].Cells[0].Selected = true;
            dataGridView2.Columns[2].Visible = true;
            dataGridView2.Columns[3].Visible = true;
            dataGridView2.Width = wdth;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
