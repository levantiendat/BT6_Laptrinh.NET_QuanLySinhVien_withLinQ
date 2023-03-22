using _102210247_LeVanTienDat.BLL;
using _102210247_LeVanTienDat.DAL;
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
    public partial class DetailForm : Form
    {
        public delegate void MyDel(int ID_Lop, string txt);
        public MyDel d { get; set; }
        public string MSSV { get; set; }
        public DetailForm(string mssv)
        {
            MSSV = mssv;
            InitializeComponent();
            SetCBB();
            GUI();
        }
        public void SetCBB()
        {
            List<CBBItems> list = new List<CBBItems>();
            QLSV_BLL bll = new QLSV_BLL();
            list.AddRange(bll.GetCBB1());
            textlopsh.Items.AddRange(list.ToArray());
            
        }
        public void GUI()
        {
            if (MSSV != "")
            {
                SINHVIEN sv = new SINHVIEN();
                QLSV_BLL bll = new QLSV_BLL();
                sv = bll.GetSVByMSSV(MSSV);
                textname.Text = sv.MSSV.ToString();
                textlopsh.Text = bll.GetLopbyID(Convert.ToInt32(sv.MALOP)).Text.ToString();
                textDate.Text = sv.NGAYSINH.ToString();
                if (sv.GIOITINH == true)
                {
                    nam.Checked = true;
                }
                else { nu.Checked = true; }
                textdtb.Text = sv.DIEMTRUNGBINH.ToString();
                anh.Checked =Convert.ToBoolean( sv.ANH);
                hocba.Checked = Convert.ToBoolean(sv.HOCBA);
                cccd.Checked = Convert.ToBoolean(sv.CCCD);
                textname.Enabled = false;
                this.Text = "EditForm";

            }
            else
            {
                this.Text = "AddForm";
            }
        }
        private void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        private void ok_Click(object sender, EventArgs e)
        {
            try
            {
                QLSV_BLL bll = new QLSV_BLL();
                SINHVIEN sv = new SINHVIEN();
                sv.MSSV=textname.Text;
                sv.MALOP = ((CBBItems)textlopsh.SelectedItem).Value;
                sv.NGAYSINH = Convert.ToDateTime(textDate.Text.ToString());
                sv.GIOITINH = nam.Checked;
                sv.DIEMTRUNGBINH=Convert.ToDouble(textdtb.Text.ToString());
                sv.ANH = anh.Checked;
                sv.HOCBA= hocba.Checked;
                sv.CCCD= cccd.Checked;
                if (MSSV != "")
                {
                    bll.UpdateSV(sv);
                    d(0, "");
                }
                else
                {
                    bll.AddSV(sv);
                    d(0, "");
                }
                this.Close();
            }
            catch
            {
                MessageBox.Show("Không thành công");
            }
            
            
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
