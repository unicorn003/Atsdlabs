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

    class LinkedList<T> : IEnumerable<T>
    {
        Node<T> head;
        Node<T> tail;
        int count;
        public bool IsEmpty()
        {
            bool fl = false;
            return fl;
        }

        public int Search()
        {

        }

        public void AddItem()
        {

        }

        public T DeleteItem()
        {

        }

        public int ListSize()
        {

        }

        public void PrintList()
        {

        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I mind about it");
        }
    }
}
