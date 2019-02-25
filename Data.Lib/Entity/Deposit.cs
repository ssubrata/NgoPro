using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Lib.Entity
{
    public class Deposit
    {
        public int Id { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime DepositDate { get; set; }

        public Book Book { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
