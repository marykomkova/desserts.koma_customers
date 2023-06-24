using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Ингредиенты
    {
        public Ингредиенты()
        {
            ИнгредиентыВдесерте = new HashSet<ИнгредиентыВдесерте>();
        }

        public long Номер { get; set; }
        public string Название { get; set; }
        public double? Количество { get; set; }
        public long? ЕдиницаИзмерения { get; set; }
        public double? Стоимость { get; set; }

        public virtual ЕдиницыИзмерения ЕдиницаИзмеренияNavigation { get; set; }
        public virtual ICollection<ИнгредиентыВдесерте> ИнгредиентыВдесерте { get; set; }
    }
}
