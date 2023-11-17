using DTOLayer.DTOs.ContactDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.ContactUsValidatorRules
{
    public class ContactUsValidator : AbstractValidator<SendMessageDTO>
    {
        public ContactUsValidator()
        {
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!!");
            RuleFor(x => x.MessageBody).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!!");
            RuleFor(x => x.MessageBody).MinimumLength(20).WithMessage("Bu Alan En Az 20 Karakter İçermelidir!!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!!");
            RuleFor(x => x.Subject).MaximumLength(30).WithMessage("Bu Alan En Fazla 30 Karakter İçermelidir!!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu Alan Boş Geçilemez!!");
        }
    }
}
