using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdkNet.Models;

namespace SdkNet.Validators
{
    public class BusinessDataValidator: AbstractValidator<BusinessData>
    {
        public BusinessDataValidator() {
            RuleFor(businessData => businessData.idBranch).NotNull().MinimumLength(1).MaximumLength(11);
            RuleFor(businessData => businessData.idCompany).NotNull().MinimumLength(4).MaximumLength(4);
            RuleFor(businessData => businessData.user).NotNull().MinimumLength(9).MaximumLength(11);
            RuleFor(businessData => businessData.user).NotNull().MinimumLength(1).MaximumLength(80);
        }
    }
}
