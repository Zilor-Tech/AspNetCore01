using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IAccount{ }

    public interface IMessage
    {
        public string Text { get; set; }
    }
    public interface ITool{ }

    public class Account: IAccount{}
    public class Message: IMessage
    {
        public string Text { get; set; }

        public Message()
        {
            Text = "Hello Message";
        }
    }
    public class Tool: ITool{}
}
