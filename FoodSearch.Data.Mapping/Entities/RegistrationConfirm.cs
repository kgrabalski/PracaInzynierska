using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class RegistrationConfirm
    {
        public virtual int ConfirmId { get; set; }
        public virtual Guid Code { get; set; }
        public virtual bool Confirmed { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
