using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using NhanVien;

namespace QuanlyNhanSu
{
    
    class Program
    {
        //Lay ssn tu ten
        public static string GetSSNFromFName(string name)
        {
            DataTable dtA = GetData.GetSSNFromName(name);
            DataRow dtARow = dtA.Rows[0];
            var ssn = dtARow["ssn"].ToString();
            return ssn;
        }
        //Kiem tra nhan vien partTime hay FullTime theo ten
        public static string CheckNV(string name)
        {
            DataTable dtA = GetData.GetFullDataFromName(name);
            DataRow dtARow = dtA.Rows[0];
            var type = dtARow["type"].ToString();
            return type;
        }
        //Kiem tra nhan vien partTime hay FullTime theo SSN
        public static string CheckNV2(string ssn)
        {
            DataTable dtA2 = GetData.GetFullDataFromSSN(ssn);
            DataRow dtA2Row = dtA2.Rows[0];
            var type2 = dtA2Row["type"].ToString();
            return type2;
        }
        //Kiem tra ten trung nhau
        public static int CheckTen(string name)
        {
            bool isContinue = true;
            int counter = 0;
            int dem = 0;
            int dem2=0;
            while (isContinue)
            {
                DataTable fulldata;
                fulldata = GetData.GetFullDataEmployee();
                DataRow fulldataRow = fulldata.Rows[counter];
                var fullTen = fulldataRow["fullName"].ToString();
                if (fulldataRow != null && fulldataRow[0] != null)
                {
                    if (name == fullTen)
                    {
                        dem++;
                    }
                }
                counter++;
                isContinue = (counter < fulldata.Rows.Count);
            }
            if (dem == 0)
            {
                dem2 = 0;
            }
            else if(dem==1)
            {
                dem2 = 1;
            }
            else
            {
                dem2 = 2;
            }
            return dem2;
        }
        
        static void Main(string[] args)
        {
            #region In thong tin nhan vien tu ten nhan vien
            string A = "Nguyen Van A";
            //Neu khong co ten trong danh sach
            if (CheckTen(A)==0)
            {
                Console.WriteLine("Khong co nguoi nay trong cong ty");
            }
            //Neu ten khong bi trung tim theo ten
            else if (CheckTen(A) == 1)
            {
                //Lay thong tin co ban
                DataTable dtA = GetData.GetFullDataFromName(A);
                DataRow dtARow = dtA.Rows[0];                                      
                if (CheckNV(A) == "FullTime  ")
                {
                    DataTable dtA2 = GetData.GetSalaryFullTimeFromSSN(GetSSNFromFName(A));
                    DataRow dtA2Row = dtA2.Rows[0];
                    EplFullTime ifFullTime = new EplFullTime(dtARow["ssn"].ToString(), dtARow["firstName"].ToString(), dtARow["lastName"].ToString(), dtARow["birthDate"].ToString(), dtARow["phone"].ToString(), dtARow["email"].ToString(), (int)dtARow["department"], (int)dtA2Row["commissionRate"], (int)dtA2Row["grossSales"], (int)dtA2Row["basicSalary"]);
                    ifFullTime.GetinfoFullTime();
                }
                else if (CheckNV(A) == "PartTime  ")
                {
                    DataTable dtA3 = GetData.GetSalaryPartTimeFromSSN(GetSSNFromFName(A));
                    DataRow dtA3Row = dtA3.Rows[0];
                    EplPartTime ifPartTime = new EplPartTime(dtARow["ssn"].ToString(), dtARow["firstName"].ToString(), dtARow["lastName"].ToString(), dtARow["birthDate"].ToString(), dtARow["phone"].ToString(), dtARow["email"].ToString(), (int)dtARow["department"], (int)dtA3Row["rate"], (int)dtA3Row["workingHours"]);
                    ifPartTime.GetinfoPartTime();
                }
            }
            //Neu ten bi trung, in ra ma SSN cua nhung nguoi do
            else if (CheckTen(A) == 2)
            {
                bool isContinue = true;
                int counter = 0;
                int dem = 0;
                while (isContinue)
                {
                    DataTable fulldata;
                    fulldata = GetData.GetFullDataEmployee();
                    DataRow fulldataRow = fulldata.Rows[counter];
                    var fullTen = fulldataRow["fullName"].ToString();
                    var SSN = fulldataRow["ssn"].ToString();
                    if (fulldataRow != null && fulldataRow[0] != null)
                    {
                        if (A == fullTen)
                        {
                            Console.WriteLine("SSN: " + SSN);
                        }
                    }
                    counter++;
                    isContinue = (counter < fulldata.Rows.Count);
                }
                //Sau khi co danh sach ma cua nhung nguoi co ten bi trung, nhap ma ssn cua nguoi muon lay thong tin
                Console.WriteLine("Nhap ma ssn cua nguoi muon lay thong tin: ");
                string n = Console.ReadLine();
                //Lay thong tin co ban
                DataTable dtA = GetData.GetFullDataFromSSN(n);
                DataRow dtARow = dtA.Rows[0];
                if (CheckNV2(n) == "FullTime  ")
                {
                    DataTable dtA2 = GetData.GetSalaryFullTimeFromSSN(n);
                    DataRow dtA2Row = dtA2.Rows[0];
                    EplFullTime ifFullTime = new EplFullTime(dtARow["ssn"].ToString(), dtARow["firstName"].ToString(), dtARow["lastName"].ToString(), dtARow["birthDate"].ToString(), dtARow["phone"].ToString(), dtARow["email"].ToString(), (int)dtARow["department"], (int)dtA2Row["commissionRate"], (int)dtA2Row["grossSales"], (int)dtA2Row["basicSalary"]);
                    ifFullTime.GetinfoFullTime();
                }
                else if (CheckNV2(n) == "PartTime  ")
                {
                    DataTable dtA3 = GetData.GetSalaryPartTimeFromSSN(n);
                    DataRow dtA3Row = dtA3.Rows[0];
                    EplPartTime ifPartTime = new EplPartTime(dtARow["ssn"].ToString(), dtARow["firstName"].ToString(), dtARow["lastName"].ToString(), dtARow["birthDate"].ToString(), dtARow["phone"].ToString(), dtARow["email"].ToString(), (int)dtARow["department"], (int)dtA3Row["rate"], (int)dtA3Row["workingHours"]);
                    ifPartTime.GetinfoPartTime();
                }
            }
            #endregion
        }
    }
}
