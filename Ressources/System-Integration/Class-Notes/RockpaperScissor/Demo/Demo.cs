using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace Demo
{
    class Demo
    {
        private MessageQueue mq;
        public string myText = "Not initialized";

        private void GetChannel()
        {
            if (MessageQueue.Exists(@".\Private$\MyQueue1"))
                mq = new System.Messaging.MessageQueue(@".\Private$\MyQueue1");
            else
                mq = MessageQueue.Create(@".\Private$\MyQueue1");

            Console.WriteLine("Queue Created");
        }

        private void Populate()
        {
            Message msg = new Message();
            myText = "Body text";
            msg.Body = myText;
            msg.Label = "Tine Marbjerg";
            mq.Send(msg);
            Console.WriteLine("Posted in mq1");
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

        static void Main(string[] args)
        {
            Demo d = new Demo();
            d.GetChannel();
            d.Populate();

            string result = "";
           //result = d.GetResult();

            Console.WriteLine("  send: {0} ", d.myText);
            Console.WriteLine("  receive: {0} ", result);
            Console.ReadLine();
        }
    }
}
