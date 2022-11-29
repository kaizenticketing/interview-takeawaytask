#nullable enable

namespace Ticketing.Services.Customers.Models;

[MessagePackObject]
public class Login
{
    [Key(0)]
    public Uuid? Id { get; set; }

    [Key(1)]
    public Uuid ProfileId { get; set; }

    [Key(2)]
    public LoginType Type { get; set; }

    [Key(3)]
    // [PersonalInformation]
    public string? Username { get; set; }

    /// <summary>
    /// NOTE: needs to be converted to a hash before being persisted
    /// </summary>
    [Key(4)]
    // [PersonalInformation]
    public string? NewPassword { get; set; }

    [Key(5)]
    // [PersonalInformation]
    public string? PasswordHash { get; set; }
}
