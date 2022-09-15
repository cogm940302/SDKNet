using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdkNet.Models;

namespace SdkNet.Validators
{
    internal class UrlValidator : AbstractValidator<UrlData>
    {
        public UrlValidator()
        {
            RuleFor(urlData => urlData.reference).NotNull().MinimumLength(1).MaximumLength(100);
            RuleFor(urlData => urlData.amount).GreaterThan(0);
            RuleFor(urlData => urlData.moneda).NotNull();
            RuleFor(urlData => urlData.omitNotification).GreaterThanOrEqualTo(0);
            RuleFor(urlData => urlData.promotions).MinimumLength(1).MaximumLength(40);
            RuleFor(urlData => urlData.stEmail).GreaterThanOrEqualTo(0);
        }

    }
}
