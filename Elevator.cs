using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW5_Area51
{
    class Elevator
    {
        private List<Base.BaseFloor> floors;
        public Base.BaseFloor currentFloor { get; private set; } 

        Semaphore semaphore = new Semaphore(1, 1);

        Agents currentAgent;

        public Elevator(List<Base.BaseFloor> floors)
        {
            this.floors = floors;
            currentFloor = 0;
            semaphore = new Semaphore(1, 1);
        }

        public void CallElevator(Agents agent)
        {
            semaphore.WaitOne();
            currentAgent = agent;
            Thread.Sleep(1000);
            currentFloor = agent.currentFloor;
        }

        private bool SecurityCheck()
        {
            var securityLevel = Base.SecurityLevel.Top_Secret;
            if (currentAgent.SecurityLevel < securityLevel)
            {
                securityLevel = currentAgent.SecurityLevel;
            }
            return false;
        }


        public bool GoToFloor(Base.BaseFloor floor)
        {
            Thread.Sleep(1000);
            currentFloor = floor;
           

            if (currentAgent.SecurityLevel == Base.SecurityLevel.Top_Secret) return true;
            if ((int)currentAgent.SecurityLevel >= (int)currentFloor) return true;
            return false;
        }

        public void Leave()
        {
            currentAgent = null;
            semaphore.Release();
        }
    }
}
