using System.ComponentModel.DataAnnotations;

namespace kojira.WebApi.Identity.Models.AccountViewModels;

public record LoginViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}


