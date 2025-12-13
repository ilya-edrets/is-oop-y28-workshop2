using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface INotesService
    {
        Task<OperationResult<Guid>> CreateNote(string ownerId, string text);

        Task<OperationResult<IReadOnlyCollection<Note>>> GetAllNotes(string ownerId);

        Task<OperationResult> DeleteNote(Guid noteId);
    }
}
