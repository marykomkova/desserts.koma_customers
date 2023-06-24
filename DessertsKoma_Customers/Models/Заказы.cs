using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Заказы
    {
        public Заказы()
        {
            ДесертыВзаказе = new HashSet<ДесертыВзаказе>();
        }

        public long Номер { get; set; }
        public long Пользователь { get; set; }
        public DateTime ДатаЗаказа { get; set; }
        public long Статус { get; set; }
        public DateTime ДатаВыдачи { get; set; }
        public long? Сотрудник { get; set; }
        public double? Стоимость { get; set; }
        public long? Скидка { get; set; }

        public virtual Пользователи ПользовательNavigation { get; set; }
        public virtual Скидка СкидкаNavigation { get; set; }
        public virtual Пользователи СотрудникNavigation { get; set; }
        public virtual Статусы СтатусNavigation { get; set; }
        public virtual ICollection<ДесертыВзаказе> ДесертыВзаказе { get; set; }
    }
}
