using Microsoft.EntityFrameworkCore;
using RequestService.Domain.Models;
using RequestService.Domain.Repositories;
using RequestService.Infrastructure.Data;

namespace RequestService.Infrastructure.Repositories
{
    public class DocumentRequestRepository : IDocumentRequestRepository
    {
        private readonly AppDbContext _context;

        public DocumentRequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(DocumentRequest request, CancellationToken cancellationToken = default)
        {
            _ = await _context.AddAsync(request, cancellationToken);
        }

        public Task<DocumentRequest?> FindActiveDublicateAsync(string employeeName, DocumentType type, CancellationToken cancellationToken = default)
        {
            return _context.Requests.FirstOrDefaultAsync(r => r.EmployeeName == employeeName && r.DocumentType == type &&
                (r.Status == RequestStatus.Pending || r.Status == RequestStatus.InProgress), cancellationToken);
        }

        public async Task<IReadOnlyList<DocumentRequest>> GetByEmployeeAsync(string employeeName, CancellationToken cancellationToken = default)
        {
            return await _context.Requests.Where(r => r.EmployeeName == employeeName).OrderByDescending(r => r.CreatedAtUtc).ToListAsync();
        }

        public async Task<DocumentRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Requests.FindAsync(id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _ = await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
