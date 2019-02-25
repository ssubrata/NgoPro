using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Lib.Entity
{
   public class LoanCollection
    {
        public int Id { get; set; }
        public decimal LoanCollectAmount { get; set; }
        public DateTime LoanCollectDate { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}
