using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects;

public sealed record Email
{    
    public string Value { get; } = null!;

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessRuleException($"El {nameof(email)} es obligatorio");
        }

        if (!email.Contains("@"))
        {
            throw new BusinessRuleException($"El {nameof(email)} no es válido");
        }

        Value = email;
    }
}
