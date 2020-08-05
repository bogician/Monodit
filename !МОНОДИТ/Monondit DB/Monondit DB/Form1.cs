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
using Microsoft.VisualBasic;
using System.Xml;

namespace Monondit_DB
{
    
    public partial class Form1 : Form
    {
        public static int currentRow = 0;
        public Form1()
        {
            InitializeComponent();
        }
        DataSet ds,ds1;
        DataView dv1, dv2, dv3, dv4, dv5, dv6;
        public static string nameXMlfile = "KATALOG.XML";
        public static string nameXMlfile1 = "SOSTAV.XML";
        public static string s123;

        bool isChangeSaved = true;
       

        private void button1_Click(object sender, EventArgs e)
        {
            calculator frm = new calculator(s123);
            frm.ShowDialog();
            //this.Hide();
            
        }
        Bitmap bmp;
        private void button2_Click(object sender, EventArgs e)
        {
            int height = dataGridView2.Height;
            int width = dataGridView2.Width;
            int width1 = dataGridView6.Width;
            int h1 = dataGridView2.Columns[2].Width;
            int h3 = dataGridView6.Columns[1].Width;
            dataGridView6.Columns[1].Width = 360;
            dataGridView2.Columns[2].Width = 230;
            dataGridView2.Columns[3].Width = 230;
            dataGridView2.Width = 700;
            dataGridView6.Width = 700;
            label3.Text = "Резиновая смесь";
            label2.BackColor = Color.White;
            label3.BackColor = Color.White;
            label22.BackColor = Color.White;
            label19.BackColor = Color.White;
            dataGridView2.ScrollBars = ScrollBars.None;
            dataGridView6.Rows[0].Cells[0].Selected = false;
            dataGridView2.ClearSelection();
            dataGridView2.Rows[0].Cells[0].Selected = false;
            dataGridView2.Height = (int)(dataGridView2.RowCount * dataGridView2.RowTemplate.Height*1.3);
            int height1 = dataGridView6.Height;
            dataGridView2.Columns[0].Visible = false;
            dataGridView6.Height = (int)(dataGridView6.RowCount * dataGridView6.RowTemplate.Height * 1.15);
            bmp = new Bitmap(dataGridView6.Width, dataGridView2.Height+dataGridView6.Height+500);
            label3.DrawToBitmap(bmp, new Rectangle(250, 50, label3.Width, label3.Height));
            label22.DrawToBitmap(bmp, new Rectangle(260+label3.Width, 50, label22.Width, label22.Height));
            dataGridView6.DrawToBitmap(bmp, new Rectangle(0, 100, dataGridView6.Width, dataGridView6.Height));
            label2.DrawToBitmap(bmp, new Rectangle(250, 350, label2.Width, label2.Height));
            label19.DrawToBitmap(bmp, new Rectangle(260 + label2.Width, 350, label19.Width, label19.Height));
            dataGridView2.DrawToBitmap(bmp, new Rectangle(0, 400, dataGridView2.Width, dataGridView2.Height));
            dataGridView2.Height = height;
            dataGridView2.Columns[0].Visible = true;
            dataGridView6.Height = height1;
            dataGridView2.Width = width;
            dataGridView2.Columns[2].Width = h1;
            dataGridView2.Columns[3].Width = h1;
            dataGridView6.Columns[1].Width = h3;
            dataGridView6.Width = width1;
            dataGridView2.ScrollBars = ScrollBars.Vertical;
            label2.BackColor = default;
            label3.BackColor = default;
            label22.BackColor = default;
            label19.BackColor = default;
            label3.Text = "Показатели смеси";
            printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 50, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            textBox1.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = false;
            dv3 = new DataView(ds.Tables[3]);
            dataGridView3.DataSource = dv3;
            for (int i = 0; i < dataGridView3.RowCount-1; i++)
            {
                comboBox1.Items.Add(dataGridView3.Rows[i].Cells[1].Value.ToString());
            }
            comboBox1.SelectedIndex = 0;
            dv5 = new DataView(ds.Tables[5]);
            dataGridView5.DataSource = dv5;
            for (int i = 0; i < dataGridView5.RowCount - 1; i++)
            {
                comboBox2.Items.Add(dataGridView5.Rows[i].Cells[1].Value.ToString());

            }
            comboBox2.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dv1 = new DataView(ds.Tables[0]);
            dv3 = new DataView(ds.Tables[3]);
            dataGridView3.DataSource = dv3;
            dv5 = new DataView(ds.Tables[5]);
            dataGridView5.DataSource = dv5;
            dv1.RowFilter += "KOD Like'%"+""+"%' ";

            if (checkBox1.Checked == true)
            {
                
                int k = 0;
                
                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                {
                    if(dataGridView3.Rows[i].Cells[1].Value.ToString()==comboBox1.Text)
                    {
                        k = int.Parse(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        break;
                    }
                }
                dv1.RowFilter += "AND NAZ ='" + k + "'";
          
            }
            if(checkBox2.Checked == true)
            {
                string poli = "";
                for (int i = 0; i < dataGridView5.RowCount - 1; i++)
                {
                    if (dataGridView5.Rows[i].Cells[1].Value.ToString() == comboBox2.Text)
                    {
                        poli = dataGridView5.Rows[i].Cells[0].Value.ToString();
                        break;
                    }
                }
                dv1.RowFilter += "AND POLI Like'%" + poli + "%'";
            }
            if (checkBox3.Checked == true)
            {
                dv1.RowFilter += "AND KOD Like'%" + textBox1.Text + "%'";
            }

                dataGridView1.DataSource = dv1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dv1 = new DataView(ds.Tables[0]);
            dataGridView1.DataSource = dv1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void разрешитьВноситьИзмененияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ReadOnly == true)
            {
                dataGridView1.ReadOnly = false;
                dataGridView2.ReadOnly = false;
                label23.Visible = true;
                разрешитьВноситьИзмененияToolStripMenuItem.BackColor = Color.Green;
                isChangeSaved = false;
                button1.Visible = false;
                button2.Visible = false;
                groupBox1.Visible = false;
                dataGridView7.Visible = true;
                добавитьСтрокуВРецептToolStripMenuItem.Enabled = true;
                составитьРецептToolStripMenuItem.Enabled = true;
                очиститьРецептToolStripMenuItem.Enabled = true;
                x = 0;

            }

            else
            {
                DialogResult result;
                result = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    dataGridView2.AllowUserToAddRows = true;
                    dv2.AddNew();
                    int n = dataGridView2.RowCount;
                    dataGridView2.Rows[n - 2].Cells[0].Value = smes;
                    dv2.Delete(n - 2);
                    dataGridView2.AllowUserToAddRows = false;
                    SaveXmlFile();
                    isChangeSaved = true;
                }
                else if (result == DialogResult.No)
                {
                    isChangeSaved = true;
                    LoadXMLfile();
                }
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
                button1.Visible = true;
                button2.Visible = true;
                groupBox1.Visible = true;
                label23.Visible = false;
                dataGridView7.Visible = false;
                добавитьСтрокуВРецептToolStripMenuItem.Enabled = false;
                составитьРецептToolStripMenuItem.Enabled = false;
                очиститьРецептToolStripMenuItem.Enabled = false;

                разрешитьВноситьИзмененияToolStripMenuItem.BackColor = Color.Red;
               
            }
        }

        private void добавитьЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add frm1 = new add();
            frm1.ShowDialog();
            this.Hide();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.Filter = "Файлы XML (*.xml)|*.xml";
            saveFileDialog1.FileName = "KATALOG.xml";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nameXMlfile = saveFileDialog1.FileName;
                SaveXmlFile();
                isChangeSaved = true;
            }
        }
        void SaveXmlFile()
        {
            FileStream fsWriteXml = new FileStream(nameXMlfile, FileMode.Create);
            ds.WriteXml(fsWriteXml);
            fsWriteXml.Close();
            FileStream fsWriteXml1 = new FileStream(nameXMlfile1, FileMode.Create);
            ds1.WriteXml(fsWriteXml1);
            fsWriteXml1.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                DialogResult result;
                result = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    SaveXmlFile();
                    isChangeSaved = true;
                Environment.Exit(0);
            }
                else if (result == DialogResult.No)
                {
                    isChangeSaved = true;
                Environment.Exit(0);
            }
                else
                {
                    e.Cancel = true;
                }
            
        }

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            dataGridView2.EndEdit();
            dataGridView3.EndEdit();
            dataGridView4.EndEdit();
            dataGridView5.EndEdit();
            dataGridView6.EndEdit();
            dataGridView7.EndEdit();
            SaveXmlFile();
        }
        public static string ingrid = "Бутилкаучук";
        public static int x =0;

      

        private void dataGridView7_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ingrid = dataGridView7.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            dataGridView2.Rows[x].Cells[1].Value = ingrid;
        }

        private void добавитьСтрокуВРецептToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView2.AllowUserToAddRows = true;
            dv2.AddNew();
            int n = dataGridView2.RowCount;
            dataGridView2.Rows[n - 2].Cells[0].Value = smes;
            dataGridView2.Rows[n - 2].Cells[1].Value = ingrid;
            dataGridView2.Rows[n - 2].Cells[3].Value = 0;
            dataGridView2.Rows[n - 2].Cells[2].Value = 0;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ClearSelection();
            dataGridView2.Rows[n - 2].Cells[1].Selected = true;
            x = n - 2;


        }

        private void dataGridView7_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ingrid = dataGridView7.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            dataGridView2.Rows[x].Cells[1].Value = ingrid;
        }

       
        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            x = e.RowIndex;
        }

        private void составитьРецептToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView2.EndEdit();
            int n = dataGridView2.RowCount;
            dv2.AddNew();
            dataGridView2.Rows[n].Cells[0].Value = smes;
            dataGridView2.Rows[n].Cells[1].Value = "Итого";
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                if (dataGridView2.Rows[i].Cells[2].Value.ToString()!="")
                {
                    sum += double.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                }
                else
                {
                    MessageBox.Show("Вы заполнили не все массовые доли");
                }
            }
            dataGridView2.Rows[n].Cells[2].Value = Math.Round(sum,2);
            for (int i = 0; i < n; i++)
            {
                double a = double.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                a = a * 100 / sum;
                a = Math.Round(a, 3);
                dataGridView2.Rows[i].Cells[3].Value = a.ToString();
            }
            dataGridView2.Rows[n].Cells[3].Value = 100;
            
        }

        private void очиститьРецептToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = dataGridView2.RowCount;
            int i = 0;
            while(i<n)
            {
                dataGridView2.Rows[i].Cells[1].Value = "";
                dataGridView2.Rows[i].Cells[2].Value = 0;
                dataGridView2.Rows[i].Cells[3].Value = 0;
                i++;
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadXMLfile();
        }

      

        public void LoadXMLfile()
        {
            ds = new DataSet();
            ds1 = new DataSet();
            FileStream fsReadxml = new FileStream(nameXMlfile, FileMode.Open);
            ds.ReadXml(fsReadxml, XmlReadMode.InferTypedSchema);
            fsReadxml.Close();
            FileStream fsReadxml1 = new FileStream(nameXMlfile1, FileMode.Open);
            ds1.ReadXml(fsReadxml1, XmlReadMode.InferTypedSchema);
            fsReadxml1.Close();
            dv1 = new DataView(ds.Tables[0]);
            dataGridView1.DataSource = dv1;
            string m = dataGridView1.Rows[0].Cells[3].Value.ToString();
            string s = dataGridView1.Rows[0].Cells[0].Value.ToString();
            string tfk = dataGridView1.Rows[0].Cells[8].Value.ToString();
            string poli = dataGridView1.Rows[0].Cells[2].Value.ToString();
            dv2 = new DataView(ds.Tables[1]);
            dv2.RowFilter = "KOD= '" + s + "'";
            dataGridView1.Columns[1].Width = 200;
            dataGridView3.Columns[3].Width = 300;
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[4].Visible = false;
            dataGridView3.Columns[2].Width = 300;
            dataGridView3.Columns[1].HeaderText="Назначение";
            dataGridView3.Columns[2].HeaderText = "Особые характеристики";
            dataGridView3.Columns[3].HeaderText = "Способ приготовления";
            dataGridView3.Columns[1].Width = 350;
            dataGridView1.Columns[0].HeaderText = "Шифр";
            dataGridView2.Columns[0].HeaderText = "Шифр";
            dataGridView2.Columns[3].HeaderText = "%";
            dataGridView2.Columns[2].HeaderText = "Доля";
            dataGridView2.Columns[1].HeaderText = "Состав";
            dataGridView1.Columns[2].HeaderText = "Полимер";
            dataGridView1.Columns[3].HeaderText = "Назначение";
            dataGridView2.Columns[1].Width = 240;
            dataGridView4.Columns[0].Visible = false;
            dataGridView4.Columns[1].Width = 300;
            dataGridView4.Columns[1].HeaderText = "Вид изделия";

            dv3 = new DataView(ds.Tables[3]);
            dv3.RowFilter = "NAZ= '" + m + "'";

            dv4 = new DataView(ds.Tables[4]);
            dv4.RowFilter = "TFK= '" + tfk + "'";

            dv5 = new DataView(ds.Tables[5]);
            dv5.RowFilter = "POLI= '" + poli + "'";

            currentRow = 0;
            isChangeSaved = true;
            dv6 = new DataView(ds1.Tables[0]);
            dataGridView7.DataSource = dv6;
            dataGridView7.Columns[0].Width = 200;
            dataGridView7.Columns[1].Width = 200;
            dataGridView7.Columns[2].Width = 200;
            dataGridView7.Columns[3].Width = 200;
            dataGridView7.Columns[4].Width = 200;
            dataGridView7.Columns[5].Width = 200;
            dataGridView7.Columns[6].Width = 200;
            dataGridView7.Columns[7].Width = 200;
            dataGridView7.Columns[8].Width = 200;
            dataGridView7.Columns[9].Width = 200;
            dataGridView7.Columns[10].Width = 200;
            dataGridView7.Columns[11].Width = 200;

        }
        public static string smes;
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView6.RowCount = 9;
            dataGridView6.ColumnCount = 3;
            dataGridView6.Columns[0].HeaderText = "Свойство";
            dataGridView6.Columns[1].HeaderText = "Значение"; 
            dataGridView6.Columns[2].HeaderText = ".";
            dataGridView6.Columns[2].Visible = false;
            dataGridView6.Rows[0].Cells[0].Value = "Назначение";
            dataGridView6.Rows[1].Cells[0].Value = "Тип полимера";
            dataGridView6.Rows[2].Cells[0].Value = "Вид изделия";
            dataGridView6.Rows[3].Cells[0].Value = "Условная прочность при разрыве (МПа)";
            dataGridView6.Rows[4].Cells[0].Value = "Твердость по Шор А";
            dataGridView6.Rows[5].Cells[0].Value = "Относительное удлиннение при разрыве (%)";
            dataGridView6.Rows[6].Cells[0].Value = "Особые свойства";
            dataGridView6.Rows[7].Cells[0].Value = "Способ приготовления";
            dataGridView6.Rows[8].Cells[0].Value = "Значение";
            dataGridView6.Columns[0].Width = 355;
            dataGridView6.Columns[1].Width = 335;
            dataGridView6.Rows[0].Cells[0].Selected = false;
            string s,m,tfk, poli;
            int i = e.RowIndex;
            currentRow = i;
            if(!dataGridView1.Rows[i].Cells[1].Value.Equals(DBNull.Value))
           {
                s = dataGridView1.Rows[i].Cells[0].Value.ToString();
                s123 = s;
dv2 = new DataView(ds.Tables[1]);
                dv2.RowFilter = "KOD = '" + s + "'";
dataGridView2.DataSource = dv2;
                label19.Text = s;
                label22.Text = s;
                smes = s;

                m = dataGridView1.Rows[i].Cells[3].Value.ToString();
                if(m == "")
                {
                    dataGridView3.DataSource = dv3;
                    string l = dataGridView3.Rows[0].Cells[1].Value.ToString();
                    label7.Text = l;
                    dataGridView6.Rows[0].Cells[1].Value = l;
                    string l1 = dataGridView3.Rows[0].Cells[2].Value.ToString();
                    label8.Text = l1;
                    dataGridView6.Rows[6].Cells[1].Value = l1;
                    string l2 = dataGridView3.Rows[0].Cells[3].Value.ToString();
                    label9.Text = l2;
                    dataGridView6.Rows[7].Cells[1].Value = l2;
                }
                else
                {
                    dv3 = new DataView(ds.Tables[3]);

                    dv3.RowFilter = "NAZ = '" + m + "'";
                    dataGridView3.DataSource = dv3;
                    string l = dataGridView3.Rows[0].Cells[1].Value.ToString();
                    label7.Text = l;
                    dataGridView6.Rows[0].Cells[1].Value = l;
                    string l1 = dataGridView3.Rows[0].Cells[2].Value.ToString();
                    label8.Text = l1;
                    dataGridView6.Rows[6].Cells[1].Value = l1;
                    string l2 = dataGridView3.Rows[0].Cells[3].Value.ToString();
                    label9.Text = l2;
                    dataGridView6.Rows[7].Cells[1].Value = l2;
                }

                tfk = dataGridView1.Rows[i].Cells[8].Value.ToString();
                if(tfk =="")
                {
                    dataGridView4.DataSource = dv4;
                    string l3 = dataGridView4.Rows[0].Cells[1].Value.ToString();
                    label11.Text = l3;
                }
                else
                {
                dv4 = new DataView(ds.Tables[4]);
                dv4.RowFilter = "TFK = '" + tfk + "'";
                dataGridView4.DataSource = dv4;
                    string l3 = dataGridView4.Rows[0].Cells[1].Value.ToString();
                    if (l3 == "")
                    {
                        label11.Text = 0.ToString();
                        dataGridView6.Rows[2].Cells[1].Value = 0;
                    }
                    else
                    {
                        label11.Text = l3;
                        dataGridView6.Rows[2].Cells[1].Value = l3;
                    }
                }

                poli = dataGridView1.Rows[i].Cells[2].Value.ToString();
                if (poli == "")
                {
                    dataGridView5.DataSource = dv5;
                    string l4 = dataGridView5.Rows[0].Cells[1].Value.ToString();
                    label12.Text = l4;
                    dataGridView6.Rows[1].Cells[1].Value = l4;
                }
                else
                {
                    dv5 = new DataView(ds.Tables[5]);
                    dv5.RowFilter = "POLI = '" + poli + "'";
                    dataGridView5.DataSource = dv5;
                    string l4 = dataGridView5.Rows[0].Cells[1].Value.ToString();
                    label12.Text = l4;
                    dataGridView6.Rows[1].Cells[1].Value = l4;
                }
                string proh = dataGridView1.Rows[i].Cells[6].Value.ToString();
               if(proh == "")
                {
                    label17.Text = 0.ToString();
                    dataGridView6.Rows[3].Cells[1].Value = 0;
                }
               else
                {
                    label17.Text = proh;
                    dataGridView6.Rows[3].Cells[1].Value = proh;
                }
                string tver = dataGridView1.Rows[i].Cells[5].Value.ToString();
                if (tver == "")
                {
                    label21.Text = 0.ToString();
                    dataGridView6.Rows[4].Cells[1].Value = 0;
                }
                else
                {
                    label21.Text = tver;
                    dataGridView6.Rows[4].Cells[1].Value = tver;
                }
                string ydlinn = dataGridView1.Rows[i].Cells[11].Value.ToString();
                if (ydlinn == "")
                {
                    label18.Text = 0.ToString();
                    dataGridView6.Rows[5].Cells[1].Value = 0;
                }
                else
                {
                    label18.Text = ydlinn;
                    dataGridView6.Rows[5].Cells[1].Value = ydlinn;
                }
                string sps = dataGridView1.Rows[i].Cells[7].Value.ToString();
                if (sps == "")
                {
                    //label18.Text = 0.ToString();
                    dataGridView6.Rows[8].Cells[1].Value = 0;
                }
                else
                {
                   // label18.Text = ydlinn;
                    dataGridView6.Rows[8].Cells[1].Value = sps;
                }
            }


        }

        private void найтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = Microsoft.VisualBasic.Interaction.InputBox("Введите код рецепта");
            if (s!="")
            {
                string strsort = dv1.Sort;
                dv1.Sort = "KOD";
                int index = dv1.Find(s);
                if(index == -1)
                {
                    dv1.Sort = strsort;
                    MessageBox.Show("Такого рецепта нет");

                }
                else
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index].Cells[0].Selected = true;

                    string p, p1,p2, p3;
                    int i = index;
                    currentRow = i;
                    if (!dataGridView1.Rows[i].Cells[1].Value.Equals(DBNull.Value))
                    {
                        p = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        smes = p;
                        //dv2 = new DataView(ds.Tables[1]);
                        dv2.RowFilter = "KOD = '" + p + "'";
                        dataGridView2.DataSource = dv2;
                        label19.Text = p;
                        label22.Text = p;
                        p1 = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        dv3 = new DataView(ds.Tables[3]);
                        dv3.RowFilter = "NAZ = '" + p1 + "'";
                        dataGridView3.DataSource = dv3;
                        string l = dataGridView3.Rows[0].Cells[1].Value.ToString();
                        label7.Text = l;
                        dataGridView6.Rows[0].Cells[1].Value = l;
                        string l1 = dataGridView3.Rows[0].Cells[2].Value.ToString();
                        label8.Text = l1;
                        dataGridView6.Rows[1].Cells[1].Value = l1;
                        string l2 = dataGridView3.Rows[0].Cells[3].Value.ToString();
                        label9.Text = l2;
                        dataGridView6.Rows[2].Cells[1].Value = l2;
                        p2 = dataGridView1.Rows[i].Cells[8].Value.ToString();
                        dv4 = new DataView(ds.Tables[4]);
                        dv4.RowFilter = "TFK = '" + p2 + "'";
                        dataGridView4.DataSource = dv4;
                        string l3 = dataGridView4.Rows[0].Cells[1].Value.ToString();
                        label11.Text = l3;
                        dataGridView6.Rows[3].Cells[1].Value = l3;
                        p3 = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        dv5 = new DataView(ds.Tables[5]);
                        dv5.RowFilter = "POLI = '" + p3 + "'";
                        dataGridView5.DataSource = dv5;
                        string l4 = dataGridView5.Rows[0].Cells[1].Value.ToString();
                        label12.Text = l4;
                        dataGridView6.Rows[4].Cells[1].Value = l4;

                        //proch pri razr
                        string l5 = dataGridView1.Rows[i].Cells[6].Value.ToString();
                        label17.Text = l5;
                        dataGridView6.Rows[5].Cells[1].Value = l5;
                        //tverdost po shor a
                        string l6 = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        label21.Text = l6;
                        dataGridView6.Rows[6].Cells[1].Value = l6;
                        //yslonoe ydlinnenie pri razryve
                        string l7 = dataGridView1.Rows[i].Cells[11].Value.ToString();
                        label18.Text = l7;
                        dataGridView6.Rows[7].Cells[1].Value = l7;
                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nameXMlfile = "KATALOG.xml";
            nameXMlfile1 = "SOSTAV.xml";
            LoadXMLfile();
           
        }

       
    }
}
