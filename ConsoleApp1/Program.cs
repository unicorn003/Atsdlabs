using System;
using System.Collections;
using System.Collections.Generic;


namespace Lab1
{
    class Node<T> 
    {
        public Node(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
        public Node<T> Next { get; set; }
    }

   public class LinkedList<T>  where T : IComparable<T>
    {
        Node<T> head; //first element
        Node<T> tail; //last element
        int count;      // size of list

        public LinkedList() //make a list with a constructor
        {
            count = 0;
            head = null;
            tail = null;
        }

        public bool IsEmpty() //metod for cheking empty last
        {
            bool fl = false;
            if (count == 0)
                fl = true;
            return fl;
        }

        public int Search(T item) //define the number of element
        {
            int number = 1;
            Node<T> current = head;
            while (current.Next != null)
            {
                if (item.CompareTo(current.Data) == 0)
                    break;
                else
                {
                    number++;

                    current = current.Next;
                }
            }
            return number;
        }

        public bool IsHave(T data)
        {

            Node<T> current = new Node<T> (data );
            while (current != null)
            {
                if (data.CompareTo(current.Data) == 0)
                    return true;
                current = current.Next;
            }
            return false;
        }
        public bool IsContainAll(LinkedList<T> B) //check the All elements of B containts into A
        {
            bool fl = true;
            Node<T> current = B.head;
            while (fl && current != null)
            {
                if (IsHave(current.Data) == false)
                { 
                    fl = false;
                    break;
                }
                current = current.Next;
            }
            return fl;
        }
        public void AddItem(T item)//add
        {
            Node<T> node = new Node<T>(item);
            if (count == 1 && item.CompareTo(head.Data) > 0) 
            {
                
                tail = node;
                head.Next = tail;
            }
                
            if (IsEmpty())  //add if we have an empty list
                head = node;
            if ( item.CompareTo(head.Data) < 0) //add before list
            {
                node.Next = head;
                head = node;
            }
            if (item.CompareTo(head.Data) > 0 && item.CompareTo(tail.Data)< 0)   //add into list
            {
                Node<T> current = head;
                while (current.Next != null)
                {
                    if (item.CompareTo(current.Data)> 0 && item.CompareTo(current.Next.Data) < 0)
                    {
                        node.Next = current.Next;
                        current.Next = node; 
                    }
                        else
                    current = current.Next;
                }
            }
            else //add after list
            {
                tail.Next = node;

                tail = node;
            }
            count++;
        }

        public void DeleteItem( T item) //delete element
        {
            Node<T> previous = head;
            Node<T> current = previous.Next;
            if (!IsHave(item))
                Console.WriteLine("Element is not present here");
            else { 
                if (item.CompareTo(head.Data)== 0)
                {
                    head = head.Next;
                }
                while (current != tail)
                {
                    if (item.CompareTo(current.Data) == 0)
                    {
                        previous.Next = current.Next;
                        
                    }
                    current = current.Next;
                }
                if (item.CompareTo(tail.Data) == 0)
                    tail = current;
                count--;
            }
            
        }
        
        public int ListSize()// return the number of element
        {
            return count;
        }

        public void PrintList() // print it
        {
            Node<T> current = head;
            
            while (current.Next != null)
            {
                Console.Write(current + " ");
                current = current.Next;
            }
            
        } 

        
    }



    class Program
    {
        static void Main()
        {
            Console.WriteLine("lets talk about");
        }
    }
}
