using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BettingApplication.Models
{

    public class SelectListItemComparer : EqualityComparer<SelectListItem>
    {
        public override bool Equals(SelectListItem x, SelectListItem y)
        {
            return x.Value.Equals(y.Value);
        }


        public override int GetHashCode(SelectListItem obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}

