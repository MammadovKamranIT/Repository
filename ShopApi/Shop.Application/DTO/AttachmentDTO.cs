using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.DTO
{
    public class AttachmentResponseDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public string UploadedByUserId { get; set; } = string.Empty;
        public DateTimeOffset UploadedAt { get; set; }
    }

    public class OrderAttachmentInfo { 
        
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string StoredFileName { get; set; } = string.Empty;
        public string StorageKey { get; set; } = string.Empty;
        public string UploadedByUserId { get; set; } = string.Empty;






    }

}
