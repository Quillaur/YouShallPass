using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace You_Shall_Pass.Controls
{
    public sealed class PageHeader : Control
    {
        public PageHeader()
        {
            DefaultStyleKey = typeof(PageHeader);
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value.ToUpper()); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title),
            typeof(string), typeof(PageHeader), new PropertyMetadata(null));

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(nameof(Glyph),
            typeof(string), typeof(PageHeader), new PropertyMetadata(null));
    }
}
