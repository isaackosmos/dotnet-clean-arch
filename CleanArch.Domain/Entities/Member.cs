using System.Text.Json.Serialization;
using CleanArch.Domain.Validation;

namespace CleanArch.Domain.Entities;

public sealed class Member : Entity
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Gender { get; private set; }
    public string? Email { get; private set; }
    public bool? IsActive { get; private set; }

    public Member()
    {
    }

    public Member(string? firstName, string? lastName, string? gender, string? email, bool? isActive)
    {
        ValidateDomain(firstName, lastName, gender, email, isActive);
    }

    [JsonConstructor]
    public Member(int id, string? firstName, string? lastName, string? gender, string? email, bool? isActive)
    {
        DomainValidation.When(id < 0, "Invalid member id");
        Id = id;
        ValidateDomain(firstName, lastName, gender, email, isActive);
    }

    public void Update(string? firstName, string? lastName, string? gender, string? email, bool? isActive)
    {
        ValidateDomain(firstName, lastName, gender, email, isActive);
    }

    private void ValidateDomain(string? firstName, string? lastName, string? gender, string? email,
        bool? isActive = true)
    {
        DomainValidation.When(string.IsNullOrEmpty(firstName), "FirstName is required");
        DomainValidation.When(firstName != null && firstName.Length < 3, "FirstName is too short.");
        DomainValidation.When(string.IsNullOrEmpty(lastName), "LastName is required");
        DomainValidation.When(email?.Length > 150, "Email is too long.");
        DomainValidation.When(email?.Length < 5, "Email is too short.");
        DomainValidation.When(string.IsNullOrEmpty(gender), "Gender is required");
        DomainValidation.When(!isActive.HasValue, "Must define activity");

        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        IsActive = isActive;
    }
}