using System;

namespace Core.Entities
{
    public class Note
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string OwnerId { get; init; }

        public DateTime CreatedAt { get; init; }

        public string Content { get; init; }

        // for Dapper
        public Note() { }

        public Note(string ownerId, string content)
        {
            OwnerId = ownerId;
            CreatedAt = DateTime.Now;
            Content = content;
        }
    }
}
