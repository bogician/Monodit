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
using System.Reflection;

namespace Monondit_DB
{
    public partial class add : Form
    {
        public add()
        {
            InitializeComponent();
        }
        public static string sSort;
        DataView dv1,dv2,dv3,dv4;
        DataSet ds;
        bool isChangeSaved = true;
        public static string nameXMlfile = "KATALOG.XML";
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      //  public Form1 reftoform1 { get; set; }
        

        private void сохрпнитьToolStripMenuItem_Click(object sender, EventArgs e)

        {
            dataGridView1.AllowUserToAddRows = true;
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

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            dataGridView2.EndEdit();
            dataGridView3.EndEdit();
            dataGridView4.EndEdit();
            SaveXmlFile();
        }

        private void смесьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            dv1.AddNew();
            dv1.AddNew();
            int n = dataGridView1.RowCount;
            dv1.Delete(n-2);
            SaveXmlFile();


        }

        private void назначениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dv2.AddNew();
            dv2.AddNew();
            int n = dataGridView2.RowCount;
            dv2.Delete(n - 2);

            SaveXmlFile();

        }

        private void видИзделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dv3.AddNew();
            dv3.AddNew();
            int n = dataGridView3.RowCount;
            dv3.Delete(n - 2);

            SaveXmlFile();

        }

        private void типПолимераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dv4.AddNew();
            dv4.AddNew();
            int n = dataGridView4.RowCount;
            dv4.Delete(n - 2);

            SaveXmlFile();

        }

        private void add_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                SaveXmlFile();
                isChangeSaved = true;
            }
            else if (result == DialogResult.No)
            {
                isChangeSaved = true;
            }
            else
            {
                e.Cancel = true;
            }
           
        }

        private void add_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 frm = new Form1();
           frm.Show();
            frm.Refresh();
            //this.Close();
        }

        private void удалитьВыбранныйРецептToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            string s;
            if (dataGridView1.Rows[cr].Cells[0].Value.ToString() != null)
            {
                s = dataGridView1.Rows[cr].Cells[0].Value.ToString();
            }
            else
            {
                s = "Без названия";
            }
                if (MessageBox.Show("Вы действительно хотите удалить смесь " + s + "?", "Удаление данных", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                isChangeSaved = false;
                dv1.Delete(cr);
            }
        }
        int cr,cr1,cr2,cr3;

        private void dataGridView4_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cr3 = e.RowIndex;
        }

        private void удалитьВыбранноеНазначениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s;
            if (dataGridView2.Rows[cr1].Cells[1].Value.ToString() != null)
            {
                s = dataGridView2.Rows[cr1].Cells[1].Value.ToString();
            }
            else
            {
                s = "Без названия";
            }
            if (MessageBox.Show("Вы действительно хотите удалить назначение " + s + "?", "Удаление данных", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                isChangeSaved = false;
                dv2.Delete(cr1);
            }
        }

        private void удалитьВыбранныйТипИзделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s;
            if (dataGridView3.Rows[cr2].Cells[1].Value.ToString() != null)
            {
                s = dataGridView3.Rows[cr2].Cells[1].Value.ToString();
            }
            else
            {
                s = "Без названия";
            }
            if (MessageBox.Show("Вы действительно хотите удалить Вид изделия " + s + "?", "Удаление данных", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                isChangeSaved = false;
                dv3.Delete(cr2);
            }
        
    }

        private void удалитьВыбранныйТипПолимераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s;
            if (dataGridView4.Rows[cr3].Cells[1].Value.ToString() != null)
            {
                s = dataGridView4.Rows[cr3].Cells[1].Value.ToString();
            }
            else
            {
                s = "Без названия";
            }
            if (MessageBox.Show("Вы действительно хотите удалить тип полимера " + s + "?", "Удаление данных", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                isChangeSaved = false;
                dv4.Delete(cr3);
            }
        }

       
        private void dataGridView3_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cr2 = e.RowIndex;
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cr1 = e.RowIndex;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cr = e.RowIndex;
        }


        void SaveXmlFile()
        {
            dataGridView1.EndEdit();
            dataGridView2.EndEdit();
            dataGridView3.EndEdit();
            dataGridView4.EndEdit();
            FileStream fsWriteXml = new FileStream(nameXMlfile, FileMode.Create);
            ds.WriteXml(fsWriteXml);
            fsWriteXml.Close();
            isChangeSaved = true;
        }

        
        public static int x;
        private void add_Load(object sender, EventArgs e)
        {
            nameXMlfile = "KATALOG.xml";
            ds = new DataSet();
            FileStream fsReadxml = new FileStream(nameXMlfile, FileMode.Open);
            ds.ReadXml(fsReadxml, XmlReadMode.InferTypedSchema);
            fsReadxml.Close();
            dv1 = new DataView(ds.Tables[0]);
            dataGridView1.DataSource = dv1;
            dv2 = new DataView(ds.Tables[3]);
            dataGridView2.DataSource = dv2;
            dv3 = new DataView(ds.Tables[4]);
            dataGridView3.DataSource = dv3;
            dv4 = new DataView(ds.Tables[5]);
            dataGridView4.DataSource = dv4;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[0].HeaderText = "Шифр";
            dataGridView1.Columns[2].HeaderText = "Вид изделия";
            dataGridView1.Columns[3].HeaderText = "Назначение";
            dataGridView1.Columns[5].HeaderText = "Твердость по ШорА";
            dataGridView1.Columns[6].HeaderText = "Условная прочность при разрыве МПа";
            dataGridView1.Columns[11].HeaderText = "Условное удлиннение при разрыве";
            dataGridView1.Columns[8].HeaderText = "Тип полимера";
            dataGridView2.Columns[1].Width = 250;
            dataGridView2.Columns[2].Width = 300;
            dataGridView3.Columns[1].Width = 200;
            dataGridView4.Columns[1].Width = 200;
            PropertyInfo verticalOffset = dataGridView1.GetType().GetProperty("VerticalOffset", BindingFlags.NonPublic | BindingFlags.Instance);
            verticalOffset.SetValue(this.dataGridView1, 100000, null);
            isChangeSaved = false;
           
        }
    }
}
