using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Lib.Entity
{
  public  class Loan
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }

        public Book Book { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public List<LoanCollection> LoanCollections { get; set; }
    }
}
