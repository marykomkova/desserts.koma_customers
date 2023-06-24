using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class ЗаказыПредставление
    {
        public string Логин { get; set; }
        public DateTime ДатаЗаказа { get; set; }
        public string Название { get; set; }
        public long НомерЗаказа { get; set; }
    }
}
