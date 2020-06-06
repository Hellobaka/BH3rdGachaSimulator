using me.luohuaming.Gacha.Code.CustomPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace me.cqp.luohuaming.Gacha.WPFUI
{
    public static class Helper
    {
        /// <summary>
        /// 通过值反查字典键
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static IEnumerable<String> GetKeyByChineseName(string value)
        {
            return Translation.NameTranslation.Where(x => x.Value.ChineseName == value).Select(x => x.Key);
        }

        /// <summary>
        /// 通过循环次数来查找父元素
        /// </summary>
        /// <param name="element">子元素</param>
        /// <param name="count">循环次数</param>
        /// <returns></returns>
        public static UIElement GetParent(UIElement element,int count)
        {
            UIElement uIElement=element;
            for(int i = 0; i < count; i++)
            {
                //uIElement = (UIElement)VisualTreeHelper.GetParent(uIElement);
                uIElement = (UIElement)((FrameworkElement)uIElement).Parent;
            }
            return uIElement;
        }

        /// <summary>
        /// 获取需要作为检索字典键值的TextBlock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="StackPanel_Main"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static UIElement GetTextBlock(object sender,StackPanel StackPanel_Main, UIElementCollection collection)
        {
            //ComboBox情况单一,兄弟元素就是所需的TextBlock
            if(((object)sender).GetType().Name=="ComboBox")
                return (TextBlock)VisualTreeHelper.GetChild(StackPanel_Main, collection.IndexOf((ComboBox)sender) - 1);//IndexOf-1就是兄弟元素

            UIElement child;
            TextBox textBox = (TextBox)sender;
            if(textBox.Parent as StackPanel != null)//是普通的TextBox,寻找兄弟元素即可
            {
                child = textBox;
            }
            else//是自定义控件的TextBox,层级如下 TextBox->Grid->TextBoxWithImg->StackPanel
            {
                child = GetParent(sender as UIElement, 2);//所以只需要循环两次就可以得到作为子元素的自定义控件
            }
            return (TextBlock)VisualTreeHelper.GetChild(StackPanel_Main, collection.IndexOf(child) - 1);
        }

        /// <summary>
        /// 修改ListBox所选项目值,不更换位置
        /// </summary>
        /// <param name="listBox">需要更改的ListBox</param>
        /// <param name="str">修改后的值</param>
        public static void ChangeItemText(ListBox listBox,string str)
        {
            int index = listBox.SelectedIndex;
            listBox.Items.Remove(listBox.SelectedItem);
            listBox.Items.Insert(index,str);
            listBox.SelectedIndex = index;
        }

        /// <summary>
        /// 动态添加控件到StackPanel_Main
        /// </summary>
        /// <param name="obj">PoolContent或者PoolInfo类对象,用于取出内部成员以生成视图</param>
        /// <param name="item">PropertyInfo对象,主要是获取属性名</param>
        /// <param name="window"></param>
        public static void AddElement2Panel(object obj, PropertyInfo item,MainWindow window)
        {
            string value;
            try
            {
                //从obj中获取名为item.Name的属性的值,例如 obj 有个属性Name="宫子" ,传递过来的item.Name="Name",就可以得到value="宫子"
                value=obj.GetType().GetProperty(item.Name).GetValue(obj).ToString();
            }
            catch { value = null; }
            
            //减少打字数量
            Infos infos = Translation.NameTranslation[item.Name];

            //由于控件是存在静态字典里的,窗口关闭再打开时,静态字典内的控件相关属性都不会清除
            //这时就会出现这些控件已经存在了一个逻辑父控件(关掉的那个窗口的StackPanel_Main)
            //再尝试添加时就会报错
            //所以需要判断,如果将要添加的控件的逻辑父控件不是空,则清除这个逻辑父控件内的所有子元素(我也不清楚,为什么直接给这个Parent赋值是无效的
            if ((infos.UIElementType as Control).Parent != null)
            {
                //window.StackPanel_Main.Children.Remove(infos.UIElementType);
                ((infos.UIElementType as Control).Parent as StackPanel).Children.Clear();
            }
                //(infos.UIElementType as Control).Parent.SetValue(ContentPresenter.ContentProperty, null); //无效的方法

            //待加入的控件名是TextBox
            if (infos.UIElementType.GetType().Name == "TextBox")
            {
                TextBox textBox = (TextBox)infos.UIElementType;
                //这些控件在切换ListBox项目时,只是被更换了内容,委托并没有被清除
                //没有这个-=就会出现程序越来越慢的情况
                textBox.TextChanged -= window.TextBox_TextChanged;
                textBox.TextChanged += window.TextBox_TextChanged;

                try
                {
                    if (string.IsNullOrEmpty(value))
                        value = (string)infos.DefaultValue;
                    textBox.Text = value;

                    window.StackPanel_Main.Children.Add(textBox);
                }
                catch
                {
                    textBox.Text = (string)infos.DefaultValue;
                    window.StackPanel_Main.Children.Add(textBox);

                }
            }
            else if (infos.UIElementType.GetType().Name == "ComboBox")
            {
                ComboBox comboBox = (ComboBox)infos.UIElementType;
                comboBox.SelectionChanged -= window.ComboBox_SelectionChanged;
                comboBox.SelectionChanged += window.ComboBox_SelectionChanged;
                comboBox.Items.Clear();
                foreach (var enumcontent in infos.EnmuContent)
                {
                    comboBox.Items.Add(enumcontent);
                }
                try
                {
                    if (string.IsNullOrEmpty(value))
                        value = (string)infos.DefaultValue;
                    comboBox.SelectedItem = comboBox.Items[comboBox.Items.IndexOf(value)];
                    window.StackPanel_Main.Children.Add(comboBox);
                }
                catch
                {
                    window.StackPanel_Main.Children.Add(comboBox);
                }
            }
            else
            {
                TextBoxWithImg img = (TextBoxWithImg)infos.UIElementType;
                img.text.TextChanged -= window.TextBox_TextChanged;
                img.text.TextChanged += window.TextBox_TextChanged;
                try
                {
                    if (string.IsNullOrEmpty(value))
                        value = (string)infos.DefaultValue;
                    img.text.Text = value;
                    window.StackPanel_Main.Children.Add(img);
                }
                catch
                {
                    img.text.Text = (string)infos.DefaultValue;
                    window.StackPanel_Main.Children.Add(img);
                }
            }
        }

        /// <summary>
        /// 生成所有成员是默认值的PoolContent或者PoolInfo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetTempleteItem(object obj)
        {
            Dictionary<string, Infos> dic = Translation.NameTranslation;
            foreach(var item in obj.GetType().GetProperties())
            {
                PropertyInfo property = obj.GetType().GetProperty(item.Name);
                //为obj的属性赋值,使用ChangeType来适应类型
                property.SetValue(obj, Convert.ChangeType(dic[item.Name].DefaultValue, property.PropertyType));
            }
            return obj;
        }

        /// <summary>
        /// 根据TextBox内容动态更新静态成员CustomPool的属性
        /// </summary>
        /// <param name="sender">TextChange事件的sender</param>
        /// <param name="window"></param>
        public static void ChangeProperty(object sender,MainWindow window)
        {
            UIElementCollection collection = window.StackPanel_Main.Children;
            TextBlock textblock = (TextBlock)Helper.GetTextBlock(sender, window.StackPanel_Main, collection);

            PoolInfo info = window.GetPoolInfo(window.List_PoolDisplay.SelectedItem);
            PoolContent content = window.GetPoolContent(window.List_PoolContentDisplay.SelectedItem, info);

            string keyname = Helper.GetKeyByChineseName(textblock.Text).FirstOrDefault();
            //??运算符:左侧为null时,将右侧的值赋给等号左边
            var property = info.GetType().GetProperty(keyname) ?? content.GetType().GetProperty(keyname);

            //?这句的作用似乎和上面那句重复了
            //用于获取值是来自List_PoolContentDisplay还是List_PoolDisplay
            object obj = (info.GetType().GetProperties().Contains(property)) ? (object)info : content;
            if (sender.GetType().Name == "ComboBox")
            {
                ComboBox comboBox = (ComboBox)sender;
                //将变化的值赋值给obj对应的属性
                property.SetValue(obj, Convert.ChangeType(comboBox.SelectedItem.ToString(), property.PropertyType));
            }
            else if (sender.GetType().Name == "TextBox")
            {
                TextBox textBox = sender as TextBox;
                property.SetValue(obj, Convert.ChangeType(textBox.Text, property.PropertyType));

                //如果变化的是PoolName或者是Name,还需要同步更新左侧ListBox的内容
                string key = GetKeyByChineseName(textblock.Text).FirstOrDefault();
                if (key == "PoolName" || key == "Name")
                {
                    if (obj.Equals(info))
                        ChangeItemText(window.List_PoolDisplay, textBox.Text);
                    else
                        ChangeItemText(window.List_PoolContentDisplay, textBox.Text);
                }
            }
        }
    }
}
