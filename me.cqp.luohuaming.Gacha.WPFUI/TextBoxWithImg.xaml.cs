using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace me.cqp.luohuaming.Gacha.WPFUI
{
    /// <summary>
    /// TextBoxWithImg.xaml 的交互逻辑
    /// </summary>
    public partial class TextBoxWithImg : UserControl
    {
        public TextBoxWithImg()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                img.Source= new BitmapImage(new Uri(text.Text));
            }
            catch
            {
                img.Source = null;
            }
        }

        private void img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 在WPF中， OpenFileDialog位于Microsoft.Win32名称空间
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "所有文件|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(dialog.FileName)); 
                text.Text=dialog.FileName;
            }
        }
    }
}
