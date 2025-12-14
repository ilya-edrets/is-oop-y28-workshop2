using Core.Abstractions;
using Core.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Postgres
{
    internal class NotesStorage : INotesStorage
    {
        private DapperContext _dapperContext;

        public NotesStorage(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddNote(Note note)
        {
            var query = """
                INSERT INTO notes (id, owner_id, created_at, content)
                VALUES (@Id, @OwnerId, @CreatedAt, @Content);
                """;

            // Используем именованные параметры для защиты от SQL Injection
            var parameters = new DynamicParameters();
            parameters.Add("Id", note.Id);
            parameters.Add("OwnerId", note.OwnerId);
            parameters.Add("CreatedAt", note.CreatedAt);
            parameters.Add("Content", note.Content);

            // Соединение с БД это unmanaged ресурс, поэтому using обязателен
            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteNote(Guid noteId)
        {
            var query = """
                DELETE FROM notes
                WHERE id = @Id;
                """;

            var parameters = new DynamicParameters();
            parameters.Add("Id", noteId);

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<IReadOnlyCollection<Note>> GetAllNotesByUserName(string userId)
        {
            var query = """
                SELECT id, owner_id, created_at, content
                FROM notes
                WHERE owner_id = @OwnerId
                """;

            var parameters = new DynamicParameters();
            parameters.Add("OwnerId", userId);

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QueryAsync<Note>(query, parameters);

            return result.AsList();
        }
    }
}
