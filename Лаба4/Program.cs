using System;
using System.Collections.Generic;
using System.Linq;

namespace Лаба_4
{
    class Program
    {
        class GraphSearch
        {
            /// <summary>
            /// Число графов
            /// </summary>
            private int Count;
            /// <summary>
            /// Массив вершин
            /// </summary>
            private LinkedList<int>[] Node;
            /// <summary>
            /// Конструктор класса
            /// </summary>
            /// <param name="Count"></param>
            public GraphSearch(int Count)
            {
                this.Count = Count;
                Node = new LinkedList<int>[Count];
                for (int i = 0; i < Count; ++i)
                    Node[i] = new LinkedList<int>();
            }
            /// <summary>
            /// Добавление пути
            /// </summary>
            /// <param name="Count"></param>
            /// <param name="w"></param>
            public void AddEdge(int first, int second)
            {
                Node[first].AddLast(second);
            }
            /// <summary>
            /// Поиск по глубине
            /// </summary>
            /// <param name="num"></param>
            /// <param name="visited"></param>
            void DeepFirstSearch(int num, bool[] visited)
            {
                visited[num] = true;
                Console.Write(num + " ");
                LinkedList<int> vList = Node[num];
                foreach (var i in vList)
                {
                    if (!visited[i])
                        DeepFirstSearch(i, visited);
                }
            }
            /// <summary>
            /// Начальная функция поиска в глубину
            /// </summary>
            /// <param name="Count"></param>
            public void DeepFirstSearch(int Count)
            {
                bool[] visited = new bool[this.Count];
                DeepFirstSearch(Count, visited);
            }
            /// <summary>
            /// Функция поиска в глубину
            /// </summary>
            /// <param name="s"></param>
            public void BreadthFirstSearch(int s)
            {
                bool[] visited = new bool[Count];
                for (int i = 0; i < Count; i++)
                    visited[i] = false;
                LinkedList<int> queue = new LinkedList<int>();
                visited[s] = true;
                queue.AddLast(s);
                while (queue.Any())
                {
                    s = queue.First();
                    Console.Write(s + " ");
                    queue.RemoveFirst();
                    LinkedList<int> list = Node[s];
                    foreach (var val in list)
                    {
                        if (!visited[val])
                        {
                            visited[val] = true;
                            queue.AddLast(val);
                        }
                    }
                }
            }
        }
        class Dijkstra
        {
            /// <summary>
            /// Количество вершин
            /// </summary>
            static int Count = 8;
            /// <summary>
            /// Поиск минимального расстояния
            /// </summary>
            /// <param name="dist"></param>
            /// <param name="sptSet"></param>
            /// <returns></returns>
            int minDistance(int[] dist, bool[] sptSet)
            {
                int minValue = int.MaxValue, minIndex = -1;
                for (int i = 0; i < Count; i++)
                    if (sptSet[i] == false && dist[i] <= minValue)
                    {
                        minValue = dist[i];
                        minIndex = i;
                    }
                return minIndex;
            }
            /// <summary>
            /// Вывод значений
            /// </summary>
            /// <param name="dist"></param>
            void printSolution(int[] dist)
            {
                Console.Write("Вершина \t Расстояние от источника\n");
                for (int i = 0; i < Count; i++)
                    Console.Write(i + " \t\t " + dist[i] + "\n");
            }
            /// <summary>
            /// Алгоритм Дейкстры
            /// </summary>
            /// <param name="graph"></param>
            /// <param name="src"></param>
            public void DijkstraAlgorithm(int[,] graph, int src)
            {
                int[] dist = new int[Count];
                bool[] sptSet = new bool[Count];
                for (int i = 0; i < Count; i++)
                {
                    dist[i] = int.MaxValue;
                    sptSet[i] = false;
                }
                dist[src] = 0;
                for (int count = 0; count < Count - 1; count++)
                {
                    int u = minDistance(dist, sptSet);
                    sptSet[u] = true;

                    for (int v = 0; v < Count; v++)
                        if (!sptSet[v] && graph[u, v] != 0 &&
                            dist[u] != int.MaxValue &&
                            dist[u] + graph[u, v] < dist[v])
                            dist[v] = dist[u] + graph[u, v];
                }
                printSolution(dist);
            }
        }
        class Kruskals
        {
            public class Edge : IComparable<Edge>
            {
                public int src, dest, weight;
                public int CompareTo(Edge compareEdge)
                {
                    return this.weight - compareEdge.weight;
                }
            }
            public class subset
            {
                public int parent, rank;
            };
            int V, E;
            public Edge[] edge;
            public Kruskals(int v, int e)
            {
                V = v;
                E = e;
                edge = new Edge[E];
                for (int i = 0; i < e; ++i)
                    edge[i] = new Edge();
            }
            int find(subset[] subsets, int i)
            {
                if (subsets[i].parent != i)
                    subsets[i].parent = find(subsets, subsets[i].parent);

                return subsets[i].parent;
            }
            void Union(subset[] subsets, int x, int y)
            {
                int xroot = find(subsets, x);
                int yroot = find(subsets, y);
                if (subsets[xroot].rank < subsets[yroot].rank)
                    subsets[xroot].parent = yroot;
                else if (subsets[xroot].rank > subsets[yroot].rank)
                    subsets[yroot].parent = xroot;
                else
                {
                    subsets[yroot].parent = xroot;
                    subsets[xroot].rank++;
                }
            }

            public void KruskalAlgorithm()
            {
                Edge[] result = new Edge[V];
                int e = 0;
                int i = 0;
                for (i = 0; i < V; ++i)
                    result[i] = new Edge();

                Array.Sort(edge);

                subset[] subsets = new subset[V];
                for (i = 0; i < V; ++i)
                    subsets[i] = new subset();

                for (int v = 0; v < V; ++v)
                {
                    subsets[v].parent = v;
                    subsets[v].rank = 0;
                }

                i = 0;
                while (e < V - 1)
                {
                    Edge next_edge = new Edge();
                    next_edge = edge[i++];
                    int x = find(subsets, next_edge.src);
                    int y = find(subsets, next_edge.dest);

                    if (x != y)
                    {
                        result[e++] = next_edge;
                        Union(subsets, x, y);
                    }
                }

                Console.WriteLine("Минимальное остовное дерево:");
                int minimumCost = 0;
                for (i = 0; i < e; ++i)
                {
                    Console.WriteLine(result[i].src + " -- "
                                      + result[i].dest
                                      + " == " + result[i].weight);
                    minimumCost += result[i].weight;
                }

                Console.WriteLine("Минимальный вес остовного дерева:"
                                  + minimumCost);
            }
        }
        public static void Main(String[] args)
        {
            Console.WriteLine("Задание 1");
            GraphSearch g = new GraphSearch(9);
            g.AddEdge(0, 1); g.AddEdge(1, 2);
            g.AddEdge(2, 4); g.AddEdge(0, 4);
            g.AddEdge(4, 5); g.AddEdge(0, 3);
            g.AddEdge(3, 5); g.AddEdge(5, 6);
            g.AddEdge(5, 7); g.AddEdge(7, 2);
            g.AddEdge(6, 8); g.AddEdge(2, 8);
            g.AddEdge(1, 0); g.AddEdge(2, 1);
            g.AddEdge(4, 2); g.AddEdge(4, 0);
            g.AddEdge(5, 4); g.AddEdge(3, 0);
            g.AddEdge(5, 3); g.AddEdge(6, 5);
            g.AddEdge(7, 5); g.AddEdge(2, 7);
            g.AddEdge(8, 6); g.AddEdge(8, 2);
            Console.WriteLine("Поиск в глубину, начиная с вершины 0");
            g.DeepFirstSearch(0);
            Console.Write("\n Поиск в ширину, начиная с вершины 0 \n");
            g.BreadthFirstSearch(0);

            Console.WriteLine("\n");

            Console.WriteLine("Задание 2");
            int[,] graph = new int[,] {
                { 0, 6, 17, 11, 0, 0, 0, 0 },
                { 6, 0, 0, 25, 0, 0, 0, 19 },
                { 17, 0, 9, 0, 0, 0, 0, 0 },
                { 11, 25, 8, 0, 2, 0, 0, 0 },
                { 0, 0, 0, 2, 0, 21, 14, 0 },
                { 0, 0, 0, 0, 21, 0, 0, 0 },
                { 0, 0, 0, 0, 14, 0, 0, 9 },
                { 0, 19, 0, 0, 0, 0, 9, 0}
            };
            Dijkstra t = new Dijkstra();
            Console.WriteLine("Алгоритм Дейкстры для вершины 0");
            t.DijkstraAlgorithm(graph, 0);

            Console.WriteLine();
            Console.WriteLine("Задание 3");
            int V = 9;
            int E = 12;
            Kruskals kruskals = new Kruskals(V, E);
            kruskals.edge[0].src = 0;
            kruskals.edge[0].dest = 1;
            kruskals.edge[0].weight = 2;

            kruskals.edge[1].src = 0;
            kruskals.edge[1].dest = 4;
            kruskals.edge[1].weight = 5;

            kruskals.edge[2].src = 0;
            kruskals.edge[2].dest = 3;
            kruskals.edge[2].weight = 3;

            kruskals.edge[3].src = 1;
            kruskals.edge[3].dest = 2;
            kruskals.edge[3].weight = 10;

            kruskals.edge[4].src = 2;
            kruskals.edge[4].dest = 4;
            kruskals.edge[4].weight = 2;

            kruskals.edge[5].src = 2;
            kruskals.edge[5].dest = 7;
            kruskals.edge[5].weight = 5;

            kruskals.edge[6].src = 2;
            kruskals.edge[6].dest = 8;
            kruskals.edge[6].weight = 6;

            kruskals.edge[7].src = 3;
            kruskals.edge[7].dest = 5;
            kruskals.edge[7].weight = 8;

            kruskals.edge[8].src = 4;
            kruskals.edge[8].dest = 5;
            kruskals.edge[8].weight = 7;

            kruskals.edge[9].src = 5;
            kruskals.edge[9].dest = 7;
            kruskals.edge[9].weight = 3;

            kruskals.edge[10].src = 5;
            kruskals.edge[10].dest = 6;
            kruskals.edge[10].weight = 4;

            kruskals.edge[11].src = 6;
            kruskals.edge[11].dest = 8;
            kruskals.edge[11].weight = 11;

            kruskals.KruskalAlgorithm();
        }
    }
}