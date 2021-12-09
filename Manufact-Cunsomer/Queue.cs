using System;
using System.Collections.Generic;
using System.Threading;

namespace Manufacturers_Cunsomers
{
    public class Queue
    {
        private static Goals _goals = new Goals();
        private static Queue<Goals> buffer = new Queue<Goals>();
        private static int _maxSize = 5;
        private static Mutex _mfcAndCmrWait = new Mutex();
        private static Mutex _cunsomer = new Mutex();
        
        public void AddGoals()
        {
            _mfcAndCmrWait.WaitOne();
            if (buffer.Count<_maxSize)
            {
                buffer.Enqueue(_goals);
                Thread.Sleep(500);
                ConsoleHelper.WriteToConsole($"Manufacture {Thread.CurrentThread.ManagedThreadId} add new goal");
                _mfcAndCmrWait.ReleaseMutex();
                ManufacturerWait();
            }
            else
            {
                ConsoleHelper.WriteToConsole($"buffer id full!");
                _mfcAndCmrWait.ReleaseMutex();
                ManufacturerWait();
            }
        }
        
        private void ManufacturerWait()
        {
            Thread.Sleep(100);
            AddGoals();
        }


        public void CompleteGoal()
        {
            _mfcAndCmrWait.WaitOne();
            if (buffer.Count>0)
            {
                buffer.Dequeue();
                Thread.Sleep(500);
                ConsoleHelper.WriteToConsole($"Cunsomer {Thread.CurrentThread.ManagedThreadId} complete goal");
                _mfcAndCmrWait.ReleaseMutex();
                CunsomerWait();
            }
            else
            {
                ConsoleHelper.WriteToConsole($"buffer is empty!");
                _mfcAndCmrWait.ReleaseMutex();
                CunsomerWait();
            }
        }
        
        private void CunsomerWait()
        {
            Thread.Sleep(100);
            CompleteGoal();
        }
        
    }
}