using System.ComponentModel.DataAnnotations;

namespace UserManagement.BusinessLogic.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
