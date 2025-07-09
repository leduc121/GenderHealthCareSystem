using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.DTOs
{
    public class ConsultantDto
    {
        public string Id { get; set; }          // trùng với consultant_id trong bảng users
        public string FullName { get; set; }    // first_name + " " + last_name
        public bool IsAvailable { get; set; }   // từ ConsultantProfile.IsAvailable
    }

}
