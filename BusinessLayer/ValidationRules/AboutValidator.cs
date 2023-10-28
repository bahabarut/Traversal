using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.Image1).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Bu Alan 10 Karakterden Fazla Olmalıdır!");
        }
    }
}
