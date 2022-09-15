using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdkNet.Models;

namespace SdkNet.Validators
{
    public class D3DSValidator : AbstractValidator<D3DSData>
    {
        public D3DSValidator() {
            RuleFor(d3dsData => d3dsData.email).MinimumLength(1).MaximumLength(100);
            RuleFor(d3dsData => d3dsData.phone).MinimumLength(1).MaximumLength(20);
            RuleFor(d3dsData => d3dsData.address).MinimumLength(1).MaximumLength(60);
            RuleFor(d3dsData => d3dsData.city).MinimumLength(1).MaximumLength(30);
            RuleFor(d3dsData => d3dsData.state).MinimumLength(2).MaximumLength(2);
            RuleFor(d3dsData => d3dsData.zipCode).MinimumLength(1).MaximumLength(10);
            RuleFor(d3dsData => d3dsData.countryCode).MinimumLength(3).MaximumLength(3);
        }

    }
}
