using AppData;
using AppData.Entities;
using AppData.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiHatController : ControllerBase
    {
        private readonly IBaiHatRepo _repo;
        private readonly IValidator<BaiHat> _validator;
        private readonly AppDbContext _context;

        public BaiHatController(IBaiHatRepo repo, IValidator<BaiHat> validator, AppDbContext context)
        {
            _repo = repo;
            _validator = validator;
            _context = context;
        }

        [HttpGet("GetByTheLoaiAndTrangThai")]
        public IActionResult GetByTheLoaiAndTrangThai(string theLoai, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(theLoai) || string.IsNullOrWhiteSpace(trangThai))
            {
                return BadRequest("Thể loại và trạng thái không được để trống.");
            }
            var baiHats = _context.BaiHats
                .Where(b => b.TheLoai == theLoai && b.Status == trangThai)
                .ToList();

            if (!baiHats.Any())
            {
                return NotFound("Không có bài hát nào phù hợp với tiêu chí tìm kiếm.");
            }

            return Ok(baiHats);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baiHat = await _repo.GetAllAsync();
            return Ok(baiHat);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var baiHat = await _repo.GetByIdAsync(id);
            if (baiHat == null) return NotFound();
            return Ok(baiHat);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BaiHat baiHat)
        {
            var validatorResult = await _validator.ValidateAsync(baiHat);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }
                ));
            }
            await _repo.AddAsync(baiHat);
            return Ok(baiHat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BaiHat baiHat)
        {
            if(id != baiHat.Id)
            {
                return BadRequest("Id Không trùng khớp");
            }
            var validatorResult = await _validator.ValidateAsync(baiHat);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }
                ));
            }
            await _repo.UpdateAsync(baiHat);
            return Ok(baiHat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);
            return NoContent();
        }

    }
}
