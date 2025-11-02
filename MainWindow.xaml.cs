using Microsoft.UI.Xaml;
using HyperZoneFRPGUILauncherforWindows.Pages; // 确保引入了 Pages 命名空间

namespace HyperZoneFRPGUILauncherforWindows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            this.Title = "HyperZoneFRP GUI Launcher for Windows"; // 设置窗口标题

            // ★★★ 启用 Mica 材质的关键代码 ★★★
            // 这会让整个窗口的背景都拥有通透效果，从而让页面中的 Acrylic/Mica 笔刷表现更佳。
            SystemBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();

            // 在窗口初始化后，立即让根 Frame 导航到我们的引导页 (此逻辑保持不变)
            RootFrame.Navigate(typeof(WelcomePage));
        }
    }
}
