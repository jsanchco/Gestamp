namespace SGDE.Domain.ViewModels
{
    #region Using

    using System;

    #endregion

    public class OrderViewModel
    {
        public string Id { get; set; }

        public string Region { get; set; }
        public string Country { get; set; }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public string OrderPriority { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public DateTime ShipDate { get; set; }
        public int UnitsSlod { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalProfi { get; set; }
    }
}
