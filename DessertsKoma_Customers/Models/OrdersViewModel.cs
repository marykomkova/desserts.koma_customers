using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertsKoma_Customers.Models
{
    public class OrdersViewModel
    {
        public List<Заказы> OrdersModel { get; set; }
        public List<List<ДесертыВзаказе>> DessertsModel { get; set; }
        public Пользователи UserModel { get; set; }
    }
}
