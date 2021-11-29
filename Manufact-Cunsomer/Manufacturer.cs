using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manufacturers_Cunsomers
{
    public class Manufacturer
    {
        public string Name { get; set; }
        Mutex mutex = new Mutex();
        public Manufacturer()
        {

        }
        
        
        public void CreateGoal(Queue<Goals> buffer,Goals goals, int size)
        {
            if (buffer.Count<size)
            {
                mutex.WaitOne();
                Console.WriteLine($"Manufacturer {Name} creating a goal");
                Thread.Sleep(2000);
                buffer.Enqueue(goals);
                Console.WriteLine($"Manufacturer {Name} create a goal");
                ManufacturerWait(buffer,goals,size);
                mutex.ReleaseMutex();
            }
            else
            {
                ManufacturerWait(buffer,goals,size);
            }
        }
        
        
        
        private void ManufacturerWait(Queue<Goals> buffer,Goals goals, int size)
        {
            Console.WriteLine($"Manufacturer {Name} is wait");
            Thread.Sleep(500);
            CreateGoal(buffer,goals, size);
        }

        public void CreateThread(Queue<Goals> buffer,Goals goals, int size)
        {
            CreateGoal(buffer,goals, size);
        }
        
    }
}