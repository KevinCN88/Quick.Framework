﻿using Autofac;
using LocalStorageDemo.ViewModel;
using Quick;
using System;
using System.Windows;

/*工程创建说明：
 * (1)新建一个.net Framework工程，版本大于等于4.6.1
 * (2)Nuget搜索QuickFramework.Wpf，安装即可添加全部引用
 */

/*引用库说明及帮助文档：
 Quick.Core 框架核心，基于.net Standard 2.0
 Quick.Wpf  框架WPF核心，基于.net Framework 4.6.1
 Quick.HandyControl UI基础支持，帮助地址：https://handyorg.github.io/handycontrol/quick_start/
 Newtonsoft.Json  基础json转换支持，帮助地址：https://www.newtonsoft.com/json/help/html/Introduction.htm
 Autofac  依赖注入基础支持，帮助地址：https://autofac.readthedocs.io/en/latest/getting-started/index.html
 AutoMapper 对象映射支持, 帮助地址：https://docs.automapper.org/en/stable/index.html
 Serilog.Sinks.File  文件日志支持，帮助地址：https://github.com/serilog/serilog/wiki/Getting-Started
 Dapper 数据库访问类库，帮助地址：https://github.com/DapperLib/Dapper    https://dapper-tutorial.net/
 DevExpress.Mvvm  -- MVVM绑定增强，帮助地址：https://docs.devexpress.com/WPF/115771/mvvm-framework/dxbinding/dxbinding
 PropertyChanged.Fody -- 该包简化了通知属性的编写, 帮助地址：https://github.com/Fody/PropertyChanged/wiki/Attributes
 */

/*Demo说明：
  演示LocalStorage的用法，十分方便的随时随地持久化数据
 */
namespace LocalStorageDemo
{
    /// <summary>
    /// 1.将基类改成QWpfApplication，同样的在Xaml中也要修改基类
    /// 2.在Xaml中将StartupUri="MainWindow.xaml"删除
    /// </summary>
    public partial class App : QWpfApplication
    {
        protected override void ConfigureStartup(QApplicationCreationOptions options)
        {
            //默认情况下，LocalStorage将存储到程序启动目录下localstorage.json文件里，如果需要将localStorage合并到配置文件中
            //使用以下设置清除默认的文件名，同样的，也可以通过以下设置修改存储的文件名
            //options.Configuration.LocalStorageFileName = null;

            //如果需要修改配置文件和LocalStorage的存储目录，使用下面的语句进行设置
            //options.Configuration.BasePath = "";
        }

        //由于框架采用模块化设计，必须设置启动模块
        public override Type StartupModuleType => typeof(AppModule);

        //为了从容器中获取窗口类，需要重写该方法来弹出窗口，需要在Xaml中将StartupUri="MainWindow.xaml"删除
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //从IOC容器中获取Mainwindow
            MainWindow mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }


    /// <summary>
    /// 应用程序主模块，用于注入该容器的服务
    /// </summary>
    [DependsOn(typeof(QWpfModule))]
    public class AppModule : QModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //注入窗口类
            context.ServiceBuilder.RegisterType<MainWindow>().SingleInstance();

            //注入主窗口ViewModel
            context.ServiceBuilder.RegisterType<MainWindowViewModel>().SingleInstance();
        }
    }
}
