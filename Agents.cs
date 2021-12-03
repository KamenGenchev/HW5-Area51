using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW5_Area51
{
    class Agents
    {
        Random random = new Random();
        enum AgentActivity { Work, UseElevator, GoHome };

        public Base.BaseFloor currentFloor { get; private set; }
        public string Name { get; set; }
        public Base.SecurityLevel SecurityLevel { get; }
        public Elevator Elevator { get; }

        public Agents(string name, Elevator elevator, Base.SecurityLevel securityLevel)
        {
            Name = name;
            SecurityLevel = securityLevel;
            Elevator = elevator;
            
        }

        private AgentActivity GetRandomAgentActivity()
        {
            int n = random.Next(10);
            if (n < 3) return AgentActivity.Work;
            if (n < 8) return AgentActivity.UseElevator;
            return AgentActivity.GoHome;
        }
        private Base.BaseFloor GetRandomFloor()
        {
            int n = random.Next(16);
            if (n < 5) return Base.BaseFloor.G;
            if (n < 9) return Base.BaseFloor.S;
            if (n < 13) return Base.BaseFloor.T1;
            return Base.BaseFloor.T2;
        }

        private void UseElevator()
        {
            lock (Elevator)
            {
                Console.WriteLine($"{Name} calls the elevator.");
                Elevator.CallElevator(this);
                Console.WriteLine($"{Name} entered the elevator.");
                    
                        Base.BaseFloor floor = GetRandomFloor();
                        Console.WriteLine($"{Name} selects floor {floor}.");
                        Elevator.GoToFloor(floor);
            }
        }

        public void WorkWorkWork()
        {
            Console.WriteLine($"{Name} has arrived to the base.");

            bool working = true;
            while (working)
            {
                var NextActivity = GetRandomAgentActivity();
                switch (NextActivity)
                {
                    case AgentActivity.Work:
                        Console.WriteLine($"{Name} is working on floor {currentFloor}.");
                        Thread.Sleep(100);
                        break;
                        
                    case AgentActivity.UseElevator:
                        UseElevator();
                        working = false;
                        break;
                    case AgentActivity.GoHome:
                        working = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
            Console.WriteLine($"{Name} is going back home.");
        }
    }
}
