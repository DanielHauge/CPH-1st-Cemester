using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace PlayerTwo
{
    class Program
    {
        private MessageQueue mqP1;
        private MessageQueue mqP2;
        public string playerTwoHand = " Not initialized";
        private void joinChannel()
        {
            if (MessageQueue.Exists(@".\Private$\MyQueueP1"))
                mqP1 = new System.Messaging.MessageQueue(@".\Private$\MyQueueP1");
            else
                mqP1 = MessageQueue.Create(@".\Private$\MyQueueP1");
            if (MessageQueue.Exists(@".\Private$\MyQueueP2"))
                mqP2 = new System.Messaging.MessageQueue(@".\Private$\MyQueueP2");
            else
                mqP2 = MessageQueue.Create(@".\Private$\MyQueueP2");
            Console.WriteLine(" Queue Created");
        }
        private string getHand()
        {
            string hand = " Error in getHand()";
            int i = RandomNumber(1, 3);
            switch (i)
            {
                case 1:
                    hand = "scissors";
                    break;
                case 2:
                    hand = "rock";
                    break;
                case 3:
                    hand = "paper";
                    break;
            }
            return hand;
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string getPlayerOneHand()
        {
            Message player1Hit;
            String hit = "";
            string label = " Label error in getPlayerTwoHand()";
            try
            {
                player1Hit = mqP1.Receive(new TimeSpan(0, 0, 50));
                player1Hit.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                hit = player1Hit.Body.ToString();
                label = player1Hit.Label;
            }
            catch
            {
                hit = " Error in getPlayerTwoHand()";
            }
            Console.WriteLine(" Hand received from " + label);
            // Console.ReadLine();
            return hit;
        }
        private void postHit()
        {
            Message m = new System.Messaging.Message();
            playerTwoHand = getHand();
            m.Body = playerTwoHand;
            m.Label = "Player2";
            mqP2.Send(m);
            Console.WriteLine(" Hand posted in mqP2");
            // Console.ReadLine();
        }
        private string evaluateResult(string playeronehand, string playertwohand)
        {
            string returnResult = " Not yet calculated" + " " + playeronehand + " " + playertwohand;
            switch (playertwohand)
            {
                case "scissors":
                    if (playeronehand.Equals("rock"))
                    {
                        returnResult = "You loose! he had " + playeronehand +
                            " and beat your " + playertwohand;
                    }
                    if (playeronehand.Equals("paper"))
                    {
                        returnResult = "Gratz! you win, your " +
                            playertwohand + " beat his " + playeronehand;
                    }
                    if (playeronehand.Equals("scissors"))
                    {
                        returnResult = "Same hand.. doh! " + playeronehand
                            + " and " + playertwohand;
                    }
                    break;
                case "rock":
                    if (playeronehand.Equals("rock"))
                    {
                        returnResult = "Same hand.. doh! " + playeronehand +
                            " and " + playertwohand;
                    }
                    if (playeronehand.Equals("paper"))
                    {
                        returnResult = "You loose! he had " + playeronehand
                            + " and beat your " + playertwohand;
                    }
                    if (playeronehand.Equals("scissors"))
                    {
                        returnResult = "Gratz! you win, your " +
                            playertwohand + " beat his " + playeronehand;
                    }
                    break;
                case "paper":
                    if (playeronehand.Equals("rock"))
                    {
                        returnResult = "Gratz! you win, your " +
                            playertwohand + " beat his " + playeronehand;
                    }
                    if (playeronehand.Equals("paper"))
                    {
                        returnResult = "Same hand.. doh! " + playeronehand +
                            " and " + playertwohand;
                    }
                    if (playeronehand.Equals("scissors"))
                    {
                        returnResult = "You loose! he had " +
                            playeronehand + " and beat your " + playertwohand;
                    }
                    break;
            }
            Console.WriteLine(" Result evaluated ");
            return returnResult;
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.joinChannel();
            p.postHit();  
            string playeronehand = p.getPlayerOneHand();
            Console.WriteLine(" p1= " + playeronehand);
            Console.WriteLine(" p2= " + p.playerTwoHand);
            string result = p.evaluateResult(playeronehand, p.playerTwoHand);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
