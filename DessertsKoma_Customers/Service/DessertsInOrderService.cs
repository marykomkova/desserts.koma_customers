using DessertsKoma_Customers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertsKoma_Customers.Service
{
    public class DessertsInOrderService
    {
        private readonly DessertsKomaContext _context;

        public DessertsInOrderService(DessertsKomaContext context)
        {
            _context = context;
        }

        public List<Заказы> GetЗаказы(long user)
        {
            var orders = _context.Заказы
                .Include(d => d.ПользовательNavigation)
                .Where(x => x.ПользовательNavigation.Номер == user)
                .Include(d => d.СотрудникNavigation)
                .Include(d => d.СкидкаNavigation)
                .Include(d => d.СтатусNavigation)
                .ToList();

            return orders;
        }

        public List<List<ДесертыВзаказе>> GetДесертыВЗаказах(long user)
        {
            var desserts = new List<List<ДесертыВзаказе>>();
            foreach(var ord in GetЗаказы(user))
            {
                var des = _context.ДесертыВзаказе.Where(d => d.Заказ == ord.Номер)
                    .Include(d => d.ДесертNavigation)
                    .ThenInclude(d => d.ИзображениеNavigation)
                    .Include(d => d.ДесертNavigation)
                    .ThenInclude(d => d.ТипNavigation).ToList();
                desserts.Add(des);
            }
            return desserts;
        }
    }
}
