using System;
using MeetAndGoMobile.Services.Contracts;

namespace MeetAndGoMobile.Services
{
    internal class MasterPageService : IMasterPageService
    {
        private event Action<ToggleActions> ToggleListener;
        
        public void ToggleMaster(ToggleActions toggleAction)
        {
            ToggleListener?.Invoke(toggleAction);
        }

        public void Subscribe(Action<ToggleActions> listener)
        {
            ToggleListener += listener;
        }

        public void Unsubscribe(Action<ToggleActions> listener)
        {
            ToggleListener -= listener;
        }
    }
}
