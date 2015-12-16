using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLiKho.Models.Entities;

namespace QuanLiKho.Areas.Admin.Models
{
    public class GetMa
    {
        private Entities db = new Entities();
        public  string MaHoaDon()
        {
            string maHD = "";
            if (db.HoaDon.Count() != 0)
            {
                var hds = (from p in db.HoaDon
                           orderby p.MaHD descending
                           select p).Skip(0).Take(1);
        string numberString = hds.ToList()[0].MaHD.Substring(2);
        int number = Convert.ToInt32(numberString);
        number++;
                numberString = number.ToString();
                while (numberString.Length< 5)
                {
                    numberString = 0 + numberString;
                }
             maHD = "HD" + numberString;
            }
            else
            {
                maHD = "HD00001";
            }
            return maHD;
        }
        public string MaCongNo()
        {
            string maCN = "";
            if (db.CongNo.Count() != 0)
            {
                var cns = (from p in db.CongNo
                           orderby p.MaCN descending
                           select p).Skip(0).Take(1);
                string numberString = cns.ToList()[0].MaCN.Substring(2);
                int number = Convert.ToInt32(numberString);
                number++;
                numberString = number.ToString();
                while (numberString.Length < 5)
                {
                    numberString = 0 + numberString;
                }
                maCN = "CN" + numberString;
            }
            else
            {
                maCN = "CN00001";
            }
            return maCN;
        }
        public string MaPhieu()
        {

            string maPH = "";
            if (db.PhieuThuChi.Count() != 0)
            {
                var ph = (from p in db.PhieuThuChi
                          orderby p.MaPH descending
                          select p).Skip(0).Take(1);
                string numberString = ph.ToList()[0].MaPH.Substring(2);
                int number = Convert.ToInt32(numberString);
                number++;
                numberString = number.ToString();
                while (numberString.Length < 5)
                {
                    numberString = 0 + numberString;
                }
                maPH = "PH" + numberString;
            }
            else
            {
                maPH = "PH00001";
            }
            return maPH;
        }
        public string MaHangHoa()
        {

            string maHH = "";
            if (db.HangHoa.Count() != 0)
            {
                var Hh = (from p in db.HangHoa
                          orderby p.MaHH descending
                          select p).Skip(0).Take(1);
                string numberString = Hh.ToList()[0].MaHH.Substring(2);
                int number = Convert.ToInt32(numberString);
                number++;
                numberString = number.ToString();
                while (numberString.Length < 5)
                {
                    numberString = 0 + numberString;
                }
                maHH = "HH" + numberString;
            }
            else
            {
                maHH = "HH00001";
            }
            return maHH;
        }
        public string MaDoiTac()
        {
            string maDT = "";
            if (db.DoiTac.Count() != 0)
            {
                var dt = (from p in db.DoiTac
                          orderby p.MaDT descending
                          select p).Skip(0).Take(1);
                string numberString = dt.ToList()[0].MaDT.Substring(2);
                int number = Convert.ToInt32(numberString);
                number++;
                numberString = number.ToString();
                while (numberString.Length < 5)
                {
                    numberString = 0 + numberString;
                }
                maDT = "DT" + numberString;
            }
            else
            {
                maDT = "DT00001";
            }
            return maDT;
        }
        public string MaNganQuy()
        {
            string maNQ = "";
            if (db.NganQuy.Count() != 0)
            {
                var nq = (from p in db.NganQuy
                          orderby p.MaNQ descending
                          select p).Skip(0).Take(1);
                string numberString = nq.ToList()[0].MaNQ.Substring(2);
                int number = Convert.ToInt32(numberString);
                number++;
                numberString = number.ToString();
                while (numberString.Length < 5)
                {
                    numberString = 0 + numberString;
                }
                maNQ = "NQ" + numberString;
            }
            else
            {
                maNQ = "NQ00001";
            }
            return maNQ;
        }
    }
}