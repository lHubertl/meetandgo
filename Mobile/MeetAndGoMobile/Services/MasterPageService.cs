using System;

namespace MeetAndGoMobile.Services
{
    internal class MasterPageService : IMasterPageService
    {
        private event Action ToggleListener;
        public void ToggleMaster()
        {
            ToggleListener?.Invoke();
        }

        public void SubscribeToggleListener(Action listener)
        {
            ToggleListener += listener;
        }

        public void UnsubscribeToggleListener(Action listener)
        {
            ToggleListener -= listener;
        }
    }
}
