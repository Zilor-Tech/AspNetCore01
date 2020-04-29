using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp2
{
    public static class Sample01
    {
        public interface IAccount{ }
        public interface IMessage{ }
        public interface ITool{ }

        public class Base
        {
            public Base()
            {
                Console.WriteLine($"Created:{GetType().Name}");
            }

        }

        public class Account:Base, IAccount{}
        public class Message:Base, IMessage{}
        public class Tool:Base, ITool{}

        public static void Run()
        {
            // 创建服务集合，并在此注册服务类型
            var serviceCollection = new ServiceCollection()
                .AddTransient<IAccount, Account>()
                .AddTransient<IMessage, Message>();

            // 这个容器构建器类型由Autofac提供
            // 所以这里创建的是Autofac提供的容器构建器对象
            var containerBuilder = new ContainerBuilder();

            // 调用容器构建器对象的Populate方法，可以将服务集合中的注册项添加到Autofac的容器中
            containerBuilder.Populate(serviceCollection);

            // 使用autofac容器构建器的注册方法，来注册服务类型
            // Autofac的注册和服务集合的注册效果是一样的，但是需要注意注册顺序
            // 如果在调用 Populate 之前进行注册，那么服务集合中的注册类型会覆盖 Autofac 的注册；
            // 如果在调用 Populate 之后进行注册，Autofac 注册的类型会覆盖服务集合的注册。
            // 也就是说，调用 Populate 之前或者之后都可以进行注册
            containerBuilder.RegisterType<Tool>().As<ITool>();

            
            // 利用Auofac的容器构建器对象构建出作为依赖注入容器的服务提供对象
            // 创建AutofacServiceProvider类，也就是Autofac的服务提供对象
            // 由于这个类型是IServiceProvider接口的实现，所以我们可以直接使用IServiceProvider接口获取服务实例
            var container = containerBuilder.Build();
            IServiceProvider provider = new AutofacServiceProvider(container);
            
            Debug.Assert(provider.GetService<IAccount>() is Account);
            Debug.Assert(provider.GetService<IMessage>() is Message);
            Debug.Assert(provider.GetService<ITool>() is Tool);
        }
    }
}
