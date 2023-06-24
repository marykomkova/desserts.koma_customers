using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Корзина
    {
        public Корзина()
        {
            ДесертыВкорзине = new HashSet<ДесертыВкорзине>();
        }

        public long Номер { get; set; }
        public long Пользователь { get; set; }
        public long? Скидка { get; set; }

        public virtual Пользователи ПользовательNavigation { get; set; }
        public virtual Скидка СкидкаNavigation { get; set; }
        public virtual ICollection<ДесертыВкорзине> ДесертыВкорзине { get; set; }
    }
}
