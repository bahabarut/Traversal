using DTOLayer.DTOs.AnnouncementDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.AnnouncementValidatorRules
{
    public class AnnouncementUpdateValidator : AbstractValidator<AnnouncementUpdateDTO>
    {
        public AnnouncementUpdateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan boş geçilemez!");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Bu alan boş geçilemez!");
            RuleFor(x => x.Content).MinimumLength(5).WithMessage("Bu alan 5 karakterden fazla olmalı!");
        }
    }
}
