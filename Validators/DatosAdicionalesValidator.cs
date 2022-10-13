using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdkNet.Models;

namespace SdkNet.Validators
{
    public class DatosAdicionalesValidator : AbstractValidator<DatosAdicionalesData.DataItem>
    {
        public DatosAdicionalesValidator()
        {
            RuleFor(dataItem => dataItem.id).GreaterThan(-1);
            RuleFor(dataItem => dataItem.label).NotNull().MinimumLength(1).MaximumLength(60);
            RuleFor(dataItem => dataItem.value).NotNull().MinimumLength(1).MaximumLength(100);

        }

    }
}
