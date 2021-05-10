using System;
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

        Job GetJobByIdWithDetails(int id);

        List<Job> GetAllJobsByCustomerId(int id);

        void ComepleteJob(int JobId, DateTime complete);

        List<Job> GetAllUncompleteJobs();
    }
}