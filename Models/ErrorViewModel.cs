using System.ComponentModel.DataAnnotations;

namespace yorokoanime.Models;

public class ErrorViewModel
{
    [Key]
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}