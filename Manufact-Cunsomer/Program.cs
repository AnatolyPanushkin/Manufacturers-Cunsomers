using System;
using System.Collections.Generic;
using System.Threading;

namespace Manufacturers_Cunsomers
{
    class Program
    { 
        static void Main(string[] args)
        {
            Queue manufacturer = new Queue();
            Queue cunsomer = new Queue();
            
            
            Thread[] manufacturers = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                manufacturers[i] = new Thread(manufacturer.AddGoals);
            }

            Thread[] cunsomers = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                cunsomers[i] = new Thread(cunsomer.CompleteGoal);
            }

            foreach (var v in manufacturers)
            {
                v.Start();
            }

            foreach (var v in cunsomers)
            {
                v.Start();
            }
            
            foreach (var v in cunsomers)
            {
                v.Join();
            }
            
            foreach (var v in manufacturers)
            {
                v.Join();
            }
            
            

            
            Console.ReadLine();
        }
        

    }
}