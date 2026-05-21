using RequestService.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace RequestService.Application.Dtos
{
    public record UpdateStatusDto
    {
        [Required]
        public RequestStatus NewStatus { get; init; }

        [MaxLength(200)]
        public string? Comment { get; init; }
    }
}
