using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class PasswordResetRequest
    {
        public virtual Guid RequestId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual bool PasswordChanged { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
