// using System.Collections.Generic;
// using System.Linq;
//
// namespace Manufacturers_Cunsomers
// {
//     public class Queue<T>
//     {
//         private List<T> items = new List<T>();
//
//         private T Head => items.Last();
//         private T Tail => items.First();
//         public int Size = 0;
//         public int Count =2;
//         
//         public Queue(){}
//
//         public Queue(T data)
//         {
//             items.Add(data);
//         }
//
//         public void Enqueue(T data)
//         {
//             items.Insert(0, data);
//             Size++;
//         }
//
//         public T Dequeue()
//         {
//             var item = Head;
//             items.Remove(item);
//             Size--;
//             return item;
//         }
//
//         public T Peek()
//         {
//             return Head;
//         }
//         
//     }
// }