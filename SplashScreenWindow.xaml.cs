using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using Windows.Graphics;
using WinRT.Interop;

namespace HyperZoneFRPGUILauncherforWindows
{
    public sealed partial class SplashScreenWindow : Window
    {
        private AppWindow m_appWindow;

        public SplashScreenWindow()
        {
            this.InitializeComponent();

            m_appWindow = GetAppWindowForCurrentWindow();

            if (m_appWindow.Presenter is OverlappedPresenter overlappedPresenter)
            {
                overlappedPresenter.SetBorderAndTitleBar(false, false); // 移除边框和标题栏
                overlappedPresenter.IsMaximizable = false; // 不允许最大化
                overlappedPresenter.IsMinimizable = false; // 不允许最小化
                overlappedPresenter.IsResizable = false;   // 不允许调整大小
            }

            const int windowWidth = 400;
            const int windowHeight = 320;

            DisplayArea displayArea = DisplayArea.GetFromWindowId(m_appWindow.Id, DisplayAreaFallback.Nearest);
            int screenWidth = displayArea.WorkArea.Width;
            int screenHeight = displayArea.WorkArea.Height;

            int x = (screenWidth - windowWidth) / 2;
            int y = (screenHeight - windowHeight) / 2;

            m_appWindow.MoveAndResize(new RectInt32(x, y, windowWidth, windowHeight));

            this.Title = "HyperZoneFRP GUI Launcher for Windows 启动中";
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }
    }
}
