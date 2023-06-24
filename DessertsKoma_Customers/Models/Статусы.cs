using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Статусы
    {
        public Статусы()
        {
            Заказы = new HashSet<Заказы>();
        }

        public long Номер { get; set; }
        public string Название { get; set; }

        public virtual ICollection<Заказы> Заказы { get; set; }
    }
}
