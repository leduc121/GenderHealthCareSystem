using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.DTOs
{
    public class StdDiseaseDTO
    {
        public string Id { get; set; } = null!;
        public string DiseaseName { get; set; } = null!;
        public decimal TestPrice { get; set; }
    }
}
