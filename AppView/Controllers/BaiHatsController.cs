using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Entities;
using System.Text;
using System.Text.Json;

namespace AppView.Controllers
{
    public class BaiHatsController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _apiBaseUrl = "https://localhost:7124/BaiHats";

        public BaiHatsController()
        {
            _client = new();
        }

        // GET: BaiHats
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var baiHatsJson = await response.Content.ReadAsStringAsync();
                var baiHatList = JsonSerializer.Deserialize<List<BaiHat>>(baiHatsJson);
                return View(baiHatList);
            }
            ModelState.AddModelError(string.Empty, "Không thể tải danh sách Bài Hát.");
            return View(new List<BaiHat>());
        }

        // GET: BaiHats/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var baiHatJson = await response.Content.ReadAsStringAsync();
                var baiHatDetail = JsonSerializer.Deserialize<BaiHat>(baiHatJson);
                return View(baiHatDetail);
            }

            return NotFound();
        }

        // GET: BaiHats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BaiHats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgheSi,TenBaiHat,Album,TheLoai,ThoiGianPhat,NgayPhatHanh,TrangThai")] BaiHat baiHat)
        {
            if (ModelState.IsValid)
            {
                baiHat.Id = Guid.NewGuid();
                var jsonContent = JsonSerializer.Serialize(baiHat);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(_apiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không thể tạo mới Bài Hát.");
            }

            return View(baiHat);
        }

        // GET: BaiHats/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var baiHatJson = await response.Content.ReadAsStringAsync();
                var baiHatDetail = JsonSerializer.Deserialize<BaiHat>(baiHatJson);
                return View(baiHatDetail);
            }

            return NotFound();
        }

        // POST: BaiHats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NgheSi,TenBaiHat,Album,TheLoai,ThoiGianPhat,NgayPhatHanh,TrangThai")] BaiHat baiHat)
        {
            if (id != baiHat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var jsonContent = JsonSerializer.Serialize(baiHat);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"{_apiBaseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không thể cập nhật Bài Hát.");
            }

            return View(baiHat);
        }

        // GET: BaiHats/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var baiHatJson = await response.Content.ReadAsStringAsync();
                var baiHatDetail = JsonSerializer.Deserialize<BaiHat>(baiHatJson);
                return View(baiHatDetail);
            }

            return NotFound();
        }

        // POST: BaiHats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _client.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Không thể xóa Bài Hát.");
            return View();
        }

        private bool BaiHatExists(Guid id)
        {
            var response = _client.GetAsync($"{_apiBaseUrl}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
