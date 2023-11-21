using System.Windows;
using System.Windows.Controls;

namespace MUSICMAN.Components
{
    /// <summary>
    /// Логика взаимодействия для Rating.xaml
    /// </summary>
    public partial class Rating : UserControl
    {
        public Rating()
        {
            InitializeComponent();
        }

        public Style RatingStyle
        {
            get { return (Style)GetValue(RatingStyleProperty); }
            set { SetValue(RatingStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RatingStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatingStyleProperty =
            DependencyProperty.Register("RatingStyle", typeof(Style), typeof(Rating),
                new PropertyMetadata(default(Style)));

        public int CurrentRating
        {
            get { return (int)GetValue(CurrentRatingProperty); }
            set { SetValue(CurrentRatingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentRating.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentRatingProperty =
            DependencyProperty.Register("CurrentRating", typeof(int), typeof(Rating),
                new FrameworkPropertyMetadata(default(int), CurrentRatingPropertChanged));

        public int MaxRating
        {
            get { return (int)GetValue(MaxRatingProperty); }
            set { SetValue(MaxRatingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxRating.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxRatingProperty =
            DependencyProperty.Register("MaxRating", typeof(int), typeof(Rating),
                new FrameworkPropertyMetadata(100, MaxRatingPropertChanged));

        private static void CurrentRatingPropertChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (source is Rating rating)
            {
                //rating.CurrentRating = (int)e.NewValue + 1;
            }
        }

        private static void MaxRatingPropertChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (source is Rating rating)
            {
                //rating.MaxRating = (int)e.NewValue;
            }
        }
    }
}