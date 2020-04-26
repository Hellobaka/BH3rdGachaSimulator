## 崩坏3抽卡模拟图片版
基于Jie2GG的[Native.FrameWork](https://github.com/Jie2GG/Native.Framework)框架，依托于酷Q平台进行开发

## 引言
交流群:671467200<br>
受到我之前使用过的[插件](https://cqp.cc/t/43414)启发，想开发一个属于自己的抽卡模拟插件<br>
~~目前输出结果只是图片，所以仅限于[CQP](https://cqp.cc/t/14901)，免费的CQA由于无法发送图片而不能使用~~<br>
抽卡结果有图片和文字版，[CQP](https://cqp.cc/t/14901)与[CQA](https://cqp.cc/t/23253)都可使用<br>
根据官方公布的概率修改(下调至五分之一)之后能更符合实际游戏内的抽卡情况:unamused:<br>
如何上手使用请往下看<br>
## 功能实装情况
### 抽卡系统
- [x] 扩充补给
- [x] 精准补给
- [x] 标配补给
- [ ] 家园补给
- [x] 仓库系统(半实装，功能待定)
- [x] 可更改的抽卡概率
- [x] 签到与水晶限制机制
- [x] 文字版抽卡结果
### 指令控制
- [x] 支持群聊与私聊
- [x] 数据各群之间独立
- [x] 分群开启与管理员
- [x] 批量导入群与管理员
- [x] 自定义指令与回答文本
- [x] 通过接收消息实现开关
- [x] 允许执行sql语句
- [x] 后台群详细功能（功能制作中……)
- [x] 检查更新 
### 卡池更改
- [ ] 名称纠错
- [ ] 通过接收消息实现更改抽卡池更换
- [ ] 通过爬取崩坏3官网的公告来实现自动更换卡池
### 尚未想到的功能……

## 如何使用
1.下载插件[本体](https://cqp.cc/t/47221) 扩展名为cpk<br>
2.下载插件所必须的[数据包](https://lanzous.com/ibietkh) (2020.4.17)<br>
3.下载插件需要的[字体](https://www.lanzous.com/i9hl6ve) (2020.2.19)<br>
4.将cpk复制到酷Q的插件目录 ...\CQP-xiaoi\酷Q Pro\app下<br>
![说明图片1](https://i.loli.net/2020/03/21/QfVBumNkZ54j1bP.png)<br>
5.将数据包解压到插件的数据目录下 ...\CQP-xiaoi\酷Q Pro\data\app\me.cqp.luohuaming.Gacha 下<br>
![说明图片2](https://i.loli.net/2020/03/21/xeFt4rOMNIQpfbV.png)<br>
6.解压字体包，全选右键安装<br>
## data.db文件如何打开
百度下载SQLite Expert Personal，之后把文件拖进去即可<br>

## 项目如何使用
酷Q开启开发模式，参考[酷Q文库](https://docs.cqp.im/dev/v9/devmode/)的方法开启<br>
visual studio的最低版本为2019<br>
clone下载之后，设置Native.Core项目的生成目录为 ...\CQP-xiaoi\酷Q Pro\dev<br>
### 具体方法为:
>1.右击Native.Core项目，点击属性<br>
![说明图片3](https://i.loli.net/2020/03/21/PlNBCAHV1JWmLsO.png)<br>
2.左侧点击生成一栏，设置输出路径<br>
![说明图片4](https://i.loli.net/2020/03/21/mtCeRTWDHAh2Irg.png)<br>

点击生成-重新生成解决方案，之后酷Q重载应用即可
### 特别鸣谢
>\[SDK]Native.SDK --最贴近酷Q的C# SDK<br>
作者：Jie2GG<br>
交流群：@947295340<br>
GitHub：https://github.com/Jie2GG/Native.Framework<br>

感谢解包大佬提供的解包！没有解包图片就没有这个项目了
