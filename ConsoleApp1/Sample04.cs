using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    public static class Sample04
    {
        public interface IAccount{ }
        public interface IMessage{ }
        public interface ITool{ }

        public class Base : IDisposable
        {
            public Base()
            {
                Console.WriteLine($"Created:{GetType().Name}");
            }


            public void Dispose()
            {
                Console.WriteLine($"Disposed: {GetType().Name}");
            }
        }

        public class Account: Base, IAccount{}
        public class Message:Base, IMessage{}
        public class Tool:Base, ITool{}

        public static void Run()
        {
            using (var root = new ServiceCollection()
                .AddTransient<IAccount, Account>()
                .AddScoped<IMessage, Message>()
                .AddSingleton<ITool, Tool>()
                .BuildServiceProvider())
            {
                using (var scope = root.CreateScope())
                {
                    var child = scope.ServiceProvider;
                    child.GetService<IAccount>();
                    child.GetService<IMessage>();
                    child.GetService<ITool>();
                    Console.WriteLine("释放子容器");
                }
                Console.WriteLine("释放根容器");
            }
        }
    }
}
