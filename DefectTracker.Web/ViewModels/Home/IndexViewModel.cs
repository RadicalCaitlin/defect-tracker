using DefectTracker.Core;
using System.Collections.Generic;

namespace DefectTracker.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Projects> Projects { get; set; }
    }
}
