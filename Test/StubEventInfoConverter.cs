using Common.Services;
using Common.Tools.WebSite;

namespace Test
{
    class StubEventInfoConverter : IEventInfoConverter
    {
        private readonly string msg;

        public StubEventInfoConverter(EventInfo eventInfo)
        {
            msg = Messages.AM01(eventInfo.title, eventInfo.artist);
            msg += Messages.URL;
        }

        public EventInfo ConvertEventInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
