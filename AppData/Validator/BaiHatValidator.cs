using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using FluentValidation;

namespace AppData.Validator
{
    public class BaiHatValidator : AbstractValidator<BaiHat>
    {
        public BaiHatValidator()
        {
            RuleFor(b => b.TenBaiHat)
                .NotEmpty().WithMessage("Tên bài hát không được để trống.");

            RuleFor(b => b.NgheSi)
                .NotEmpty().WithMessage("Nghệ sĩ không được để trống.");

            RuleFor(b => b.Album)
                .NotEmpty().WithMessage("Album không được để trống.");

            RuleFor(b => b.TheLoai)
                .NotEmpty().WithMessage("Thể loại không được để trống.");

            RuleFor(b => b.ThoiGianPhatHanh)
                .NotEqual(TimeSpan.Zero).WithMessage("Thời gian phát không được để trống.");

            RuleFor(b => b.NgayPhatHanh)
                .NotEmpty().WithMessage("Ngày phát hành không được để trống.")
                .LessThan(DateTime.Now).WithMessage("Ngày phát hành phải là ngày trong quá khứ.");

            RuleFor(b => b.Status)
                .Must(StatusValidator).WithMessage("Trạng thái phải là 'Đang phát' hoặc 'Ngừng phát'.");
        }
        private bool StatusValidator(string trangThai)
        {
            return trangThai == "Đang phát" || trangThai == "Ngừng phát";
        }
    }
}
