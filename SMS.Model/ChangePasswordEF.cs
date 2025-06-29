using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain
{
    public class ChangePasswordEF
    {
        public int? UserId { get; set; }
        public int? UserLoginId { get; set; }
        public string OldPassword { get; set; }
        public string EncryptPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string EncryptOldPassword { get; set; }
        public string EncryptNewPassword { get; set; }
        public string EncryptConfirmPassword { get; set; }
        public string randomNumber { get; set; }
    }
}
