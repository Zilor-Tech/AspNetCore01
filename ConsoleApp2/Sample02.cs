using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp2
{
    public static class Sample02
    {
        public interface IAccount{ }
        public interface IMessage{ }
        public interface ITool{ }

        public interface ITest{ }


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
        public class Test: ITest
        {
            public IMessage Message { get; set; }

            public Test(IAccount account)
            {
                Console.WriteLine($"Ctor:Test(IAccount)");
            }
        }

        public static void Run()
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<IAccount, Account>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(serviceCollection);
            // 属性注入
            containerBuilder.RegisterType<Test>().As<ITest>().PropertiesAutowired();
            // 程序集注入
            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                // 筛选基类为Base
                .Where(t => t.BaseType == typeof(Base))
                // 暴露第一个接口
                .As(t => t.GetInterfaces()[0])
                // 生命周期模式为Scope
                .InstancePerLifetimeScope();

            var container = containerBuilder.Build();
            IServiceProvider provider = new AutofacServiceProvider(container);
            
            Debug.Assert(provider.GetService<IAccount>() is Account);
            Debug.Assert(provider.GetService<IMessage>() is Message);
            Debug.Assert(provider.GetService<ITool>() is Tool);

            var test = provider.GetService<ITest>();
            Console.Read();
        }
    }
}
