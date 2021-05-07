using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface INoteRepository
    {
        void Add(Note note);
        void Delete(int noteId);
        List<Note> GetAll();
        Note GetById(int id);
        void Update(Note note);
    }
}