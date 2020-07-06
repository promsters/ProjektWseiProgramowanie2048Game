using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace _2048Game.Animation
{
    public static class ScaleHelper
    {
        private static void AnimateScale(FrameworkElement target, double from, double to, double time = 200, bool reverse = false)
        {
            var scaleAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromMilliseconds(time),
                AutoReverse = reverse
            };

            ((target.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ApplyAnimationClock(ScaleTransform.ScaleXProperty, scaleAnimation.CreateClock());
            ((target.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ApplyAnimationClock(ScaleTransform.ScaleYProperty, scaleAnimation.CreateClock());
        }

        public static void ScaleIn(FrameworkElement target)
        {
            ((target.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ScaleX = 0.2;
            ((target.RenderTransform as TransformGroup).Children[0] as ScaleTransform).ScaleX = 0.2;

            AnimateScale(target, 0.2, 1);
        }

        public static void Pulse(FrameworkElement target)
        {
            AnimateScale(target, 1, 1.3, 100, true);
        }

    }
}
