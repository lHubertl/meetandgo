using System;

namespace MeetAndGoMobile.Services
{
    public interface IMasterPageService
    {
        void ToggleMaster();
        void SubscribeToggleListener(Action listener);
        void UnsubscribeToggleListener(Action listener);
    }
}
