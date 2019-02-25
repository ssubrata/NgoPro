using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Lib.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public bool IsActive { get; set; }
        public string PaymentAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
