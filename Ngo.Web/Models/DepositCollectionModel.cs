using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngo.Web.Models
{
    public class DepositCollectionModel
    {
        public int Id { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime DepositDate { get; set; }

    }
}
