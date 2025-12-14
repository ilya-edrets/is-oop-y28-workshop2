using System;

namespace Core.Entities
{
    public class Note
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string OwnerId { get; init; } = string.Empty;

        public DateTime CreatedAt { get; init; } = DateTime.Now;

        public string Content { get; init; } = string.Empty;

        // for Dapper
        public Note() { }

        public Note(string ownerId, string content)
        {
            OwnerId = ownerId;
            Content = content;
        }
    }
}
