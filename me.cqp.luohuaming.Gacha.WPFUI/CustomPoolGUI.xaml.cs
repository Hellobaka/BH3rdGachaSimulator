using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using me.luohuaming.Gacha.Code.CustomPool;
using me.luohuaming.Gacha.UI;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace me.cqp.luohuaming.Gacha.WPFUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private static CustomPool cp;//存储所有数据的变量
        static bool loaded = false;//标记控件是否加载完成
        static bool saved = true;//标记是否已经保存
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cp = GetCustomPool();
            foreach (var item in cp.Infos)
            {
                List_PoolDisplay.Items.Add(item.PoolName);
            }
            if (List_PoolDisplay.Items.Count != 0)//默认选中最后一项
                List_PoolDisplay.SelectedItem = List_PoolDisplay.Items[List_PoolDisplay.Items.Count - 1];
        }
        /// <summary>
        /// 反序列化文件,将结果存入cp变量
        /// </summary>
        /// <returns></returns>
        public CustomPool GetCustomPool()
        {
            string path = $@"{CQSave.AppDirectory}CustomPool\pool.json";
            if (!File.Exists(path))
                return new CustomPool();
            return JsonConvert.DeserializeObject<CustomPool>(File.ReadAllText(path));
        }

        /// <summary>
        /// 通过传入的Item,获取序列数,从cp.infos数组中取出相应的PoolInfo
        /// </summary>
        /// <param name="item">ListBoxItem</param>
        /// <returns></returns>
        public PoolInfo GetPoolInfo(object item)
        {
            return cp.Infos[List_PoolDisplay.Items.IndexOf(item)];
        }

        /// <summary>
        /// 从传入的info中,根据序列数返回PoolContent
        /// </summary>
        /// <param name="item">获取的ListBoxItem</param>
        /// <param name="info"></param>
        /// <returns></returns>
        public PoolContent GetPoolContent(object item, PoolInfo info)
        {
            if (info == null ||item==null) return null;
            return info.PoolContents[List_PoolContentDisplay.Items.IndexOf(item)];
        }

        private void List_PoolDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tempselectionchanged = e;
            List_PoolContentDisplay.Items.Clear();
            var listbox = (ListBox)sender;
            if (listbox.Items.Count == 0 || listbox.SelectedIndex==-1) return;
            PoolInfo poolinfo = GetPoolInfo(listbox.SelectedItem);

            GenerateEditView(poolinfo);
            if (listbox.SelectedIndex == -1) return;
            if (poolinfo != null && poolinfo.PoolContents!=null)
                foreach (var item in poolinfo.PoolContents)
                {
                    List_PoolContentDisplay.Items.Add(item.Name);
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取按钮的父控件
            var parent = VisualTreeHelper.GetParent((sender as Button).Parent);
            //ListBox位于父控件的第0位
            var listbox = (ListBox)VisualTreeHelper.GetChild(parent, 0);
            switch ((sender as Button).Content)
            {
                case "添加":
                    //向选中项的下一位添加
                    listbox.Items.Insert(listbox.SelectedIndex+1, "新元素...");
                    //向全局变量cp中添加新元素
                    AddNewElement(listbox);
                    listbox.SelectedItem = listbox.Items[listbox.SelectedIndex + 1];
                    break;
                case "删除":
                    int index = listbox.SelectedIndex;
                    RemoveElement(listbox);
                    if (listbox.SelectedItems.Count != 0)
                        listbox.Items.RemoveAt(index);
                    if (listbox.Items.Count != 0)
                    {
                        listbox.SelectedIndex = (index + 1 > listbox.Items.Count) ? listbox.Items.Count - 1 : index;
                    }
                    break;
                case "清空":
                    if(MessageBox.Show("这样将会失去"+(listbox.Name.Contains("Content")?"这个池子的":"")+"所有数据，确定继续？", "提示", MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        ClearElement(listbox);
                        listbox.Items.Clear();
                    }
                    break;
            }
        }

        /// <summary>
        /// 从全局cp中删除指定的元素
        /// </summary>
        /// <param name="listbox"></param>
        private void RemoveElement(ListBox listbox)
        {
            if (!listbox.Name.Contains("Content"))//是上面的ListBox
            {
                cp.Infos.Remove(GetPoolInfo(listbox.SelectedItem));
            }
            else
            {
                var poolinfo = GetPoolInfo(List_PoolDisplay.SelectedItem);
                poolinfo.PoolContents.Remove(GetPoolContent(List_PoolContentDisplay.SelectedItem,poolinfo));
            }
        }

        /// <summary>
        /// 向全局cp中添加新元素
        /// </summary>
        /// <param name="listbox"></param>
        private void AddNewElement(ListBox listbox)
        {
            if (!listbox.Name.Contains("Content"))//是上面的ListBox
            {
                if(listbox.SelectedItem==null)//无被选中的,添加模板
                    cp.Infos.Add((PoolInfo)Helper.GetTempleteItem(new PoolInfo()));
                else
                {
                    //有被选中的,方便起见,复制当前选中的项目
                    PoolInfo info =GetPoolInfo(List_PoolDisplay.SelectedItem).Clone();
                    info.PoolName = "新元素...";info.PoolContents = new List<PoolContent>();
                    cp.Infos.Insert(List_PoolDisplay.SelectedIndex+1,info);
                }
            }
            else//List_PoolContentDisplay
            {   
                if(cp.Infos.Count==0)//想在下面的listbox添加,却发现上面的listbox是空的
                {
                    cp.Infos.Add((PoolInfo)Helper.GetTempleteItem(new PoolInfo()));//先添加模板
                    List_PoolDisplay.Items.Add("新元素...");
                    List_PoolDisplay.SelectedIndex = 0;
                }
                if (listbox.SelectedItem == null)//is null ,need to get a templete item
                    GetPoolInfo(List_PoolDisplay.SelectedItem).PoolContents.Add((PoolContent)Helper.GetTempleteItem(new PoolContent()));
                else//not null,clone the selected item
                {
                    var info = GetPoolInfo(List_PoolDisplay.SelectedItem);
                    PoolContent content = GetPoolContent(List_PoolContentDisplay.SelectedItem,info).Clone();
                    content.Name = "新元素...";
                    info.PoolContents.Insert(List_PoolContentDisplay.SelectedIndex + 1,content);
                }
            }
        }

        /// <summary>
        /// 清空全局cp中的指定数组
        /// </summary>
        /// <param name="listbox"></param>
        private void ClearElement(ListBox listbox)
        {
            if (!listbox.Name.Contains("Content"))
            {
                cp.Infos.Clear();
            }
            else
            {
                GetPoolInfo(List_PoolDisplay.SelectedItem).PoolContents.Clear();
            }
        }

        /// <summary>
        /// 动态生成右侧编辑窗口
        /// </summary>
        /// <param name="obj">PoolContent或者PoolInfo类成员,用于取出内部成员以生成视图</param>
        private void GenerateEditView(object obj)
        {
            //标记未加载完成,防止触发TextChange事件
            loaded = false;
            StackPanel_Main.Children.Clear();
            if (obj == null) return;
            //通过GetType()获取对象内部公有属性
            foreach (var item in obj.GetType().GetProperties())
            {
                //是字典定义中不生成的元素.跳过
                if (!Translation.NameTranslation[item.Name].visible)
                    continue;
                //提示文本TextBlock
                TextBlock textBlock = new TextBlock()
                {
                    Margin = new Thickness(2, 2, 2, 2),
                    //通过属性名作为字典的Key,获取字典中的中文名称
                    Text = Translation.NameTranslation[item.Name].ChineseName
                };
                StackPanel_Main.Children.Add(textBlock);
                //将对应的控件添加进StackPanel_Main
                Helper.AddElement2Panel(obj, item,this);
            }
            loaded = true;
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).Parent == null || !loaded) return;
            saved = false;
            try
            {
                Helper.ChangeProperty(sender, this);
            }
            catch { }
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Parent == null ||!loaded) return;
            saved = false;
            try
            {
                Helper.ChangeProperty(sender, this);
            }
            catch { }            
        }

        static SelectionChangedEventArgs tempselectionchanged;
        private void List_PoolContentDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_PoolDisplay.SelectedItem == null || List_PoolContentDisplay.SelectedItem == null) return;
            var pc = GetPoolContent(List_PoolContentDisplay.SelectedItem, GetPoolInfo(List_PoolDisplay.SelectedItem));
            GenerateEditView(pc);
        }

        private void List_PoolDisplay_GotFocus(object sender, RoutedEventArgs e)
        {
            if (List_PoolDisplay.SelectedIndex != -1)
            {
                List_PoolDisplay_SelectionChanged(sender, tempselectionchanged);
            }
        }

        private void List_PoolContentDisplay_GotFocus(object sender, RoutedEventArgs e)
        {
            if (List_PoolDisplay.SelectedIndex != -1)
            {
                List_PoolContentDisplay_SelectionChanged(sender, tempselectionchanged);
            }
        }

        private void menuLoad_Click(object sender, RoutedEventArgs e)
        {
            if (!saved)
                if (MessageBox.Show("尚未保存，是否重新载入？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
            saved = true;
            //打开文件选择面板
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Json文件|*.json"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    cp = JsonConvert.DeserializeObject<CustomPool>(File.ReadAllText(dialog.FileName));
                    List_PoolDisplay.Items.Clear();
                    List_PoolContentDisplay.Items.Clear();
                    StackPanel_Main.Children.Clear();

                    foreach (var item in cp.Infos)
                    {
                        List_PoolDisplay.Items.Add(item.PoolName);
                    }
                    if (List_PoolDisplay.Items.Count != 0)
                        List_PoolDisplay.SelectedItem = List_PoolDisplay.Items[List_PoolDisplay.Items.Count - 1];
                    MessageBox.Show("载入成功", "提示");
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"Json解析失败,错误信息:{exc.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Directory.Exists(CQSave.AppDirectory + "CustomPool"))
                    Directory.CreateDirectory(CQSave.AppDirectory + "CustomPool");
                File.WriteAllText($@"{CQSave.AppDirectory}\CustomPool\pool.json", JsonConvert.SerializeObject(cp));
                saved = true;
            }
            catch(Exception exc)
            {
                MessageBox.Show($"保存失败,错误信息{exc.Message}", "错误");
                return;
            }
            MessageBox.Show("保存成功");
        }

        private void menuQuit_Click(object sender, RoutedEventArgs e)
        {
            if (!saved)
                if (MessageBox.Show("尚未保存，是否退出？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
            this.Close();
        }
    }
}
