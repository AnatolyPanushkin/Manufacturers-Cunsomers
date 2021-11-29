﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Manufacturers_Cunsomers
{
    class Program
    {
        //исполнитель должен поождать
        private static Mutex _cunsomerMustWait = new Mutex();
        //производители должны подождать
        private static Mutex _manufacturerMustWait = new Mutex();
        //флаг создания задачи
        private static bool _manufact = false;
        
        private static Queue<Goals> buffer = new Queue<Goals>();
        private static Goals _goals = new Goals();
        private static int maxSize = 3;
        
        static void Main(string[] args)
        {

            Thread[] manufacturers = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                manufacturers[i] = new Thread(CreateGoals);
            }

            Thread[] cunsomers = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                cunsomers[i] = new Thread(CompleteGoal);
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

        //Функция создания задачи для производителя
        static void CreateGoals()
        {
            if (buffer.Count<maxSize)
            {
                _manufacturerMustWait.WaitOne();
                //Добавляем новую задачу
                Thread.Sleep(2000);
                buffer.Enqueue(_goals);
                ConsoleHelper.WriteToConsole($"Manufacturer {Thread.CurrentThread.ManagedThreadId} add new goal");
                _manufacturerMustWait.ReleaseMutex();
            }
            else
            {
                ManufacturerWait();
            }
        }

        static void ManufacturerWait()
        {
            if (buffer.Count>=maxSize)
                ConsoleHelper.WriteToConsole($"buffer is full!");
            Thread.Sleep(2000);
            CreateGoals();
        }


        static void CompleteGoal()
        {
            if (buffer.Count>0)
            {
                _cunsomerMustWait.WaitOne();
                Thread.Sleep(2000);
                buffer.Dequeue();
                ConsoleHelper.WriteToConsole($"Cunsomer {Thread.CurrentThread.ManagedThreadId} complete task");
                _cunsomerMustWait.ReleaseMutex();
            }
            else
            {
                CunsomerWait();
            }
        }

        static void CunsomerWait()
        {
            if (buffer.Count==0)
                ConsoleHelper.WriteToConsole("buffer is empty!");
            Thread.Sleep(2000);
            CompleteGoal();
        }
         
        
        
    }
}