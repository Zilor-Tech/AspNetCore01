using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    public static class Sample03
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

        public class Account: Base, IAccount{}
        public class Message:Base, IMessage{}
        public class Tool:Base, ITool{}

        public static void Run()
        {
            var root = new ServiceCollection()
                .AddTransient<IAccount, Account>()
                .AddScoped<IMessage, Message>()
                .AddSingleton<ITool, Tool>()
                .BuildServiceProvider();
            var child1 = root.CreateScope().ServiceProvider;
            var child2 = root.CreateScope().ServiceProvider;

            GetService<IAccount>(child1);
            GetService<IMessage>(child1);
            GetService<ITool>(child1);
            Console.WriteLine();
            GetService<IAccount>(child2);
            GetService<IMessage>(child2);
            GetService<ITool>(child2);
        }

        public static void GetService<T>(IServiceProvider provider)
        {
            provider.GetService<T>();
            provider.GetService<T>();
        }
    }
}
