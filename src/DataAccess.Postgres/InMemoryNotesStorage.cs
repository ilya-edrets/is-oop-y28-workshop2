using Core.Abstractions;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Postgres
{
    internal class InMemoryNotesStorage : INotesStorage
    {
        private readonly Dictionary<Guid, Note> _storage = new Dictionary<Guid, Note>();

        public Task AddNote(Note note)
        {
            _storage[note.Id] = note;
            return Task.CompletedTask;
        }

        public Task DeleteNote(Guid noteId)
        {
            if (_storage.ContainsKey(noteId))
            {
                _storage.Remove(noteId);
            }

            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Note>> GetAllNotesByUserName(string ownerId)
        {
            var notes = _storage
                .Where(x => x.Value.OwnerId == ownerId)
                .Select(x => x.Value)
                .ToList() as IReadOnlyCollection<Note>;

            return Task.FromResult(notes);
        }
    }
}
