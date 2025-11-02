// Pages/LoginPage.xaml.cs
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks; // 引入异步编程所需的命名空间

namespace HyperZoneFRPGUILauncherforWindows.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        // 登录按钮的点击事件处理程序
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // --- 1. 输入验证 ---
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                ShowLoginError("请输入您的账号。");
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Password))
            {
                ShowLoginError("请输入您的密码。");
                return;
            }

            // --- 2. 进入加载状态 ---
            SetLoadingState(true);

            // --- 3. 模拟网络请求 ---
            // 在这里，我们将调用后端的登录 API
            // 目前，我们先用一个延迟来模拟
            await Task.Delay(2000); // 模拟2秒的网络延迟

            // --- 4. 处理登录结果 (模拟) ---
            bool loginSuccess = UsernameTextBox.Text == "admin" && PasswordTextBox.Password == "123456"; // 模拟一个正确的账号密码

            if (loginSuccess)
            {
                // 登录成功，可以导航到主界面
                // TODO: 实现导航到主应用界面的逻辑
                var dialog = new ContentDialog
                {
                    Title = "登录成功",
                    Content = "欢迎回来！即将进入主界面。",
                    CloseButtonText = "好的",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
            else
            {
                // 登录失败，显示错误信息
                ShowLoginError("账号或密码错误，请重试。");
            }

            // --- 5. 退出加载状态 ---
            SetLoadingState(false);
        }

        // 一个辅助方法，用于控制界面的加载状态
        private void SetLoadingState(bool isLoading)
        {
            if (isLoading)
            {
                LoginButton.IsEnabled = false; // 禁用按钮防止重复点击
                LoginButtonText.Visibility = Visibility.Collapsed; // 隐藏 "登录" 文字
                LoginProgressRing.Visibility = Visibility.Visible; // 显示加载环
                LoginProgressRing.IsActive = true;
            }
            else
            {
                LoginButton.IsEnabled = true;
                LoginButtonText.Visibility = Visibility.Visible;
                LoginProgressRing.Visibility = Visibility.Collapsed;
                LoginProgressRing.IsActive = false;
            }
        }

        // 一个辅助方法，用于在 InfoBar 中显示错误信息
        private void ShowLoginError(string message)
        {
            LoginErrorBar.Title = "登录失败";
            LoginErrorBar.Message = message;
            LoginErrorBar.IsOpen = true;
        }
    }
}
