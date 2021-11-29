using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manufacturers_Cunsomers
{
    public class Cunsomer
    {
        public string Name {get; set;}

        public Cunsomer()
        {
            Thread _thread = new Thread(CreateThread);
            _thread.Start();
        }

        public async Task CompleteGoal(Queue<Goals> buffer)
        {
            if (buffer.Count>0)
            {
                Console.WriteLine($"Cunsomer {Name} comleting task");
                Thread.Sleep(2000);
                await Task.Run(()=> buffer.Dequeue());
                Console.WriteLine($"Cunsomer {Name} complete task");
                CunsomerWait(buffer);
            }
            else
            {
               await Task.Run(()=>CunsomerWait(buffer)); 
            }
        }
        
        private void CunsomerWait(Queue<Goals> buffer)
        {
            Console.WriteLine($"Manufacturer {Name} is wait");
            Thread.Sleep(2000);
           CompleteGoal(buffer);
        }
        
        private void CreateThread()
        {
            Console.WriteLine($"Cunsomer with ID - {Thread.CurrentThread.ManagedThreadId} was created");
        }
        
    }
}