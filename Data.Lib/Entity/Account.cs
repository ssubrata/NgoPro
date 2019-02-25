using System;
using System.Collections.Generic;

namespace Data.Lib.Entity
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string Status { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime CreateDate { get; set; }
     
        public List<Deposit> Deposit { get; set; }
        public List<Loan> Loan { get; set; }

    }
}