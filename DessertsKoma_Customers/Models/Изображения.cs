using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class Изображения
    {
        public Изображения()
        {
            Десерты = new HashSet<Десерты>();
            Пользователи = new HashSet<Пользователи>();
            ТипыДесертов = new HashSet<ТипыДесертов>();
        }

        public long Номер { get; set; }
        public string ИмяФайла { get; set; }
        public string Название { get; set; }
        public byte[] Данные { get; set; }

        public virtual ICollection<Десерты> Десерты { get; set; }
        public virtual ICollection<Пользователи> Пользователи { get; set; }
        public virtual ICollection<ТипыДесертов> ТипыДесертов { get; set; }
    }
}
