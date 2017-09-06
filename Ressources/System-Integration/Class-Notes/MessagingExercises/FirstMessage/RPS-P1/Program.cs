using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace RPS_P1
{
    class Program
    {
        private bool GameInSession;
        private MessageQueue mq;
        private MessageQueue p1_p2;
        private MessageQueue p2_p1;

        static void Main(string[] args)
        {
            Program thisProgram = new Program() { GameInSession = false };
            thisProgram.GetChannels();
            



        }
        /// <summary>
        /// This function is to make it possible to start up without having errors on which order the players are initiated.
        /// </summary>
        private void TryInitGame()
        {
            
            if (GetResult() == "RequestGame")
            {
                this.Populate("Player 1", "Accept");
                this.GameInSession = true;
            } else
            {
                this.Populate("Player 1", "RequestGame");
            }

        }


        private void Populate(string WhoAmI, string BodyText)
        {
            Message msg = new Message();
            msg.Body = BodyText;
            msg.Label = WhoAmI;
            p1_p2.Send(msg);
            Console.WriteLine("Posted in FirstQue");
        }

        private string GetResult()
        {
            Message msg;
            string str = "";
            string label = "";
            try
            {
                msg = p2_p1.Receive(new TimeSpan(0, 0, 50));
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

        private void GetChannels()
        {
            if (MessageQueue.Exists(@".\Private$\P1>P2"))
                p1_p2 = new System.Messaging.MessageQueue(@".\Private$\P1>P2");
            else
                p1_p2 = MessageQueue.Create(@".\Private$\P1>P2");

            if (MessageQueue.Exists(@".\Private$\P2>P1"))
                p2_p1 = new System.Messaging.MessageQueue(@".\Private$\P2>P1");
            else
                p2_p1 = MessageQueue.Create(@".\Private$\P2>P1");

            Console.WriteLine("Queue's Created");
        }
    }
}
