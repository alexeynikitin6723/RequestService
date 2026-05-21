using RequestService.Domain.Models;

namespace RequestService.Application.Dtos
{
    public record DocumentRequestDto(
        Guid Id,
        string EmployeeName,
        DocumentType DocumentType,
        int Quantity,
        string? Reason,
        RequestStatus Status,
        DateTime CreatedAtUtc,
        DateTime? UpdatedAtUtc
    );
}
