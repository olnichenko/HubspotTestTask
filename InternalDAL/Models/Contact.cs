using InternalDAL.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalDAL.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Contact : BaseEntity<long>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
