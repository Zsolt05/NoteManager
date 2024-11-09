using System.ComponentModel.DataAnnotations;

namespace NoteManager.Shared
{
    public class NoteDto
    {
        /// <summary>
        /// A jegyzet címe
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public string Title { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// A jegyzet tartalma
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [Required(ErrorMessage = "A tartalom megadása kötelező.")]
        public string Content { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }

    public class NoteCreateDto : NoteDto
    {

    }

    public class NoteUpdateDto : NoteCreateDto
    {

    }

    public class NoteReadDto : NoteDto
    {
        /// <summary>
        /// A jegyzet azonosítója
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public string Id { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// A létrehozás dátuma
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// A felhasználó azonosítója
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public string UserId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    }
}
