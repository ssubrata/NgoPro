using Data.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngo.Web.Models
{
    public class LoanModel
    {
        public int AccountId { get; set; }
        public string AccountNo { get; set; }
        public MemberModel Member { get; set; }
        public BookModel Book { get; set; }
        public List<ProvideLoanList> ProvideLoanList { get; set; }





    }
}
