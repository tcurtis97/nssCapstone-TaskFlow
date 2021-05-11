using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface IWorkDayRepository
    {
        void AddWorkDay(WorkDay workDay);
        void DeleteWorkDay(int workDayId);
        void UpdateWorkDay(WorkDay workDay);
        void WorkDayExist(WorkDay workDay);
    }
}