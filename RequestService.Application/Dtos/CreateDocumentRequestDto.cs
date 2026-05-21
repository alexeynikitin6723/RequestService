using RequestService.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RequestService.Application.Dtos
{
    public record CreateDocumentRequestDto
    {
        [Required, MaxLength(100)]
        public string EmployeeName { get; init; } = string.Empty;

        public DocumentType DocumentType { get; init; }

        [Range(1, 10)]
        public int Quantity { get; init; } = 1;

        [MaxLength(500)]
        public string? Reason { get; init; }
    }
}
