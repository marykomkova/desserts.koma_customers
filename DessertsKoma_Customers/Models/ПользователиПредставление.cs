using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class ПользователиПредставление
    {
        public string Логин { get; set; }
        public string Пароль { get; set; }
        public string Имя { get; set; }
        public string Роль { get; set; }
        public bool? ОтображениеНаСайте { get; set; }
        public bool? ПодтверждениеАккаунта { get; set; }
        public string НомерТелефона { get; set; }
        public long НомерИзображения { get; set; }
    }
}
