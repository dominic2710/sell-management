using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellManagement.UC;

namespace SellManagement.Model
{
    class mORDERDETAIL
    {
        string orderno, customercd, customernm, phone, address;
        List<mORDERITEM> orderitems;

        public mORDERDETAIL(string orderno, string customercd, string customernm, string phone, string address, List<mORDERITEM> orderitems)
        {
            this.orderno = orderno;
            this.customercd = customercd;
            this.customernm = customernm;
            this.phone = phone;
            this.address = address;
            this.orderitems = orderitems ;
        }

        public string ORDERNO { get => orderno; set => orderno = value; }
        public string CUSTOMERCD { get => customercd; set => customercd = value; }
        public string CUSTOMERNM { get => customernm; set => customernm = value; }
        public string PHONE { get => phone; set => phone = value; }
        public string ADDRESS { get => address; set => address = value; }
        internal List<mORDERITEM> ORDERITEMS { get => orderitems; set => orderitems = value; }
    }
}
