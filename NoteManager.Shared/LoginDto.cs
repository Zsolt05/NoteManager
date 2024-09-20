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
        public required string UserName { get; set; }

        /// <summary>
        /// A jelszó.
        /// </summary>
        public required string Password { get; set; }
    }
}
