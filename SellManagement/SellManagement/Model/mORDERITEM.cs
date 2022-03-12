using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellManagement.Model
{
    public class mORDERITEM
    {
        string no, prdcd, prdnm, setcm, untnm, form, category, note;
        double quantity, weight, purchaseprice, saleprice, price, totalweight;
        List<mPRODUCTSET> mPRODUCTSETs;

        public mORDERITEM(string no, string prdcd, string prdnm, double quantity, double weight, double purchaseprice, double saleprice, string setcm,double totalweight)
        {
            this.no = no;
            this.prdcd = prdcd;
            this.prdnm = prdnm;
            this.quantity = quantity;
            this.weight = weight;
            this.purchaseprice = purchaseprice;
            this.saleprice = saleprice;
            this.price = quantity * saleprice;
            this.setcm = setcm;
            this.totalweight = totalweight;
            this.mPRODUCTSETs = new List<mPRODUCTSET>();
        }

        public mORDERITEM(string no)
        {
            this.no = no;
            this.prdcd = "";
            this.prdnm = "";
            this.quantity = 0;
            this.weight = 0;
            this.purchaseprice = 0;
            this.saleprice = 0;
            this.setcm = "";
            this.price = 0;
            this.mPRODUCTSETs = new List<mPRODUCTSET>();
        }

        public string NO { get => no; set => no = value; }
        public string PRDCD { get => prdcd; set => prdcd = value; }
        public string PRDNM { get => prdnm; set => prdnm = value; }
        public double QUANTITY { get => quantity; set => quantity = value; }
        public double WEIGHT { get => weight; set => weight = value; }
        public double PURCHASEPRICE { get => purchaseprice; set => purchaseprice = value; }
        public double SALEPRICE { get => saleprice; set => saleprice = value; }
        public string SETCM { get => setcm; set => setcm = value; }
        public double PRICE { get => price; set => price = value; }
        public double TOTALWEIGHT { get => totalweight; set => totalweight = value; }
        internal List<mPRODUCTSET> PRODUCTSETs { get => mPRODUCTSETs; set => mPRODUCTSETs = value; }
        public string UNTNM { get => untnm; set => untnm = value; }
        public string FORM { get => form; set => form = value; }
        public string CATEGORY { get => category; set => category = value; }
        public string NOTE { get => note; set => note = value; }

        public mORDERITEM Clone()
        {
            mORDERITEM oRDERITEM = new mORDERITEM(this.no, this.prdcd, this.prdnm, this.quantity, this.weight, this.purchaseprice, this.saleprice, this.setcm, this.totalweight);

            foreach (mPRODUCTSET prdsetItem in this.PRODUCTSETs)
            {
                oRDERITEM.PRODUCTSETs.Add(new mPRODUCTSET(prdsetItem.NO, prdsetItem.PRDCD, prdsetItem.PRDNM, prdsetItem.QUANTITY, prdsetItem.WEIGHT, prdsetItem.PURCHASEPRICE, prdsetItem.SALEPRICE));
            }

            return oRDERITEM;
        }
    }
}
