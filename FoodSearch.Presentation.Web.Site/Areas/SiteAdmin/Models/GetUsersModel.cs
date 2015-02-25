using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Models
{
    public class GetUsersModel
    {
        public string Query { get; set; }
        [NullableIntRange(0, int.MaxValue)]
        public int? Page { get; set; }
        [NullableIntRange(1, 250)]
        public int? PageSize { get; set; }
    }
}
