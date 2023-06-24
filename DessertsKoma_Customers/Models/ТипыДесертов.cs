using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class ТипыДесертов
    {
        public ТипыДесертов()
        {
            Десерты = new HashSet<Десерты>();
        }

        public long Номер { get; set; }
        public string Название { get; set; }
        public long? ЕдиницаИзмерения { get; set; }
        public long? Изображение { get; set; }
        public string Описание { get; set; }

        public virtual ЕдиницыИзмерения ЕдиницаИзмеренияNavigation { get; set; }
        public virtual Изображения ИзображениеNavigation { get; set; }
        public virtual ICollection<Десерты> Десерты { get; set; }
    }
}
