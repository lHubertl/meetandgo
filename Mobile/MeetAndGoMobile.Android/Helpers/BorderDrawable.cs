using Android.Graphics;

namespace MeetAndGoMobile.Droid.Helpers
{
    internal class BorderDrawable : Android.Graphics.Drawables.Drawable
    {
        private readonly Paint _paint;
        private Rect _boundsRect;

        public BorderDrawable(Color color, int width)
        {
            _paint = new Paint
            {
                Color = color,
                StrokeWidth = width
            };

            _paint.SetStyle(Paint.Style.Stroke);
        }

        public override int Opacity => 0;
        protected override void OnBoundsChange(Rect bounds)
        {
            base.OnBoundsChange(bounds);
            _boundsRect = bounds;
        }

        public override void Draw(Canvas canvas)
        {
            canvas.DrawRect(_boundsRect, _paint);
        }

        public override void SetAlpha(int alpha)
        {
            //throw new NotImplementedException();
        }

        public override void SetColorFilter(ColorFilter colorFilter)
        {
            //throw new NotImplementedException();
        }
    }
}