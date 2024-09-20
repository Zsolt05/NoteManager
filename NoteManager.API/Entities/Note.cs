using NoteManager.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.API.Entities
{
    [Table("Notes")]
    public class Note
    {
        /// <summary>
        /// Az azonosító.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required string Id { get; set; }

        /// <summary>
        /// A jegyzet címe.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// A jegyzet tartalma.
        /// </summary>
        public required string Content { get; set; }

        /// <summary>
        /// A létrehozás dátuma.
        /// </summary>
        public required DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// A felhasználó azonosítója.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// A felhasználó.
        /// </summary>
        public required User User { get; set; }
    }

    public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId);
            builder.Property(n => n.Title).IsRequired();
            builder.Property(n => n.Content).IsRequired();
        }
    }
}
