

namespace SMS.Model.DTOs
{
    public class UsersDto
    {
        public int ID { get; set; }
        public string? FullName { get; set; } = "";
        public string? MobileNo { get; set; }
        public string? Email { get; set; } = "";
        public string? UserID { get; set; } = "";
        public string? Address { get; set; } = "";
        public string? Password { get; set; } = "";
        public int IsAdmin { get; set; }
        public string? CreatedDate { get; set; } = "";
        public string? CreatedBy { get; set; } = "";
        public string? UserType { get; set; } = "";
    }
    
    
}

