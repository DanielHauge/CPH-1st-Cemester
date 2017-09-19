using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading;

namespace AdminEnrollment_AP
{
    class Program
    {

        static void Main(string[] args)
        {

            BasicRabbitManager MQ = new BasicRabbitManager();
            while (true)
            {
                Console.ReadLine();
                HandleRequest(MQ.WorkerReceiveMessage("AP"), MQ);
            }


        }

        private static void HandleRequest(string v, BasicRabbitManager MQ)
        {
            Console.WriteLine(v);
            if (v.Split(':')[0].Equals("Enrollment_To_AP")){
                if (Int32.Parse(v.Split(':')[1].ToString()) > 30)
                {
                    MQ.WorkerSendMessage("EnrollmentResponse", Encoding.UTF8.GetBytes("Student who applied for AP with the score of" + v.Split(':')[1].ToString() + " got accepted"));
                }
                else
                {
                    MQ.WorkerSendMessage("EnrollmentResponse", Encoding.UTF8.GetBytes("Student who applied for AP with the score of" + v.Split(':')[1].ToString() + " did not get accepted"));
                }
            }
            else
            {
                MQ.WorkerSendMessage("InvalidLetterChannel", Encoding.UTF8.GetBytes("InvaliddLetter received: "+v));
            }
        }
    }
}
