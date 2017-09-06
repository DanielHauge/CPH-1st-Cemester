using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace FirstMessage
{
    class Program
    {
        private MessageQueue mq;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello");

            Program _This = new Program();
            _This.GetChannel();
            _This.Populate("Heaaallooo", "This person did it", "This is bodytext");
            Console.ReadLine();
            Console.WriteLine(_This.GetResult());
            Console.ReadLine();

        }

        private void GetChannel()
        {
            if (MessageQueue.Exists(@".\Private$\FirstQue"))
                mq = new System.Messaging.MessageQueue(@".\Private$\FirstQue");
            else
                mq = MessageQueue.Create(@".\Private$\FirstQue");

            Console.WriteLine("Queue Created");
        }

        private void Populate(string myText, string Label, string BodyText)
        {
            Message msg = new Message();
            myText = BodyText;
            msg.Body = myText;
            msg.Label = Label;
            mq.Send(msg);
            Console.WriteLine("Posted in FirstQue");
        }


        private string GetResult()
        {
            Message msg;
            string str = "";
            string label = "";
            try
            {
                msg = mq.Receive(new TimeSpan(0, 0, 50));
                msg.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                str = msg.Body.ToString();
                label = msg.Label;
            }
            catch
            {
                str = "Error in GetResult()";
            }
            Console.WriteLine("Received from " + label);
            return str;
        }
    }
}
