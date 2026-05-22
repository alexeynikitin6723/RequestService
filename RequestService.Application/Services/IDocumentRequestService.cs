using RequestService.Application.Dtos;

namespace RequestService.Application.Services
{
    public interface IDocumentRequestService
    {
        Task<DocumentRequestDto> CreateRequestAsync(CreateDocumentRequestDto dto, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DocumentRequestDto>> GetRequestByEmployeeAsync(string employeeName, CancellationToken cancellationToken = default);
        Task<DocumentRequestDto> GetRequestByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DocumentRequestDto> UpdateStatusAsync(Guid id, UpdateStatusDto dto, CancellationToken cancellationToken = default);
    }
}
