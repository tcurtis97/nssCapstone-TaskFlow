using System.Collections.Generic;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface IWorkRecordRepository
    {
        void Add(WorkRecord workRecord);
        void Delete(int workRecordId);
        List<WorkRecord> GetAll();
        List<WorkRecord> GetAllWorkRecordsByJobId(int id);
        WorkRecord GetById(int id);
        void Update(WorkRecord workRecord);
    }
}