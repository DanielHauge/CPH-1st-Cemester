using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQ_Messaging_Exercise
{
    class Program
    {
        private MessageQueue P1_P2;
        private MessageQueue P2_P1;
        private enum GameHand { Rock, Paper, Scissor};
        private GameHand OpponentHand;
        private bool OpponentChose;
        private GameHand MyPlayingHand;
        private string WhoAmI;
        private bool Game_in_session;

        static void Main(string[] args)
        {
            Program player1 = new Program() { WhoAmI = "Player1", Game_in_session = false };
            player1.GetChannel();
            Console.WriteLine((GameHand)new Random(DateTime.UtcNow.Millisecond).Next(0, 3));
            
            while (true)
            {
                Command(Console.ReadLine(), player1, player1.P1_P2, player1.P2_P1);
            }



        }

        private static void Command(string v, Program player, MessageQueue Outgoing, MessageQueue Incomming)
        {
            Console.WriteLine(" exit - play - randomhand - watch || Gamesession: "+player.Game_in_session);
            switch (v)
                {
                case "exit":
                    System.Environment.Exit(0);
                    break;
                case "play":
                    player.InitGame(Outgoing, Incomming);
                    player.OpponentChose = false;
                    break;
                case "randomhand":
                    player.MyPlayingHand = player.GetHand();
                    string s = player.LookInChannelForMessage(Incomming);
                    if (s.Contains("Hand:")) {
                        player.OpponentHand = player.GetHand(s.Split(':').Last());
                        player.Populate(Outgoing, player.GenerateConclusion(player.MyPlayingHand, player.OpponentHand), player.WhoAmI);
                    }
                    else player.Populate(Outgoing, "Hand:"+player.MyPlayingHand.ToString(), player.WhoAmI); 
                    break;
                case "paper":
                    player.MyPlayingHand = player.GetHand("Paper");
                    string s1 = player.LookInChannelForMessage(Incomming);
                    if (s1.Contains("Hand:"))
                    {
                        player.OpponentHand = player.GetHand(s1.Split(':').Last());
                        player.Populate(Outgoing, player.GenerateConclusion(player.MyPlayingHand, player.OpponentHand), player.WhoAmI);
                    }
                    else player.Populate(Outgoing, "Hand:" + player.MyPlayingHand.ToString(), player.WhoAmI);
                    break;
                case "scissor":
                    player.MyPlayingHand = player.GetHand("Scissor");
                    string s2 = player.LookInChannelForMessage(Incomming);
                    if (s2.Contains("Hand:"))
                    {
                        player.OpponentHand = player.GetHand(s2.Split(':').Last());
                        player.Populate(Outgoing, player.GenerateConclusion(player.MyPlayingHand, player.OpponentHand), player.WhoAmI);
                    }
                    else player.Populate(Outgoing, "Hand:" + player.MyPlayingHand.ToString(), player.WhoAmI);
                    break;
                case "rock":
                    player.MyPlayingHand = player.GetHand("Rock");
                    string s3 = player.LookInChannelForMessage(Incomming);
                    if (s3.Contains("Hand:"))
                    {
                        player.OpponentHand = player.GetHand(s3.Split(':').Last());
                        player.Populate(Outgoing, player.GenerateConclusion(player.MyPlayingHand, player.OpponentHand), player.WhoAmI);
                    }
                    else player.Populate(Outgoing, "Hand:" + player.MyPlayingHand.ToString(), player.WhoAmI);
                    break;
                case "watch":
                    if (player.LookInChannelForMessage(Incomming).Contains("Conclusion:"))
                        {
                        player.Game_in_session = false;
                        }
                    break;
                default:
                    Console.WriteLine("No valid command input");
                    break;




            }
        }

        private string GenerateConclusion(GameHand player1, GameHand player2)
        {
            string Conclusion = "";
            if (player1 == player2) Conclusion += "Conclusion: Tie! - player1:" + player1.ToString() + " vs. player2:" + player2.ToString();
            else
            {
                if (player1 == (GameHand)0 && player2 == (GameHand)2) Conclusion += "Conclusion: Player1 Wins! - player1:" + player1.ToString() + " vs. player2:" + player2.ToString();
                else if (player1 == (GameHand)0 && player2 == (GameHand)1) Conclusion += "Conclusion: Player2 Wins! - player1:" + player1.ToString() + " vs. player2:" + player2.ToString();
                else if (player1 == (GameHand)1 && player2 == (GameHand)2) Conclusion += "Conclusion: Player2 Wins! - player1:" + player1.ToString() + " vs. player2:" + player2.ToString();
                else if (player1 == (GameHand)1 && player2 == (GameHand)0) Conclusion += "Conclusion: Player1 Wins! - player1:" + player1.ToString() + " vs. player2:" + player2.ToString();
                else if (player1 == (GameHand)2 && player2 == (GameHand)1) Conclusion += "Conclusion: Player1 Wins! - player1:" + player1.ToString() + " vs. player2:" + player2.ToString();
                else if (player1 == (GameHand)2 && player2 == (GameHand)0) Conclusion += "Conclusion: Player2 Wins! - player1:" + player1.ToString() + " vs. player2:" + player2.ToString();
                else { Conclusion += "Something went wrong"; Console.WriteLine("somewthing went wrong in Conclusion"); }


            }
            Game_in_session = false;
            return Conclusion;
        }

        private void InitGame(MessageQueue mq, MessageQueue incomming)
        {
            if (!Game_in_session)
            {
                if (this.LookInChannelForMessage(incomming).Equals("GameInvitation"))
                {
                    Game_in_session = true;
                    Console.WriteLine("Accepted invitation to play");
                }
                else
                {
                    this.Populate(mq, "GameInvitation", WhoAmI);
                    Console.WriteLine("Invited To play");
                }
            }
            else Console.WriteLine("Cannot Initialize game when a game is allready in session.");
            
        }


        private GameHand GetHand()
        {

            return (GameHand)new Random(DateTime.UtcNow.Millisecond).Next(0, 3);
        }
        private GameHand GetHand(string s)
        {
            if (s.Equals("Rock"))
            {
                return GameHand.Rock;
            } else if (s.Equals("Paper"))
            {
                return GameHand.Paper;
            } else if (s.Equals("Scissor"))
            {
                return GameHand.Scissor;
            } else
            {
                Console.WriteLine("Error: invalid string. Therefor giving random.");
                return (GameHand)new Random(DateTime.UtcNow.Millisecond).Next(0, 3);
            }

        }
        private GameHand GetHand(int s)
        {
            try
            {
                return (GameHand)s;
            } catch
            {
                Console.WriteLine("Error: out of bounds. parsing int to get hand needs to be either 0, 1 or 2. Therefor giving random.");
                return (GameHand)new Random(DateTime.UtcNow.Millisecond).Next(0, 3);
            }
            
        }



        private void GetChannel()
        {
            if (MessageQueue.Exists(@".\Private$\P1>P2"))
                P1_P2 = new System.Messaging.MessageQueue(@".\Private$\P1>P2");
            else
                P1_P2 = MessageQueue.Create(@".\Private$\P1>P2");

            if (MessageQueue.Exists(@".\Private$\P2>P1"))
                P2_P1 = new System.Messaging.MessageQueue(@".\Private$\P2>P1");
            else
                P2_P1 = MessageQueue.Create(@".\Private$\P2>P1");
            
            Console.WriteLine("Queue Created");
        }

        private void Populate(MessageQueue mq, string Message, string who)
        {
            Message msg = new Message();
            msg.Body = Message;
            msg.Label = who;
            mq.Send(msg);
            Console.WriteLine("/// Posted message in: " + mq.QueueName.ToString() + " - Who: "+who+" - Message: "+Message);
        }

        private string LookInChannelForMessage(MessageQueue mq)
        {
            if (mq.GetAllMessages().Length>0)
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
                Console.WriteLine("'\\\' Received message in " + mq.QueueName.ToString() + " - From: " + label + " - Message: " + str);
                return str;
            }
            else return "MessageQue Was Empty";
        }

     


    }
}
