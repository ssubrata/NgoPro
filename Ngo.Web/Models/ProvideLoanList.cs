using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngo.Web.Models
{
    public class ProvideLoanList
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime LoanDate { get; set; }
    }
}
