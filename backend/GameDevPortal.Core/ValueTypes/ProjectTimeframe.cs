using GameDevPortal.Core.Models;

namespace GameDevPortal.Core.ValueTypes;

public class ProjectTimeFrame
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public ProjectStatus Status { get; private set; }

    public ProjectTimeFrame(DateTime startDate, DateTime endDate, ProjectStatus status)
    {
        if (startDate >= endDate) throw new ArgumentException("Project start date must be before the end date.", nameof(startDate));
        StartDate = startDate;
        EndDate = endDate;

        if (status == ProjectStatus.Finished && endDate > DateTime.UtcNow) throw new ArgumentException("Project status can not be finished if the end date of the project has not passed.", nameof(status));
        if (status > ProjectStatus.Preparation && startDate > DateTime.UtcNow) throw new ArgumentException("Projects which have entered production must have a start date before the current date.", nameof(status));
        Status = status;
    }
}