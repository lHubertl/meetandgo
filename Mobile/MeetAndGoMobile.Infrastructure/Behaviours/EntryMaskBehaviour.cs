using System.Collections.Generic;
using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Behaviours
{
    public class EntryMaskBehaviour : Behavior<Entry>
    {
        private IDictionary<int, char> _positions;
        private const char AnySymbolChar = '_';

        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }

        public string MatchWithMask(string text)
        {
            if (text is null || _positions is null)
            {
                return string.Empty;
            }

            text = text.Replace(" ", "");

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                    {
                        text = text.Insert(position.Key, value);
                    }
                }
            if (text.Length > Mask.Length)
            {
                var count = text.Length - Mask.Length;
                text = text.Remove(Mask.Length, count);
            }
            return text;

        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var dict = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != AnySymbolChar)
                    dict.Add(i, Mask[i]);

            _positions = dict;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!(sender is Entry control))
            {
                return;
            }

            control.Text = MatchWithMask(control.Text);
        }
    }
}
