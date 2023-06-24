using DessertsKoma_Customers.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DessertsKoma_Customers.Service
{
    public class TopService
    {
        private readonly DessertsKomaContext _context;

        public TopService(DessertsKomaContext context)
        {
            _context = context;
        }

        public List<Десерты> GetTopДесерты()
        {
            var topDesserts = _context.Десерты
                .Include(d => d.ДесертыВзаказе)
                .OrderByDescending(d => d.ДесертыВзаказе.Count)
                .Take(4)
                .Select(g => g.Номер)
                .ToList();

            return _context.Десерты
                .Where(d => topDesserts.Contains(d.Номер))
                .Include(d => d.ТипNavigation)
                .Include(d => d.ИзображениеNavigation)
                .ToList();
        }
    }
}
