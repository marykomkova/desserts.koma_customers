using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Десерты
    {
        public Десерты()
        {
            ДесертыВзаказе = new HashSet<ДесертыВзаказе>();
            ДесертыВкорзине = new HashSet<ДесертыВкорзине>();
            ИнгредиентыВдесерте = new HashSet<ИнгредиентыВдесерте>();
        }

        public long Номер { get; set; }
        public string Название { get; set; }
        public long Тип { get; set; }
        public long Изображение { get; set; }
        public int Количество { get; set; }
        public double? Стоимость { get; set; }

        public virtual Изображения ИзображениеNavigation { get; set; }
        public virtual ТипыДесертов ТипNavigation { get; set; }
        public virtual ICollection<ДесертыВзаказе> ДесертыВзаказе { get; set; }
        public virtual ICollection<ДесертыВкорзине> ДесертыВкорзине { get; set; }
        public virtual ICollection<ИнгредиентыВдесерте> ИнгредиентыВдесерте { get; set; }
    }
}
