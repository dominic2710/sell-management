using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellManagement.Model
{
    class mPRODUCT
    {
        string prdcd, prdnm, untnm, form, category, status, note;
        double purchaseprice, saleprice, inventory, expiryday, weight;

        public mPRODUCT(string prdcd, string prdnm,  string untnm, string form, string category, string status, double purchaseprice, double saleprice, double inventory, double expiryday, double weight)
        {
            this.prdcd = prdcd;
            this.prdnm = prdnm;
            this.untnm = untnm;
            this.form = form;
            this.category = category;
            this.status = status;
            this.purchaseprice = purchaseprice;
            this.saleprice = saleprice;
            this.inventory = inventory;
            this.expiryday = expiryday;
            this.weight = weight;
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
    }
}
