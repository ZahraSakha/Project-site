using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhaSite.Models.ViewModels.Validators
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(i => i.CurrentPassword)
                .NotNull().WithMessage("رمز عبور فعلی نمی تواند خالی باشد")
                .NotEmpty().WithMessage("رمز عبور فعلی نمی تواند خالی باشد")
                .MinimumLength(3).WithMessage("طول رمز عبور فعلی باید بیشتر از 3 حرف باشد");

            RuleFor(i => i.NewPassword)
                .NotNull().WithMessage("رمز عبور جدید نمی تواند خالی باشد")
                .NotEmpty().WithMessage("رمز عبور جدید نمی تواند خالی باشد")
                .MinimumLength(3).WithMessage("طول رمز عبور جدید باید بیشتر از 3 حرف باشد");

            RuleFor(i => i.ConfirmNewPassword)
                .NotNull().WithMessage("تکرار رمز عبور جدید نمی تواند خالی باشد")
                .NotEmpty().WithMessage("تکرار رمز عبور جدید نمی تواند خالی باشد")
                .MinimumLength(3).WithMessage("طول تکرار رمز عبور جدید باید بیشتر از 3 حرف باشد")
                .Must((m, cnp) => cnp == m.NewPassword).WithMessage("رمز عبور جدید با تکرار آن مطابقت ندارد");
        }
    }
}
