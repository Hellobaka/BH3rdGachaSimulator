﻿using Unity;
using Native.Sdk.Cqp.Interface;
using me.luohuaming.Gacha.Code;
using me.luohuaming.Gacha.UI;
using me.luohuaming.Gacha.Code.CustomPool;

namespace Native.Core
{
    /// <summary>
    /// 酷Q应用主入口类
    /// </summary>
    public class CQMain
	{
		/// <summary>
		/// 在应用被加载时将调用此方法进行事件注册, 请在此方法里向 <see cref="IUnityContainer"/> 容器中注册需要使用的事件
		/// </summary>
		/// <param name="unityContainer">用于注册的 IOC 容器 </param>
		public static void Register (IUnityContainer unityContainer)
		{
			unityContainer.RegisterType<IGroupMessage, Event_GroupMessage>("群消息处理");
			unityContainer.RegisterType<IPrivateMessage, Event_PrivateMessage>("私聊消息处理");
			unityContainer.RegisterType<IMenuCall, Event_MenuCall>("控制窗口");
			unityContainer.RegisterType<IMenuCall, CustomPoolForm_Call>("自定义池子设置");

			unityContainer.RegisterType<ICQStartup, Event_StartUp>("酷Q启动事件");
		}
	}
}
