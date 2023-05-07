using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Models;
using System.Linq.Expressions;

namespace GameDevPortal.Core.Specifications
{
    internal class FullProgressReportSpecification : BaseSpecification<ProgressReport>
    {
        public FullProgressReportSpecification(Pagination pagination) : base(pagination)
        {
            AddInclude(pr => pr.Categories);
        }

        public FullProgressReportSpecification(Expression<Func<ProgressReport, bool>> criteria, Pagination pagination) : base(criteria, pagination)
        {
            AddInclude(pr => pr.Categories);
        }
    }
}