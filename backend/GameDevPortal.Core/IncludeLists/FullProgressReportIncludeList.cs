using GameDevPortal.Core.Entities;

namespace GameDevPortal.Core.IncludeLists;

public class FullProgressReportIncludeList : BaseIncludeList<ProgressReport>
{
    public FullProgressReportIncludeList()
    {
        AddInclude(pr => pr.Categories);
    }
}