using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.Core;
using Notes.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
       
        private readonly ILogger<NotesController> _logger;
        private INotesServices _notesServices;

        public NotesController(ILogger<NotesController> logger, INotesServices notesServices)
        {
            _logger = logger;
            _notesServices = notesServices;
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(_notesServices.GetNotes());
        }

        //Get note by id
        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetNote(int id)
        {
            try
            {
                return Ok(_notesServices.GetNote(id));
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);
                return Ok("Error: " + "Couldn't find the id number: " + id);
            }
        }

        //Creates a note and return it
        [HttpPost]
        public IActionResult CreateNote(Note note)
        {
            var newNote = _notesServices.CreateNote(note);
            return CreatedAtRoute("GetNote", new { newNote.Id }, newNote); //Returns the newly created note
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            _notesServices.DeleteNote(id);
            return Ok("Deletion of note with id "+ id +" is succesful");
        }

        [HttpPut]
        public IActionResult UpdateNote([FromBody] Note note) {
            _notesServices.UpdateNote(note);
            return Ok("Your changes: " + note.Value);
        }
    }
}
