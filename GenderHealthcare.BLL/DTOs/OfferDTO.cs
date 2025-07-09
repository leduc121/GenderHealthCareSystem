using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.DTOs
{
    public class OfferDTO
    {
        public string Id { get; set; } = null!;
        public string OfferName { get; set; } = null!;
        public string OfferType { get; set; } = null!; // "Percentage" hoặc "Fixed"
        public decimal DiscountValue { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ApplicableServices { get; set; } = null!;
    }
}
