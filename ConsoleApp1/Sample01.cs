using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    public static class Sample01
    {
        public interface IAccount{ }
        public interface IMessage{ }
        public interface ITool{ }

        public class Account: IAccount{}
        public class Message: IMessage{}
        public class Tool: ITool{}

        public static void Run()
        {
            var provider = new ServiceCollection()
                .AddTransient<IAccount, Account>()
                .AddScoped<IMessage, Message>()
                .AddSingleton<ITool, Tool>()
                .BuildServiceProvider();

            Debug.Assert(provider.GetService<IAccount>() is Account);
            Debug.Assert(provider.GetService<IMessage>() is Message);
            Debug.Assert(provider.GetService<ITool>() is Tool);
        }
    }
}
