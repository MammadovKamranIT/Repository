namespace Shop.DTO.Customer_DTOs
{
    public class AppMemberResponseDto
    {

        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset JoinedAt { get; set; }

    }
}
