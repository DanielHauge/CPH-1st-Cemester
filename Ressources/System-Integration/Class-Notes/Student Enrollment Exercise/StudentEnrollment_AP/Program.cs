using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollment_AP
{
    class Program
    {
        static void Main(string[] args)
        {

            BasicRabbitManager MQ = new BasicRabbitManager();

            MQ.WorkerSendMessage("AP", Encoding.UTF8.GetBytes("Enrollment_To_AP:" + new Random().Next(100)));

        }
    }
}
