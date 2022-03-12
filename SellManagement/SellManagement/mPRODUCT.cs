using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellManagement.Model;

namespace SellManagement
{
    class mPRODUCT
    {
        string prdcd, prdnm, untnm, form, category, status, note;
        double purchaseprice, saleprice, inventory, expiryday, weight;
        List<mPRODUCTSET> productsets;


        public mPRODUCT(string prdcd, string prdnm,  string untnm, string form, string category, string status, string note, double purchaseprice, double saleprice, double inventory, double expiryday, double weight)
        {
            this.prdcd = prdcd;
            this.prdnm = prdnm;
            this.untnm = untnm;
            this.form = form;
            this.category = category;
            this.status = status;
            this.note = note;
            this.purchaseprice = purchaseprice;
            this.saleprice = saleprice;
            this.inventory = inventory;
            this.expiryday = expiryday;
            this.weight = weight;
            this.productsets = new List<mPRODUCTSET>();
        }

        public mPRODUCT(string prdcd)
        {
            this.prdcd = prdcd;
            this.prdnm = "";
            this.untnm = "Cái";
            this.form = "Bánh lẻ";
            this.category = "Bánh mặn";
            this.status = "Đang kinh doanh";
            this.purchaseprice = 0;
            this.saleprice = 0;
            this.inventory = 0;
            this.expiryday = 0;
            this.weight = 0;
            this.productsets = new List<mPRODUCTSET>();
        }

        public string PRDCD { get => prdcd; set => prdcd = value; }
        public string PRDNM { get => prdnm; set => prdnm = value; }
        public string UNTNM { get => untnm; set => untnm = value; }
        public string FORM { get => form; set => form = value; }
        public string CATEGORY { get => category; set => category = value; }
        public string STATUS { get => status; set => status = value; }
        public double PURCHASEPRICE { get => purchaseprice; set => purchaseprice = value; }
        public double SALEPRICE { get => saleprice; set => saleprice = value; }
        public double INVENTORY { get => inventory; set => inventory = value; }
        public double EXPIRYDAY { get => expiryday; set => expiryday = value; }
        public double WEIGHT { get => weight; set => weight = value; }
        public string NOTE { get => note; set => note = value; }
        internal List<mPRODUCTSET> PRODUCTSETs { get => productsets; set => productsets = value; }
    }
}
