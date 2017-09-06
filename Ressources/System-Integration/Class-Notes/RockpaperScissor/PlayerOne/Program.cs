using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace PlayerOne
{
  class Program
{
    private MessageQueue mqP1;
    private MessageQueue mqP2;
    public string playerOneHand = " Not initialized";
    
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

        Console.WriteLine(" Queues Created ");
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

    public string getPlayerTwoHand()
    {
        Message playerTwoHand;
        String hand = "";
        string label = " Label error in getPlayerTwoHand()";
        try
        {
            playerTwoHand = mqP2.Receive(new TimeSpan(0, 0, 50));
            playerTwoHand.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            hand = playerTwoHand.Body.ToString();
            label = playerTwoHand.Label;
        }
        catch
        {
         hand = " Error in getPlayerTwoHand()";
        }
         Console.WriteLine(" Hand received from " + label);
         return hand;
    }

    private void postHit()
    {
        Message m = new System.Messaging.Message();
        playerOneHand = getHand();
        m.Body = playerOneHand;
        m.Label = "PlayerOne";
        mqP1.Send(m);
        Console.WriteLine(" Hand posted in mqP1");
    }

    private string evaluateResult(string playeronehand, string playertwohand)
    {
        string returnResult = " Not yet calculated" + " " + playeronehand + " " + playertwohand;
        switch (playeronehand)
        {
            case "scissors":
            if (playertwohand.Equals("rock")) { returnResult = "You loose! he had " + playertwohand +
            " and beat your " + playeronehand; }
            if (playertwohand.Equals("paper")) { returnResult = "Gratz! you win, your " +
            playeronehand + " beat his " + playertwohand; }
            if (playertwohand.Equals("scissors")) { returnResult = "Same hand.. doh! " + playeronehand
            + " and " + playertwohand; }
            break;
            case "rock":
            if (playertwohand.Equals("rock")) { returnResult = "Same hand.. doh! " + playeronehand +
            " and " + playertwohand; }
            if (playertwohand.Equals("paper")) { returnResult = "You loose! he had " + playertwohand
            + " and beat your " + playeronehand; }
            if (playertwohand.Equals("scissors")) { returnResult = "Gratz! you win, your " +
            playeronehand + " beat his " + playertwohand; }
            break;
            case "paper":
            if (playertwohand.Equals("rock")) { returnResult = "Gratz! you win, your " +
            playeronehand + " beat his " + playertwohand; }
            if (playertwohand.Equals("paper")) { returnResult = "Same hand.. doh! " + playeronehand +
            " and " + playertwohand; }
            if (playertwohand.Equals("scissors")) { returnResult = "You loose! he had " +
            playertwohand + " and beat your " + playeronehand; }
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
        string playertwohand = p.getPlayerTwoHand();   
        Console.WriteLine(" p1= " + p.playerOneHand);
        Console.WriteLine(" p2= " + playertwohand);
        string result = p.evaluateResult(p.playerOneHand, playertwohand);
        Console.WriteLine(result);
        Console.ReadLine();
    }

   }
}
