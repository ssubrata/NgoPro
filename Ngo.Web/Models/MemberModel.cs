using Data.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ngo.Web.Models
{
    public class MemberModel : IValidatableObject
    {
        public int Id { get; set; }
        
        public string MemberNo { get; set; }
        public string FullName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string FatherName { get; set; }

       
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db=validationContext.GetRequiredService<DataDbContext>();

            if (db.Members.Where(p => p.Id != this.Id && p.MemberNo == this.MemberNo).Count() > 0)
            {
                yield return new ValidationResult("memberNo already in use, please chose a different one. ", new[] { "MemberNo" });
            }
            if (db.Members.Where(p => p.Id != this.Id && p.NationalNo == this.NationalNo).Count() > 0)
            {
                yield return new ValidationResult("nationalNo already in use, please chose a different one. ", new[] { "NationalNo" });
            }

           
        }

      
    }
}
