using API.Utilities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required]
        [PasswordPolicy]
        public string Password { get; set; }
        public int Otp { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
