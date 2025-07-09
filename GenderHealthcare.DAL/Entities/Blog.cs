using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderHealthcare.DAL.Entities
{
    public class Blog
    {
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Status { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User Author { get; set; } // Thuộc tính điều hướng đến User
    }
}
