using System.ComponentModel.DataAnnotations;

namespace NoteManager.Shared
{
    /// <summary>
    /// A bejelentkezési DTO.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// A felhasználónév.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [Required(ErrorMessage = "A felhasználónév megadása kötelező.")]
        public string UserName { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// A jelszó.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [Required(ErrorMessage = "A jelszó megadása kötelező.")]
        public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
