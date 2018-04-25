using FluentValidation.Results;

namespace StACS.OpenAudit.Core.Interfaces
{
    public interface IValidate
    {
        ValidationResult Validate();
    }
}