using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace HyperZoneFRPGUILauncherforWindows
{
    public partial class App : Application
    {
        private Window? m_window; 

        public App()
        {
            this.InitializeComponent();
        }

        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {

            var splashScreen = new SplashScreenWindow();
            splashScreen.DispatcherQueue.TryEnqueue(() =>
            {
                splashScreen.Activate();
            });

            await Task.Delay(3000); // 让闪屏显示 3 秒

            // 创建主窗口
            m_window = new MainWindow();

            m_window.DispatcherQueue.TryEnqueue(() =>
            {
                m_window.Activate();    // 激活主窗口，使其可见
                splashScreen.Close();   // 关闭闪屏窗口
            });
        }
    }
}
