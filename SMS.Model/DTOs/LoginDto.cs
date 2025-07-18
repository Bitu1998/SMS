using System.ComponentModel.DataAnnotations;

namespace SMS.Model.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? USERNAME { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? UPASSWORD { get; set; }
        [Required(ErrorMessage = "Otp is required.")]
        public string? OTP { get; set; } 
        public string? captcha { get; set; } 
        [Required(ErrorMessage = "Mobile is required.")]
        public string? MOBILENO { get; set; }
    }
    
}

