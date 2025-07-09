using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderHealthcare.UI.ViewModels
{
    public class StdDiseaseViewModel
    {
        public string Id { get; set; } = null!;
        public string DiseaseName { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public string OfferName { get; set; } = "";
        public decimal DiscountValue { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
