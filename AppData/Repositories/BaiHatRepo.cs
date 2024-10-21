using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public class BaiHatRepo : IBaiHatRepo
    {
        private readonly AppDbContext _context;

        public BaiHatRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(BaiHat baiHat)
        {
            await _context.BaiHats.AddAsync(baiHat);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var baiHat = await _context.BaiHats.FindAsync(id);
            if(baiHat != null && baiHat.Status != "Đang phát")
            {
                _context.BaiHats.Remove(baiHat);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<IEnumerable<BaiHat>> GetAllAsync()
        {
            return await _context.BaiHats.ToListAsync();
        }

        public async Task<BaiHat> GetByIdAsync(Guid id)
        {
            return await _context.BaiHats.FindAsync(id);
        }

        public async Task UpdateAsync(BaiHat baiHat)
        {
            _context.BaiHats.Update(baiHat);
            await _context.SaveChangesAsync();
        }
    }
}
