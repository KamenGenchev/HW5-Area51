using System;
using System.Collections.Generic;
using System.Threading;

namespace HW5_Area51
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Elevator elevator = new Elevator();
            Random random = new Random();

            List<Thread> agentThreads = new List<Thread>();

            for (int i = 1; i < 20; i++)
            {

                Base.SecurityLevel securityLevel = (Base.SecurityLevel)random.Next(3);
           
                var agents = new Agents(i.ToString(), elevator, securityLevel );
                var thread = new Thread(agents.WorkWorkWork);
                thread.Start();
                agentThreads.Add(thread);
            }
            foreach (var t in agentThreads) t.Join();
            Console.WriteLine();
            Console.WriteLine("Time to go home!");
            Console.ReadLine();
        }
    }
}
