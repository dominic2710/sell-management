﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellManagement.Api.Functions
{
    public class SellOrder
    {
        public int Id { get; set; }
        public string SellOrderNo { get; set; }
        public DateTime SellOrderDate { get; set; }
        public DateTime PlannedExportDate { get; set; }
        public string CustomerCd { get; set; }
        public string CustomerName { get; set; }
        public string ShippingCompanyCd { get; set; }
        public string ShippingCompanyName { get; set; }
        public int Status { get; set; }
        public int ForControlStatus { get; set; }
        public int SummaryCost { get; set; }
        public int ShippingCost { get; set; }
        public int SaleOffCost { get; set; }
        public int PaidCost { get; set; }
        public int SellCost { get; set; }
        public string Note { get; set; }
        public IEnumerable<SellOrderDetail> SellOrderDetails { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
