using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiKho.Areas.Common
{
    public class KhoDb
    {
        Entities db = new Entities();
        // Get Staff ID
        public NhanVien GetByID(string id)
        {
            return db.NhanVien.FirstOrDefault(c => c.TenTK.Equals(id));
        }

        //Get Admin ID
        public ChuCuaHang GetByIDAdmin(string id)
        {
            return db.ChuCuaHang.FirstOrDefault(c => c.TenTK.Equals(id));
        }
        //Admin login
        public int LoginAdmin(string id, string pass, bool isLoginAdmin)
        {
            var q = db.ChuCuaHang.SingleOrDefault(c => c.TenTK == id);
            if (q == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (q.MaPQ == CommonConstant.Admin_Group)
                    {
                        if (q.Pwd == pass)
                        {
                            return 1;
                        }
                        else { return -1; }
                    }
                    else return -2;
                }
                else
                {
                    if (q.Pwd == pass)
                    {
                        return 1;
                    }
                    else { return -1; }
                }
            }
        }


        // Staff login
        public int Login(string id, string pass, bool isLoginAdmin)
        {
            var q = db.NhanVien.SingleOrDefault(c => c.TenTK == id);
            if (q == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if ((q.MaPQ == CommonConstant.Kho_Group) || (q.MaPQ == CommonConstant.Sale_Group))
                    {
                        if (q.Pwd == pass)
                        {
                            return 1;
                        }
                        else { return -1; }
                    }
                    else return -2;
                }
                else
                {
                    if (q.Pwd == pass)
                    {
                        return 1;
                    }
                    else { return -1; }
                }
            }
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.NhanVien.Single(x => x.TenTK == userName);
            var a = from p in db.Credential
                    join q in db.PhanQuyen on p.UserGroupID equals q.MaPQ
                    join e in db.Role on p.RoleID equals e.ID
                    where q.MaPQ == user.MaPQ
                    select p.RoleID;
            return a.ToList();
        }
        public List<string> GetListCredentialbyAdmin(string userName)
        {
            var user = db.ChuCuaHang.Single(x => x.TenTK == userName);
            var a = from p in db.Credential
                    join q in db.PhanQuyen on p.UserGroupID equals q.MaPQ
                    join e in db.Role on p.RoleID equals e.ID
                    where q.MaPQ == user.MaPQ
                    select p.RoleID;
            return a.ToList();
        }
        public HangHoa ViewDetail(string id)
        {
            HangHoa hoadon = db.HangHoa.SingleOrDefault(x => x.MaHH == id);
            return hoadon;
        }
        public bool Add_HoaDon(HoaDon hd)
        {
            try
            {
                db.HoaDon.Add(hd);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Add_CT_HoaDon(CT_HoaDon ct)
        {
            try
            {
                db.CT_HoaDon.Add(ct);
                db.SaveChanges();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}