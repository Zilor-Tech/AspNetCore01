using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    public static class Sample02
    {
        public interface IAccount{ }
        public interface IMessage{ }
        public interface ITool{ }

        public class Base
        {
            public Base()
            {
                Console.WriteLine($"{GetType().Name} 已创建");
            }
        }

        public class Account:Base, IAccount{}
        public class Message:Base, IMessage{}
        public class Tool:Base, ITool{}

        public static void Run()
        {
            var services = new ServiceCollection()
                .AddTransient<Base, Account>()
                .AddTransient<Base, Message>()
                .AddTransient<Base, Tool>()
                .BuildServiceProvider()
                .GetServices<Base>().ToList();

            Debug.Assert(services.OfType<Account>().Any());
            Debug.Assert(services.OfType<Message>().Any());
            Debug.Assert(services.OfType<Tool>().Any());
        }
    }
}
