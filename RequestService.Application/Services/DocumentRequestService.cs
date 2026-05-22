using RequestService.Application.Dtos;
using RequestService.Domain.Exceptions;
using RequestService.Domain.Models;
using RequestService.Domain.Repositories;

namespace RequestService.Application.Services
{
    public class DocumentRequestService : IDocumentRequestService
    {
        private readonly IDocumentRequestRepository _repository;
        public DocumentRequestService(IDocumentRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task<DocumentRequestDto> CreateRequestAsync(CreateDocumentRequestDto dto, CancellationToken cancellationToken = default)
        {
            DocumentRequest? existing = await _repository.FindActiveDublicateAsync(dto.EmployeeName, dto.DocumentType, cancellationToken);
            if (existing is not null)
            {
                throw new BusinessException("DUPLICATE_REQUEST", $"У вас уже есть активный запрос на справку '{dto.DocumentType}'");
            }
            DocumentRequest request = new()
            {
                EmployeeName = dto.EmployeeName,
                DocumentType = dto.DocumentType,
                Quantity = dto.Quantity,
                Reason = dto.Reason,
                Status = RequestStatus.Pending,
                CreatedAtUtc = DateTime.UtcNow
            };
            await _repository.AddAsync(request, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return MapToDto(request);
        }

        public async Task<IReadOnlyList<DocumentRequestDto>> GetRequestByEmployeeAsync(string employeeName, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<DocumentRequest> request = await _repository.GetByEmployeeAsync(employeeName, cancellationToken);
            return request.Select(r => MapToDto(r)).ToList();
        }

        public async Task<DocumentRequestDto> GetRequestByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            DocumentRequest? request = await _repository.GetByIdAsync(id, cancellationToken);
            return request is null ? throw new BusinessException("NOT_FOUND", $"Запрос: {id} не найден") : MapToDto(request);
        }

        public async Task<DocumentRequestDto> UpdateStatusAsync(Guid id, UpdateStatusDto dto, CancellationToken cancellationToken = default)
        {
            DocumentRequest? request = await _repository.GetByIdAsync(id, cancellationToken);
            if (request is null)
            {
                throw new BusinessException("NOT_FOUND", $"Запрос {id} не найден");
            }

            if (!IsValidTransition(request.Status, dto.NewStatus))
            {
                throw new BusinessException("INVALID_TRANSITION",
                    $"Невозможно изменить статус с '{request.Status}' на '{dto.NewStatus}'");
            }
            request.Status = dto.NewStatus;
            request.UpdatedAtUtc = DateTime.UtcNow;

            await _repository.SaveChangesAsync(cancellationToken);
            return MapToDto(request);
        }

        private static bool IsValidTransition(RequestStatus from, RequestStatus to)
        {
            return (from, to) switch
            {
                (RequestStatus.Pending, RequestStatus.InProgress) => true,
                (RequestStatus.Pending, RequestStatus.Rejected) => true,
                (RequestStatus.InProgress, RequestStatus.Ready) => true,
                (RequestStatus.InProgress, RequestStatus.Rejected) => true,
                (RequestStatus.Ready, RequestStatus.Rejected) => true,
                _ => false
            };
        }

        private static DocumentRequestDto MapToDto(DocumentRequest request)
        {
            return new DocumentRequestDto(
                request.Id,
                request.EmployeeName,
                request.DocumentType,
                request.Quantity,
                request.Reason,
                request.Status,
                request.CreatedAtUtc,
                request.UpdatedAtUtc
            );
        }
    }
}
