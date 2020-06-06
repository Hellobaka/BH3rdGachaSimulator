using me.cqp.luohuaming.Gacha.WPFUI;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using System;

namespace me.luohuaming.Gacha.Code.CustomPool
{
    public class CustomPoolForm_Call : IMenuCall
    {
        private MainWindow _mainWindow = null;
        public void MenuCall(object sender, CQMenuCallEventArgs e)
        {
            if (this._mainWindow == null)
            {
                this._mainWindow = new MainWindow();
                this._mainWindow.Closing += MainWindow_Closing;
                this._mainWindow.Show();	// 显示窗体
            }
            else
            {
                this._mainWindow.Activate();	// 将窗体调制到前台激活
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 对变量置 null, 因为被关闭的窗口无法重复显示
            this._mainWindow = null;
            GC.Collect();
        }
    }
}
