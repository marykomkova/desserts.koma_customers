using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Скидка
    {
        public Скидка()
        {
            Заказы = new HashSet<Заказы>();
            Корзина = new HashSet<Корзина>();
        }

        public long Номер { get; set; }
        public string Фраза { get; set; }
        public int Процент { get; set; }

        public virtual ICollection<Заказы> Заказы { get; set; }
        public virtual ICollection<Корзина> Корзина { get; set; }
    }
}
