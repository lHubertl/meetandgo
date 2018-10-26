using System;
using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Behaviours
{
    public class FrameHorizontalRoundingBehavior : Behavior<Frame>
    {
        protected override void OnAttachedTo(Frame bindable)
        {
            base.OnAttachedTo(bindable);

            // Perform setup
            bindable.SizeChanged += BindableOnSizeChanged;
        }

        protected override void OnDetachingFrom(Frame bindable)
        {
            base.OnDetachingFrom(bindable);

            // Perform clean up
            bindable.SizeChanged -= BindableOnSizeChanged;
        }

        // Behavior implementation

        private void BindableOnSizeChanged(object sender, EventArgs eventArgs)
        {
            if (!(sender is Frame control))
            {
                return;
            }

            control.CornerRadius = (float)(control.Height / 2);
        }

    }
}
