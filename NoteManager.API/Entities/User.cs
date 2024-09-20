using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.API.Entities
{
    /// <summary>
    /// A felhasználó entitás.
    /// </summary>
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Az azonosító.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required string Id { get; set; }

        /// <summary>
        /// A felhasználó neve.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// A felhasználó jelszó hash-e.
        /// </summary>
        public required byte[] PasswordHash { get; set; }

        /// <summary>
        /// A felhasználó jelszó sója.
        /// </summary>
        public required byte[] PasswordSalt { get; set; }

        /// <summary>
        /// A felhasználó jegyzetei.
        /// </summary>
        public required List<Note> Notes { get; set; } = [];
    }

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordSalt).IsRequired();
        }
    }
}
