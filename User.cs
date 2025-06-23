using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email is requires.")]
        [EmailAddress(ErrorMessage = "The emails doesn't have a valid format")]
        public string Email { get; set; }
    }
}
