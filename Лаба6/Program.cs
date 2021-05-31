using System;
using System.Collections.Generic;
using System.Collections;

namespace Лаба6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Задание 1
            Student a = new Student() { Id = 247909 };
            Student b = new Student() { Id = 285428 };
            Student c = new Student() { Id = 285417 };
            List<Student> students = new List<Student>();

            students.Add(a);
            students.Add(b);
            students.Add(c);

            Console.WriteLine("Задание 1");
            PrintStudentID(0, students);
            PrintStudentID(1, students);
            PrintStudentID(2, students);

            // Задание 2
            DoublyLinkedList<ColorPalette> ColorPalettes = new DoublyLinkedList<ColorPalette>();
            ColorPalette Red = new ColorPalette("Красный");
            ColorPalette Green = new ColorPalette("Зелёный");
            ColorPalette Blue = new ColorPalette("Синий");
            ColorPalettes.Add(Red);
            ColorPalettes.Add(Green);
            ColorPalettes.Add(Blue);
            var ColorPalette = ColorPalettes.GetHead();

            Console.WriteLine();
            Console.WriteLine("Задание 2");
            do
            {
                Console.Write($"{ColorPalette.Data.color} ");
                ColorPalette = ColorPalette.Next;
            }
            while (ColorPalette != null);
            Console.Write("\n");

            // Задание 3
            CircularDoublyLinkedList<ColorPalette> trafficLights = new CircularDoublyLinkedList<ColorPalette>();
            ColorPalette Orange = new ColorPalette("Оранжевый");
            trafficLights.Add(Red);
            trafficLights.Add(Orange);
            trafficLights.Add(Green);
            var trafficLightsColorPalette = trafficLights.GetHead();
            Console.WriteLine();
            Console.WriteLine("Задание 3");
            for (int i = 0; i != 3 * trafficLights.GetCount(); i++)
            {
                Console.Write($"{trafficLightsColorPalette.Data.color} ");
                trafficLightsColorPalette = trafficLightsColorPalette.Next;
            }

            // Задание 4
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Задание 4");
            RestrictedQueue<ColorPalette> Gun = new RestrictedQueue<ColorPalette>() { Limit = 2, LimitErrorMassage = "Пистолет полон", EmptyErrorMassage = "Пистолет пуст" };
            AddСolorIntoGun(Gun, Orange);
            ShootСolor(Gun);
            AddСolorIntoGun(Gun, Blue);
            AddСolorIntoGun(Gun, Red);
            AddСolorIntoGun(Gun, Green);
            ShootСolor(Gun);
            ShootСolor(Gun);
            ShootСolor(Gun);

            // Задание 5
            Console.WriteLine();
            Console.WriteLine("Задание 5");
            RestrictedStack<ColorPalette> colorBasket = new RestrictedStack<ColorPalette>() { hasLimit = true, Limit = 2, LimitErrorMassage = "Обойма заполнена", EmptyErrorMassage = "Обойма пуста" };
            PutСolorIntoСartridge(Orange, colorBasket);
            PutСolorIntoСartridge(Blue, colorBasket);
            PutСolorIntoСartridge(Red, colorBasket);
            GetСolorFromСartridge(colorBasket);
            GetСolorFromСartridge(colorBasket);
            PutСolorIntoСartridge(Red, colorBasket);

            // Задание 6
            Console.WriteLine();
            Console.WriteLine("Задание 6");
            LinkedStack<ColorPalette> iceCream = new LinkedStack<ColorPalette>();
            AddColorInIceCream(iceCream, Orange);
            AddColorInIceCream(iceCream, Blue);
            EatColor(iceCream);
            AddColorInIceCream(iceCream, Red);
            EatColor(iceCream);
            EatColor(iceCream);

            // Задание 7
            Console.WriteLine();
            Console.WriteLine("Задание 7");
            PostfixExpressions postfixExpressions = new PostfixExpressions();
            Calculate(postfixExpressions);
            Calculate(postfixExpressions);
        }

        private static void Calculate(PostfixExpressions postfixExpressions)
        {
            Console.Write("Выражение: ");
            Console.WriteLine("Результат: " + postfixExpressions.Calculate(Console.ReadLine()));
        }

        private static void EatColor(LinkedStack<ColorPalette> iceCream)
        {
            var color = iceCream.Pop();
            Console.WriteLine($"{color.color} мороженое съели");
        }

        private static void AddColorInIceCream(LinkedStack<ColorPalette> iceCream, ColorPalette color)
        {
            iceCream.Push(color);
            Console.WriteLine($"Мороженное цвета {color.color}");
        }

        private static void PrintStudentID(int index, List<Student> list)
        {
            Console.WriteLine($"Студент под номером {index} в списке имеет ID: {list.Get(index)}");
        }

        private static void GetСolorFromСartridge(RestrictedStack<ColorPalette> colorBasket)
        {
            if (colorBasket.GetCount() == 0)
            {
                Console.WriteLine(colorBasket.EmptyErrorMassage);
                return;
            }
            var color = colorBasket.Pop();
            Console.WriteLine($"цвет {color.color} разрядили");
        }

        private static void PutСolorIntoСartridge(ColorPalette color, RestrictedStack<ColorPalette> colorBasket)
        {
            if (colorBasket.Push(color))
            {
                Console.WriteLine($"цвет {color.color} заряжен");
            }
        }

        private static void ShootСolor(RestrictedQueue<ColorPalette> Gun)
        {
            if (Gun.GetCount() == 0)
            {
                Console.WriteLine(Gun.EmptyErrorMassage);
                return;
            }
            var color = Gun.Dequeue();
            Console.WriteLine($"{color.color} цветом выстрелили");
        }

        private static void AddСolorIntoGun(RestrictedQueue<ColorPalette> Gun, ColorPalette color)
        {
            if (Gun.Enqueue(color))
            {
                Console.WriteLine($"{color.color} цвет заряжен в пистолете");
            }
        }
    }

    public class Student
    {
        public object Id { get; set; }
    }

    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }
    }

    public class ColorPalette
    {
        public string color { get; set; }
        public float radius { get; set; }
        public ColorPalette(string color)
        {
            this.color = color;
        }
    }

    public class List<T>
    {
        private T[] _array = new T[0];


        public void Add(T item)
        {
            var newArray = new T[_array.Length + 1];

            for (int i = 0; i < _array.Length; i++)
            {
                newArray[i] = _array[i];
            }

            newArray[_array.Length] = item;

            _array = newArray;
        }

        public T Get(int index)
        {
            return _array[index];
        }
    }

    public class DoublyLinkedList<T>
    {
        Node<T> head;
        Node<T> tail;
        public Node<T> GetHead()
        {
            return head;
        }
        public Node<T> GetTail()
        {
            return tail;
        }
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
            {

                head = node;
            }
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
        }

    }

    public class CircularDoublyLinkedList<T>
    {
        Node<T> head;
        int count;
        public int GetCount()
        {
            return count;
        }
        public Node<T> GetHead()
        {
            return head;
        }
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
            {
                head = node;
                head.Next = node;
                head.Previous = node;
            }
            else
            {
                node.Previous = head.Previous;
                node.Next = head;
                head.Previous.Next = node;
                head.Previous = node;
            }
            count++;
        }
    }

    public class RestrictedQueue<T>
    {
        T[] queue = new T[0];
        private object lockObject = new object();
        public int GetCount()
        {
            return queue.Length;
        }
        public int Limit { get; set; }
        public string LimitErrorMassage { get; set; }
        public string EmptyErrorMassage { get; set; }
        public T Dequeue()
        {
            var newQueue = new T[queue.Length - 1];

            for (int i = 0; i < queue.Length - 1; i++)
            {
                newQueue[i] = queue[i + 1];
            }

            var element = queue[0];

            queue = newQueue;
            return element;
        }

        public bool Enqueue(T obj)
        {
            if (queue.Length >= Limit)
            {
                Console.WriteLine(LimitErrorMassage);
                return false;
            }
            var newQueue = new T[queue.Length + 1];

            for (int i = 0; i < queue.Length; i++)
            {
                newQueue[i] = queue[i];
            }

            newQueue[queue.Length] = obj;

            queue = newQueue;
            return true;
        }

    }

    public class RestrictedStack<T>
    {
        T[] stack = new T[0];
        private object lockObject = new object();
        public int GetCount()
        {
            return stack.Length;
        }
        public bool hasLimit = false;
        public int Limit { get; set; }
        public string LimitErrorMassage { get; set; }
        public string EmptyErrorMassage { get; set; }
        public T Pop()
        {

            var newStack = new T[stack.Length - 1];

            for (int i = 0; i < stack.Length - 1; i++)
            {
                newStack[i] = stack[i];
            }

            var element = stack[stack.Length - 1];

            stack = newStack;
            return element;
        }

        public bool Push(T obj)
        {
            if (stack.Length >= Limit && hasLimit)
            {
                Console.WriteLine(LimitErrorMassage);
                return false;
            }
            var newQueue = new T[stack.Length + 1];

            for (int i = 0; i < stack.Length; i++)
            {
                newQueue[i] = stack[i];
            }

            newQueue[stack.Length] = obj;

            stack = newQueue;
            return true;
        }

    }


    public class LinkedStack<T>
    {

        public Node<T> Head { get; set; }
        public int Count { get; set; }

        public LinkedStack()
        {
            Head = null;
            Count = 0;
        }

        public LinkedStack(T data)
        {
            var item = new Node<T>(data);
            Head = item;
            Count = 1;
        }

        public void Push(T data)
        {
            var item = new Node<T>(data);
            item.Previous = Head;
            Head = item;
            Count++;
        }
        public T Pop()
        {
            var item = Head;
            Head = Head.Previous;
            Count--;
            return item.Data;
        }

        public T Peek()
        {
            return Head.Data;
        }
    }


    public class PostfixExpressions
    {
        private LinkedStack<char> operatorsStack;
        private LinkedStack<float> quantityStack;

        public float Calculate(string input)
        {
            operatorsStack = new LinkedStack<char>();
            quantityStack = new LinkedStack<float>();
            return Counting(GetExpression(input));
        }
        private int GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                default: return 5;
            }
        }


        private string GetExpression(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (IsGap(input[i])) continue;

                if (IsQuantity(input, i))
                {

                    while (!IsGap(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    output += " ";
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operatorsStack.Push(input[i]);
                    else if (input[i] == ')')
                    {
                        char s = operatorsStack.Pop();
                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operatorsStack.Pop();
                        }
                    }
                    else
                    {
                        if (operatorsStack.Count > 0 && GetPriority(input[i]) <= GetPriority(operatorsStack.Peek()))
                        {
                            output += operatorsStack.Pop().ToString() + " ";
                        }
                        operatorsStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }
            while (operatorsStack.Count > 0)
            {
                output += operatorsStack.Pop() + " ";
            }
            return output;
        }

        private bool IsGap(char c)
        {
            return (" ".IndexOf(c) != -1);
        }
        private bool IsOperator(char с)
        {
            return ("+-/*()".IndexOf(с) != -1);
        }
        private static bool IsQuantity(string input, int i)
        {
            return Char.IsDigit(input[i]);
        }

        private float Counting(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    string a = "";
                    while (!IsGap(input[i]) && !IsOperator(input[i]))
                    {
                        a += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    quantityStack.Push(float.Parse(a));
                    i--;
                }
                else if (IsOperator(input[i]))
                {
                    float a = quantityStack.Pop();
                    float b = quantityStack.Pop();
                    quantityStack.Push(CalculateOperation(input, i, a, b));
                }
            }
            return quantityStack.Peek();
        }

        private static float CalculateOperation(string input, int i, float a, float b)
        {
            float result = 0f;
            switch (input[i])
            {
                case '+':
                    result = b + a;
                    break;
                case '-':
                    result = b - a;
                    break;
                case '*':
                    result = b * a;
                    break;
                case '/':
                    result = b / a;
                    break;
            }

            return result;
        }
    }
}
