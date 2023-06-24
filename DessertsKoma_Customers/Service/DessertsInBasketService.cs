using DessertsKoma_Customers.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DessertsKoma_Customers.Service
{
    public class DessertsInBasketService
    {
        private readonly DessertsKomaContext _context;

        public DessertsInBasketService(DessertsKomaContext context)
        {
            _context = context;
        }

        public List<ДесертыВкорзине> GetДесертыВКорзине(long user)
        {
            var dessertsInBasket = _context.ДесертыВкорзине
                .Include(d => d.КорзинаNavigation)
                .Where(x => x.КорзинаNavigation.Пользователь == user)
                .Include(d => d.ДесертNavigation)
                .ThenInclude(d => d.ИзображениеNavigation)
                .Include(d => d.ДесертNavigation.ТипNavigation)
                .ToList();

            return dessertsInBasket;
        }

        public int GetСкидка(long user)
        {
            return _context.Корзина
                .Include(d => d.СкидкаNavigation)
                .Where(x => x.Пользователь == user)
                .Select(x => x.СкидкаNavigation.Процент)
                .FirstOrDefault();
        }

        public long GetСкидкаNumber(long user)
        {
            return _context.Корзина
                .Include(d => d.СкидкаNavigation)
                .Where(x => x.Пользователь == user)
                .Select(x => x.СкидкаNavigation.Номер)
                .FirstOrDefault();
        }
    }
}
