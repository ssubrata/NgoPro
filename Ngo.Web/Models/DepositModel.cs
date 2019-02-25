using Data.Lib.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngo.Web.Models
{
    public class DepositModel
    {
        public int AccountId { get; set; }
        public string AccountNo { get; set; }
        public MemberModel Member { get; set; }
        public BookModel Book { get; set; }
        public List<DepositCollectionModel> DepositCollection { get; set; }





    }
}
