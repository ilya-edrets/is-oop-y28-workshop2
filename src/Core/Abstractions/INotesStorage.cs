using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface INotesStorage
    {
        Task AddNote(Note note);

        Task<IReadOnlyCollection<Note>> GetAllNotesByUserName(string userId);

        Task DeleteNote(Guid noteId);
    }
}
