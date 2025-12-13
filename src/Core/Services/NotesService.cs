using Core.Abstractions;
using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    internal class NotesService : INotesService
    {
        private INotesStorage _storage;
        private ILogger<NotesService> _logger;

        public NotesService(INotesStorage storage, ILogger<NotesService> logger)
        {
            _storage = storage;
            _logger = logger;
        }

        public async Task<OperationResult<Guid>> CreateNote(string ownerName, string content)
        {
            var note = new Note(ownerName, content);
            await _storage.AddNote(note);

            _logger.LogInformation("New note {noteId} was created", note.Id);

            return OperationResult<Guid>.Success(note.Id);
        }

        public async Task<OperationResult> DeleteNote(Guid noteId)
        {
            await _storage.DeleteNote(noteId);

            _logger.LogInformation("Note {noteId} was deleted", noteId);

            return OperationResult.Success();
        }

        public async Task<OperationResult<IReadOnlyCollection<Note>>> GetAllNotes(string ownerName)
        {
            var notes = await _storage.GetAllNotesByUserName(ownerName);
            return OperationResult<IReadOnlyCollection<Note>>.Success(notes);
        }
    }
}
