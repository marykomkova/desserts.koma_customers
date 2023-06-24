using DessertsKoma_Customers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertsKoma_Customers.Service
{
    public class DessertsTypesService
    {
        private readonly DessertsKomaContext _context;

        public DessertsTypesService(DessertsKomaContext context)
        {
            _context = context;
        }

        public List<ТипыДесертов> GetТипыДесертов()
        {
            return _context.ТипыДесертов
                .Include(d => d.ИзображениеNavigation)
                .Include(d => d.ЕдиницаИзмеренияNavigation)
                .ToList();
        }

        public List<int> GetТипыДесертовCount()
        {
            var counts = new List<int>();

            foreach (var type in GetТипыДесертов())
            {
                counts.Add(
                _context.Десерты
                .Include(d => d.ТипNavigation)
                .Where(x => x.ТипNavigation.Номер == type.Номер)
                .Count());
            }
            return counts;
        }

        public int GetAllДесертыCount()
        {
            return _context.Десерты
                .Count();
        }

        public double GetНаимСтоимость()
        {
            return _context.Десерты
                .Min(x => x.Стоимость).Value;
        }

        public double GetНаибСтоимость()
        {
            return _context.Десерты
                .Max(x => x.Стоимость).Value;
        }

        public List<Десерты> GetAllДесерты()
        {
            return _context.Десерты
                .Include(x => x.ТипNavigation)
                .Include(x => x.ИзображениеNavigation)
                .ToList();
        }
    }
}
