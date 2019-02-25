using Data.Lib.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ngo.Web.ViewComponents
{
    public class AccountInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int? id)
        {
            var model = new Account { };
            if (id > 0 && id != null) model.Id = (int)id;
            return View(model);

        }
    }
}
