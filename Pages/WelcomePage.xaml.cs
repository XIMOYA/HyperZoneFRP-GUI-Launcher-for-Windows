using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HyperZoneFRPGUILauncherforWindows.Pages
{
    public sealed partial class WelcomePage : Page
    {
        // 存储背景图片路径的列表
        private readonly List<BitmapImage> _backgroundImages = new List<BitmapImage>();
        private int _currentBgIndex = 1; // 1 代表 BackgroundImageView1, 2 代表 BackgroundImageView2

        public WelcomePage()
        {
            this.InitializeComponent();
            LoadBackgroundImages();
            this.Loaded += WelcomePage_Loaded;
        }

        // 预加载所有背景图片
        private void LoadBackgroundImages()
        {
            for (int i = 0; i < 4; i++)
            {
                _backgroundImages.Add(new BitmapImage(new Uri($"ms-appx:///Assets/WelcomeBg_{i}.png")));
            }
        }

        // 页面加载完成后，设置第一张背景图
        private void WelcomePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (_backgroundImages.Count > 0)
            {
                BackgroundImageView1.Source = _backgroundImages[0];
            }
        }

        // 当 FlipView 翻页时触发
        private async void FeaturesFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipView = sender as FlipView;
            if (flipView == null || _backgroundImages.Count <= flipView.SelectedIndex) return;

            int newIndex = flipView.SelectedIndex;

            // 执行背景切换动画
            SwitchBackgroundWithFade(newIndex);

            // 检查是否是最后一页
            if (newIndex == _backgroundImages.Count - 1)
            {
                // 是最后一页，延迟2秒后显示按钮
                await Task.Delay(2000);
                // 再次确认用户没有切走
                if (FeaturesFlipView.SelectedIndex == newIndex)
                {
                    AnimateButtonIn();
                }
            }
            else
            {
                // 不是最后一页，隐藏按钮
                StartButton.Opacity = 0;
            }
        }

        // 背景淡入淡出切换的核心方法
        private void SwitchBackgroundWithFade(int newImageIndex)
        {
            Image fadeInTarget, fadeOutTarget;

            // 判断当前哪个 Image 在前面，哪个在后面
            if (_currentBgIndex == 1)
            {
                fadeInTarget = BackgroundImageView2;
                fadeOutTarget = BackgroundImageView1;
                _currentBgIndex = 2;
            }
            else
            {
                fadeInTarget = BackgroundImageView1;
                fadeOutTarget = BackgroundImageView2;
                _currentBgIndex = 1;
            }

            // 为即将显示的 Image 设置新的图片源
            fadeInTarget.Source = _backgroundImages[newImageIndex];

            // 创建淡入和淡出动画
            var fadeInAnimation = new DoubleAnimation { To = 1.0, Duration = new Duration(TimeSpan.FromSeconds(0.5)) };
            var fadeOutAnimation = new DoubleAnimation { To = 0.0, Duration = new Duration(TimeSpan.FromSeconds(0.5)) };

            // 启动动画
            Storyboard.SetTarget(fadeInAnimation, fadeInTarget);
            Storyboard.SetTargetProperty(fadeInAnimation, "Opacity");
            Storyboard.SetTarget(fadeOutAnimation, fadeOutTarget);
            Storyboard.SetTargetProperty(fadeOutAnimation, "Opacity");

            var sb = new Storyboard();
            sb.Children.Add(fadeInAnimation);
            sb.Children.Add(fadeOutAnimation);
            sb.Begin();
        }

        // “开始使用”按钮的动画
        private void AnimateButtonIn()
        {
            // 创建一个从下往上移动并淡入的动画
            var translateAnimation = new DoubleAnimation { From = 20, To = 0, Duration = new Duration(TimeSpan.FromSeconds(0.4)), EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut } };
            var opacityAnimation = new DoubleAnimation { From = 0, To = 1, Duration = new Duration(TimeSpan.FromSeconds(0.4)) };

            // 需要给按钮设置一个 TranslateTransform
            StartButton.RenderTransform = new TranslateTransform();

            Storyboard.SetTarget(translateAnimation, StartButton.RenderTransform);
            Storyboard.SetTargetProperty(translateAnimation, "Y");
            Storyboard.SetTarget(opacityAnimation, StartButton);
            Storyboard.SetTargetProperty(opacityAnimation, "Opacity");

            var sb = new Storyboard();
            sb.Children.Add(translateAnimation);
            sb.Children.Add(opacityAnimation);
            sb.Begin();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(LoginPage));
            }
        }
    }
}
