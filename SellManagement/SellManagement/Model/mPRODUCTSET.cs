using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellManagement.Model
{
    class mPRODUCTSET
    {
        string no, prdcd, prdnm;
        double quantity, weight, purchaseprice, saleprice;

        public mPRODUCTSET(string no, string prdcd, string prdnm, double quantity, double weight, double purchaseprice, double saleprice)
        {
            this.no = no;
            this.prdcd = prdcd;
            this.prdnm = prdnm;
            this.quantity = quantity;
            this.weight = weight;
            this.purchaseprice = purchaseprice;
            this.saleprice = saleprice;
        }

        public mPRODUCTSET(string no)
        {
            this.no = no;
            this.prdcd = "";
            this.prdnm = "";
            this.quantity = 0;
            this.weight = 0;
            this.purchaseprice = 0;
            this.saleprice = 0;
        }

        public string NO { get => no; set => no = value; }
        public string PRDCD { get => prdcd; set => prdcd = value; }
        public string PRDNM { get => prdnm; set => prdnm = value; }
        public double QUANTITY { get => quantity; set => quantity = value; }
        public double WEIGHT { get => weight; set => weight = value; }
        public double PURCHASEPRICE { get => purchaseprice; set => purchaseprice = value; }
        public double SALEPRICE { get => saleprice; set => saleprice = value; }
    }
}
