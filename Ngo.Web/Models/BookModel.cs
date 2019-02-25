using Data.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngo.Web.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public bool IsActive { get; set; }
        public string PaymentAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
