using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    public static class Sample05
    {
        public interface IAccount{ }
        public interface IMessage{ }
        public interface ITool{ }

        public interface ITest{ }

        public class Account: IAccount{}
        public class Message: IMessage{}
        public class Tool: ITool{}
        public class Test: ITest
        {
            public Test(IAccount account)
            {
                Console.WriteLine($"Ctor:Test(IAccount)");
            }

            public Test(IAccount account, IMessage message)
            {
                Console.WriteLine($"Ctor:Test(IAccount,IMessage)");
            }

            public Test(IAccount account, IMessage message, ITool tool)
            {
                Console.WriteLine($"Ctor:Test(IAccount,IMessage,ITool)");
            }
        }

        public static void Run()
        {
            var test = new ServiceCollection()
                .AddTransient<IAccount, Account>()
                .AddTransient<IMessage, Message>()
                .AddTransient<ITest, Test>()
                .BuildServiceProvider()
                .GetService<ITest>();
        }
    }
}
