using _102210247_LeVanTienDat.DAL;
using _102210247_LeVanTienDat.DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _102210247_LeVanTienDat.BLL
{
    internal class QLSV_BLL
    {
        
        public List<CBBItems> GetCBB()
        {
            QLSVDataContext db=new QLSVDataContext();
            List<CBBItems> list = new List<CBBItems>();
            list.Add(new CBBItems { Value=0,Text="All"});
            foreach(LOPSINHHOAT i in db.LOPSINHHOATs)
            {
                list.Add(new CBBItems { Value = i.ID, Text = i.LOPSH });
            }
            return list;
        }
        public List<CBBItems> GetCBB1()
        {
            QLSVDataContext db = new QLSVDataContext();
            List<CBBItems> list = new List<CBBItems>();
            
            foreach (LOPSINHHOAT i in db.LOPSINHHOATs)
            {
                list.Add(new CBBItems { Value = i.ID, Text = i.LOPSH });
            }
            return list;
        }
        public List<SINHVIEN> GetSVByIDLop(int ID_Lop)
        {
            List<SINHVIEN> sv = new List<SINHVIEN>();
            QLSVDataContext db = new QLSVDataContext();
            if(ID_Lop==0)
            foreach(var i in db.SINHVIENs)
            {
                SINHVIEN s = GetSVByMSSV(i.MSSV);
                sv.Add(s);
            }
            else
            {
                foreach (var i in db.SINHVIENs)
                {
                    SINHVIEN s = GetSVByMSSV(i.MSSV);
                    if(s.MALOP==ID_Lop) sv.Add(s);
                    
                }
            }
            
            
            return sv;

        }
        public SINHVIEN GetSVByMSSV(string mssv)
        {
            QLSVDataContext db = new QLSVDataContext();
            var result = (from s in db.SINHVIENs
                          where s.MSSV == mssv
                          select s).FirstOrDefault();
            if (result != null)
            {
                SINHVIEN sv = new SINHVIEN();
                sv.MSSV = result.MSSV;
                sv.MALOP = result.MALOP;
                sv.NGAYSINH = result.NGAYSINH;
                sv.GIOITINH = result.GIOITINH;
                sv.DIEMTRUNGBINH = result.DIEMTRUNGBINH;
                sv.ANH = result.ANH;
                sv.HOCBA = result.HOCBA;
                sv.CCCD = result.CCCD;
                sv.LOPSINHHOAT = result.LOPSINHHOAT;
                
                return sv;
            }
            else
            {
                return null;
            }
        }
        public CBBItems GetLopbyID(int ID_Lop)
        {
            QLSVDataContext db = new QLSVDataContext();
            var result = (from p in db.LOPSINHHOATs
                          where p.ID == ID_Lop
                          select p
                        ).FirstOrDefault();
            if (result != null)
            {
                CBBItems p=new CBBItems();
                p.Value = result.ID;
                p.Text = result.LOPSH;
                return p;

            }
            else
            {
                return null;
            }
        }
        public List<SINHVIEN> GetSVBySearch(int ID_Lop, string txt)
        {
            List<SINHVIEN> sv = new List<SINHVIEN>();
            QLSVDataContext db=new QLSVDataContext();
            List<SINHVIEN> sv1=new List<SINHVIEN>();
            sv1.AddRange( GetSVByIDLop(ID_Lop));
            if(txt.Length > 0) 
            { 
                foreach(SINHVIEN i in sv1)
                {
                    if (txt.CompareTo(i.MSSV) == 0)
                    {
                        sv.Add(i);
                    }
                }
            }
            else
            {
                sv.AddRange(sv1);
            }

            return sv;
            
        }
        
        public void AddSV(SINHVIEN sv)
        {
            QLSVDataContext db = new QLSVDataContext();
            try
            {
                
                db.SINHVIENs.InsertOnSubmit(sv);
            }
            catch
            {
                
            }
            finally
            {
                db.SubmitChanges();
                
            }
        }
        public void UpdateSV(SINHVIEN sv)
        {
            QLSVDataContext db = new QLSVDataContext();
            try
            {
                var s = db.SINHVIENs.Where(p => p.MSSV == sv.MSSV).FirstOrDefault();
                s.MALOP=sv.MALOP;
                s.NGAYSINH=sv.NGAYSINH;
                s.GIOITINH = sv.GIOITINH;
                s.DIEMTRUNGBINH=sv.DIEMTRUNGBINH;
                s.ANH = sv.ANH;
                s.HOCBA=sv.HOCBA;
                s.CCCD=sv.CCCD;
                
            }
            catch
            {
                
            }
            finally
            {
                db.SubmitChanges();
                
            }
        }
        public void DeleteSV(List<string> mssv)
        {
            QLSVDataContext db = new QLSVDataContext();
            try
            {
                foreach(string i in mssv)
                {
                    var s=db.SINHVIENs.Where(p=>p.MSSV==i).FirstOrDefault();
                    db.SINHVIENs.DeleteOnSubmit(s);
                }
            }
            finally { db.SubmitChanges(); }
        }
        public List<SINHVIEN> Sort(int MALOP,string txt)
        {
            List<SINHVIEN> data=new List<SINHVIEN>();
            data = GetSVByIDLop(MALOP);
            if (txt == "MSSV")
            {
                for (int i = 0; i < data.Count - 1; i++)
                {
                    for (int j = i + 1; j < data.Count; j++)
                    {
                        if (data[i].MSSV.CompareTo(data[j].MSSV) > 0)
                        {
                            SINHVIEN tmp = new SINHVIEN();
                            tmp = data[i];
                            data[i] = data[j];
                            data[j] = tmp;
                        }
                    }
                }
            }
            else if (txt == "MALOP")
            {
                for (int i = 0; i < data.Count - 1; i++)
                {
                    for (int j = i + 1; j < data.Count; j++)
                    {
                        if (data[i].MALOP > data[j].MALOP)
                        {
                            SINHVIEN tmp = new SINHVIEN();
                            tmp = data[i];
                            data[i] = data[j];
                            data[j] = tmp;
                        }
                    }
                }
            }
            else if (txt == "NGAYSINH")
            {
                for (int i = 0; i < data.Count - 1; i++)
                {
                    for (int j = i + 1; j < data.Count; j++)
                    {
                        if (data[i].NGAYSINH> data[j].NGAYSINH)
                        {
                            SINHVIEN tmp = new SINHVIEN(); 
                            tmp = data[i];
                            data[i] = data[j];
                            data[j] = tmp;
                        }
                    }
                }
            }
            else if (txt == "DIEMTRUNGBINH")
            {
                for (int i = 0; i < data.Count - 1; i++)
                {
                    for (int j = i + 1; j < data.Count; j++)
                    {
                        if (data[i].DIEMTRUNGBINH < data[j].DIEMTRUNGBINH)
                        {
                            SINHVIEN tmp = new SINHVIEN(); 
                            tmp = data[i];
                            data[i] = data[j];
                            data[j] = tmp;
                        }
                    }
                }
            }
            


            return data;

        }
        
    }
}
