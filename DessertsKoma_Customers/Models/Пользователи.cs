using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Пользователи
    {
        public Пользователи()
        {
            ЗаказыПользовательNavigation = new HashSet<Заказы>();
            ЗаказыСотрудникNavigation = new HashSet<Заказы>();
            Корзина = new HashSet<Корзина>();
        }

        public long Номер { get; set; }
        public string Логин { get; set; }
        public string Пароль { get; set; }
        [NotMapped]
        public string ПодтверждениеПароля { get; set; }
        public string Имя { get; set; }
        public long? Изображение { get; set; }
        public long? Роль { get; set; }
        public bool? ОтображениеНаСайте { get; set; }
        public bool? Подтверждение { get; set; }
        public string НомерТелефона { get; set; }

        public virtual Изображения ИзображениеNavigation { get; set; }
        public virtual Роли РольNavigation { get; set; }
        public virtual ICollection<Заказы> ЗаказыПользовательNavigation { get; set; }
        public virtual ICollection<Заказы> ЗаказыСотрудникNavigation { get; set; }
        public virtual ICollection<Корзина> Корзина { get; set; }
    }
}
