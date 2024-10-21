using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public interface IBaiHatRepo
    {
        Task<IEnumerable<BaiHat>> GetAllAsync();
        Task<BaiHat> GetByIdAsync(Guid id);
        Task AddAsync(BaiHat baiHat);
        Task UpdateAsync(BaiHat baiHat);
        Task Delete(Guid id);
    }
}
