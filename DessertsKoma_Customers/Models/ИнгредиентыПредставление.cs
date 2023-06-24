using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class ИнгредиентыПредставление
    {
        public long Номер { get; set; }
        public string Ингредиент { get; set; }
        public double? Количество { get; set; }
        public string ЕдиницаИзмерения { get; set; }
        public double? Стоимость { get; set; }
    }
}
