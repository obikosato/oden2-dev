using Common.Tools.WebSite;

namespace Test
{
    class StubEventInfoConverter : IEventInfoConverter
    {
        private readonly EventInfo eventInfo;

        public StubEventInfoConverter(EventInfo eventInfo)
        {
            this.eventInfo = eventInfo;
        }

        public EventInfo ConvertEventInfo()
        {
            return eventInfo;
        }
    }
}
