using GameDevPortal.Core.Entities;
using GameDevPortal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameDevPortal.Core.Specifications
{
    internal class CategoriesForProjectInSet : BaseSpecification<Category>
    {
        public CategoriesForProjectInSet(Guid projectId, HashSet<Guid> categoryIds) : base(c => (c.ProjectId == null || c.ProjectId == projectId) && categoryIds.Contains(c.Id))
        {
        }
    }
}
