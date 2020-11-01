using System.Collections.Generic;

namespace Garage3.Models.ViewModels
{
    public class MemberViewModel
    // Added by Stefan for the search function
    {
        public IEnumerable<Member> MemberList { get; set; }
        public string SearchString { get; set; }
    }
}
