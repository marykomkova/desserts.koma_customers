using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class ЕдиницыИзмерения
    {
        public ЕдиницыИзмерения()
        {
            Ингредиенты = new HashSet<Ингредиенты>();
            ТипыДесертов = new HashSet<ТипыДесертов>();
        }

        public long Номер { get; set; }
        public string Название { get; set; }

        public virtual ICollection<Ингредиенты> Ингредиенты { get; set; }
        public virtual ICollection<ТипыДесертов> ТипыДесертов { get; set; }
    }
}
