using System.Collections.Generic;

namespace WebScheduler.BLL.Events.Queries.GetEventList
{
    public class EventListVm
    {
        public IList<EventLookupDto> Events { get; set; }
    }
}
