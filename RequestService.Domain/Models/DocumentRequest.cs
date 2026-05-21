using System.ComponentModel.DataAnnotations;

namespace RequestService.Domain.Models
{
    public class DocumentRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(127)]
        public string EmployeeName { get; set; } = string.Empty;
        public DocumentType DocumentType { get; set; }
        [Range(1, 10)]
        public int Quantity { get; set; } = 1;
        [MaxLength(500)]
        public string? Reason { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }

    }
}
