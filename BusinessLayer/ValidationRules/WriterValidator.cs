using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş bırakılamaz");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Yazar adı en az 2 karakter uzunluğunda olabilir");
            RuleFor(x => x.WriterName).MaximumLength(20).WithMessage("Yazar adı en fazla 20 karakter uzunluğunda olabilir");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar soyadı boş bırakılamaz");
            RuleFor(x => x.WriterSurName).MinimumLength(2).WithMessage("Kategori adı en az 2 karakter uzunluğunda olabilir");
            RuleFor(x => x.WriterSurName).MaximumLength(20).WithMessage("Kategori adı en fazla 20 karakter uzunluğunda olabilir");
        }

        
    }
}
