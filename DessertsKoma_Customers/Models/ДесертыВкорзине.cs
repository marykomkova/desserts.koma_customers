using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class ДесертыВкорзине
    {
        public long Номер { get; set; }
        public long Корзина { get; set; }
        public long Десерт { get; set; }
        public int Количество { get; set; }

        public virtual Десерты ДесертNavigation { get; set; }
        public virtual Корзина КорзинаNavigation { get; set; }
    }
}
