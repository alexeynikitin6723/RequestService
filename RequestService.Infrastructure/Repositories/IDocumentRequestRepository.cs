using RequestService.Domain.Models;

namespace RequestService.Infrastructure.Repositories
{
    public interface IDocumentRequestRepository
    {
        Task AddAsync(DocumentRequest request, CancellationToken cancellationToken = default);
        Task<DocumentRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DocumentRequest?> FindActiveDublicateAsync(string employeeName, DocumentType type, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DocumentRequest>> GetByEmployeeAsync(string employeeName, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
