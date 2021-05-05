using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface IJobRepository
    {
        void Add(Job job);
        void Delete(int jobId);
        List<Job> GetAll();
        Job GetById(int id);
        void Update(Job job);
    }
}