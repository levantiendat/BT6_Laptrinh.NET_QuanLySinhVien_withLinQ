using _102210247_LeVanTienDat.BLL;
using _102210247_LeVanTienDat.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _102210247_LeVanTienDat
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "Hệ Thống Quản Lý Sinh Viên";
            Setlop1();
        }
        public void Setlop1()
        {
            List<CBBItems> list = new List<CBBItems>();
            QLSV_BLL bll=new QLSV_BLL();
            list.AddRange(bll.GetCBB());
            lop1.Items.AddRange(list.ToArray());
            lop1.SelectedIndex = 0;
        }
        public void ShowDGV(int ID_Lop,string txt)
        {
            QLSV_BLL bll = new QLSV_BLL();
            data.DataSource = bll.GetSVBySearch(ID_Lop, txt);
        }

        private void lop1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItems)lop1.SelectedItem).Value;
            ShowDGV(ID_Lop, "");
        }

        private void buttonsearch_Click(object sender, EventArgs e)
        {
            string txt = textsearch.Text.ToString();
            int ID_Lop = ((CBBItems)lop1.SelectedItem).Value;
            ShowDGV(ID_Lop, txt);
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            DetailForm f = new DetailForm("");
            f.d += new DetailForm.MyDel(ShowDGV);
            f.Show();
        }

        private void buttonedit_Click(object sender, EventArgs e)
        {
            if (data.SelectedRows.Count == 1)
            {
                string mssv = data.SelectedRows[0].Cells["MSSV"].Value.ToString();
                DetailForm f = new DetailForm(mssv);
                f.d += new DetailForm.MyDel(ShowDGV);
                f.Show();
            }
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            if (data.SelectedRows.Count > 0)
            {
                List<string> list = new List<string>();
                foreach(DataGridViewRow i in data.SelectedRows)
                {
                    DialogResult at = MessageBox.Show("Bạn có muốn xóa Sinh viên có mã số " + i.Cells["MSSV"].Value.ToString() + " không ?", "Confirm", MessageBoxButtons.OKCancel);
                    if (at == DialogResult.OK)
                    {
                        list.Add(i.Cells["MSSV"].Value.ToString());

                    }
                    
                }
                QLSV_BLL bLL= new QLSV_BLL();
                bLL.DeleteSV(list);
                ShowDGV(0, "");
            }
        }

        private void buttonsort_Click(object sender, EventArgs e)
        {
            try
            {
                int ID_Lop = ((CBBItems)lop1.SelectedItem).Value;
                string txt = sort.Text.ToString();
                QLSV_BLL bll = new QLSV_BLL();

                data.DataSource = bll.Sort(ID_Lop, txt);
            }
            catch
            {
                MessageBox.Show("Sắp xếp không thành công");
            }
        }
    }
}
