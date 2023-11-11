using DTOLayer.DTOs.AppUserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AppUserRegisterValidator : AbstractValidator<AppUserRegisterDTO>
    {

        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.ConfirmPassword).Equal(y=>y.Password).WithMessage("Şifreler Uyuşmuyor!");
        }
    }
}
