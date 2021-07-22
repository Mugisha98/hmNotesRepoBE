using System;
using System.Collections.Generic;
using System.Linq;
using Notes.DB;

namespace Notes.Core
{
    public class NotesServices : INotesServices
    {
        private AppDbContext _context;

        public NotesServices(AppDbContext context)
        {
            _context = context;
         }

        public Note CreateNote(Note note)
        {
            _context.Add(note);
            _context.SaveChanges();

            return note;
        }

        public void DeleteNote(int id)
        {
            var note = _context.Notes.First(n => n.Id == id);
            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

        public Note GetNote(int id)
        {
            return _context.Notes.First(n => n.Id == id);
        }

        public List<Note> GetNotes()
        {
            return _context.Notes.ToList();
        }

        public void UpdateNote(Note note)
        {
            var updatedNote = _context.Notes.First(n => n.Id == note.Id);
            updatedNote.Value = note.Value;
            _context.SaveChanges();
        }
    }
}
