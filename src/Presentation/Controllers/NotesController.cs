using Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    // Контроллеры асп.нета позволяют реализовывать паттерн MVC
    // Атрибут Authorize требует,что бы пользователи были залогигены прежде чем смогут вызывать методы этого контроллера
    [Authorize()]
    [Route("notes")]
    public class NotesController : Controller
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteRequest request)
        {
            var result = await _notesService.CreateNote(User.Identity!.Name!, request.Content);

            if (result.IsSuccess)
            {
                return base.Created();
            }
            else
            {
                return base.BadRequest(result.Error);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var result = await _notesService.GetAllNotes(User.Identity!.Name!);

            if (result.IsSuccess && result.Result != null)
            {
                return base.Ok(result.Result);
            }

            return base.BadRequest(result.Error);
        }

        // "{id:guid}" - говорит о том, что параметр Id это гуид и он явялется частью урла для этого метода
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute]Guid id)
        {
            var result = await _notesService.DeleteNote(id);

            if (result.IsSuccess)
            {
                return base.NoContent();
            }

            return base.BadRequest(result.Error);
        }
    }
}
