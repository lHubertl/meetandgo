using System;

namespace MeetAndGoMobile.Services
{
    public interface IMasterPageService
    {
        void ToggleMaster(ToggleActions toggleAction);

        void Subscribe(Action<ToggleActions> listener);
        void Unsubscribe(Action<ToggleActions> listener);
    }

    public enum ToggleActions
    {
        ToggleMenu,
        UserUpdated
    }
}
