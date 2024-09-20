namespace NoteManager.Shared
{
    /// <summary>
    /// Az authentikációs válasz DTO.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// A JWT token.
        /// </summary>
        public required string Token { get; set; }

        /// <summary>
        /// A felhasználó neve.
        /// </summary>
        public required string UserName { get; set; }
    }
}
