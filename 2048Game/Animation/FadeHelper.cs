using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace _2048Game.Animation
{
    public static class FadeHelper
    {
        private static void AnimateOpacity(FrameworkElement target, double from, double to)
        {
            var opacityAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(1000)
            };

            opacityAnimation.Completed += (s, e) => {
                if (to == 0)
                    target.Visibility = Visibility.Hidden;
            };

            Storyboard.SetTarget(opacityAnimation, target);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(FrameworkElement.OpacityProperty));

            var storyboard = new Storyboard();
            storyboard.Children.Add(opacityAnimation);
            storyboard.Begin();
        }

        public static void FadeIn(FrameworkElement target)
        {
            target.Opacity = 0;
            target.Visibility = Visibility.Visible;
            AnimateOpacity(target, 0, 1);
        }

        public static void FadeOut(FrameworkElement target)
        {
            AnimateOpacity(target, 1, 0);
        }
    }
}
