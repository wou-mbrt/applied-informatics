using System;
using System.Collections.Generic;

namespace BinaryTrees
{
    public class TestingSystem
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree(
                new Node[] {
                    new Node(0, 1, 2, "a"),
                    new Node(1, 3, 4, "b"),
                    new Node(3, -1, -1, "d"),
                    new Node(2, -1, 5, "c"),
                    new Node(4, -1, -1, "e"),
                    new Node(5, -1, -1, "f")
                });
            tree.ShowTree(showNames: true);
            int[] idInForwardOrder = tree.GetIdInForwardOrder();
            int[] idInReverseOrder = tree.GetIdInReverseOrder();
            int[] idInEndOrder = tree.GetIdInEndOrder();

            Console.Write("\nForward Order: ");
            foreach (var item in idInForwardOrder)
            {
                Console.Write($"{tree.GetNode(item).nodeName} ");
            }
            Console.Write("\nReverse Order: ");
            foreach (var item in idInReverseOrder)
            {
                Console.Write($"{tree.GetNode(item).nodeName} ");
            }
            Console.Write("\nEnd Order: ");
            foreach (var item in idInEndOrder)
            {
                Console.Write($"{tree.GetNode(item).nodeName} ");
            }


            Console.Write("\n");
            MathTree mathTree = new MathTree(
                new Node[] {
                    new Node(0, 1, 2, "a", ENodeType.MathNode, EMathOperation.Division),
                    new Node(1, 3, 4, "b", ENodeType.MathNode, EMathOperation.Multiplication),
                    new Node(2, -1, -1, "c", ENodeType.NumNode, nodeNum: 3f),
                    new Node(3, 5, 6, "d", ENodeType.MathNode, EMathOperation.Sum),
                    new Node(4, 7, 8, "e", ENodeType.MathNode, EMathOperation.Subtraction),
                    new Node(5, -1, -1, "f", ENodeType.NumNode, nodeNum: 2f),
                    new Node(6, -1, -1, "g", ENodeType.NumNode, nodeNum: 3f),
                    new Node(7, -1, -1, "h", ENodeType.NumNode, nodeNum: 7f),
                    new Node(8, -1, -1, "i", ENodeType.NumNode, nodeNum: 4f)
                });
            mathTree.ShowTree();
            idInForwardOrder = mathTree.GetIdInEndOrder();

            Console.Write("\nEnd Order: ");
            foreach (var item in idInForwardOrder)
            {
                Node node = mathTree.GetNode(item);
                if (node.nodeType == ENodeType.MathNode)
                {
                    string outputNodeData = "None";
                    if (node.mathOperation == EMathOperation.Division)
                    {
                        outputNodeData = "/";
                    }
                    else if (node.mathOperation == EMathOperation.Multiplication)
                    {
                        outputNodeData = "*";
                    }
                    else if (node.mathOperation == EMathOperation.Subtraction)
                    {
                        outputNodeData = "-";
                    }
                    else if (node.mathOperation == EMathOperation.Sum)
                    {
                        outputNodeData = "+";
                    }

                    Console.Write($"{outputNodeData} ");
                }
                else if (node.nodeType == ENodeType.NumNode)
                {
                    Console.Write($"{node.nodeNum} ");

                }
                else
                {
                    Console.Write($"id:{node.nodeId} ");
                }
            }
            Console.Write($"\nCalculate: {mathTree.Calculate()}");
        }
    }

    public enum ENodeType
    {
        BaseNode,
        MathNode,
        NumNode
    }
    public enum EMathOperation
    {
        None,
        Sum,
        Multiplication,
        Division,
        Subtraction
    }
    public class Node
    {
        public Node(int nodeId, int leftNodeId, int rightNodeId, string nodeName,
            ENodeType nodeType = ENodeType.BaseNode, EMathOperation mathOperation = EMathOperation.None,
            float nodeNum = 0f)
        {
            this.nodeId = nodeId;
            this.leftNodeId = leftNodeId;
            this.rightNodeId = rightNodeId;
            this.nodeName = nodeName;
            this.nodeType = nodeType;
            this.nodeNum = nodeNum;
            this.mathOperation = mathOperation;
        }
        public string nodeName;
        public int nodeId;
        public int leftNodeId;
        public int rightNodeId;
        public bool visited;
        public ENodeType nodeType;
        public float nodeNum;
        public EMathOperation mathOperation;
        public int parentNodeId;
    }

    public class Tree
    {
        public Node headNode;
        public Dictionary<int, Node> tree
        {
            get;
            set;
        }

        private int nodesCount = 0;

        public Tree(Node[] nodes)
        {
            tree = new Dictionary<int, Node>();
            int nodeId = -1;
            for (int i = 0; i < nodes.Length; i++)
            {
                try
                {
                    if (!tree.ContainsKey(nodes[i].nodeId))
                    {
                        tree[nodes[i].nodeId] = nodes[i];
                        nodeId = nodes[i].nodeId;
                        nodesCount++;
                    }
                    else
                    {
                        throw new Exception($"Было обноружено два одинкаковых узла с id {nodes[i]}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                }
            }
            SetParents();
            FindHeadNode(nodeId);
        }

        private void FindHeadNode(int randomNodeId)
        {
            if (randomNodeId == -1)
            {
                return;
            }
            while (tree[randomNodeId].parentNodeId != -1)
            {
                randomNodeId = GetNode(randomNodeId).parentNodeId;
            }
            headNode = GetNode(randomNodeId);
        }

        public void ShowTree(bool showNames = true)
        {
            this.showNames = showNames;
            ShowNodes(headNode, "");
        }

        protected bool showNames;
        protected virtual void ShowNodes(Node node, string previousPadding)
        {
            Console.Write(previousPadding);
            var outputNodeData = showNames ? node.nodeName : node.nodeId.ToString();
            Console.Write(outputNodeData);
            if (node.leftNodeId != -1 || node.rightNodeId != -1)
            {
                int previousPaddingLength = previousPadding.Length;
                previousPadding = "";
                for (int i = 0; i < previousPaddingLength; i++)
                {
                    previousPadding += " ";
                }
                previousPadding += "|__";
            }
            if (node.leftNodeId != -1)
            {
                Console.Write("\n");
                ShowNodes(GetNode(node.leftNodeId), previousPadding);
            }
            if (node.rightNodeId != -1)
            {
                Console.Write("\n");
                ShowNodes(GetNode(node.rightNodeId), previousPadding);
            }
        }

        public int Count()
        {
            return nodesCount;
        }
        public void SetParents()
        {
            foreach (var key in tree.Keys)
            {
                tree[key].parentNodeId = -1;
            }
            foreach (var key in tree.Keys)
            {
                if (tree[key].leftNodeId != -1)
                {
                    try
                    {
                        if (tree.ContainsKey(tree[key].leftNodeId))
                        {
                            tree[tree[key].leftNodeId].parentNodeId = tree[key].nodeId;
                        }
                        else
                        {
                            throw new Exception($"Узел с id {tree[key].leftNodeId} не был назначен и указывается в качестве дочернего корня");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                    }
                }
                if (tree[key].rightNodeId != -1)
                {
                    try
                    {
                        if (tree.ContainsKey(tree[key].rightNodeId))
                        {
                            tree[tree[key].rightNodeId].parentNodeId = tree[key].nodeId;
                        }
                        else
                        {
                            throw new Exception($"Узел с id {tree[key].rightNodeId} не был назначен и указывается в качестве дочернего корня");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                    }
                }
            }
        }
        public Node GetNode(int id)
        {
            try
            {
                if (tree.ContainsKey(id))
                {
                    return tree[id];
                }
                else
                {
                    throw new Exception($"Не существует узла с id {id}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            return null;
        }

        public int[] GetIdInForwardOrder(bool startFromLeftWing = true)
        {
            int[] result = new int[this.Count()];
            UnvisitNodes();
            indexOfCount = 0;
            result = ForwardOrder(startFromLeftWing, result, headNode);
            return result;
        }

        private int indexOfCount;
        private int[] ForwardOrder(bool startFromLeftWing, int[] result, Node node)
        {
            result[indexOfCount] = node.nodeId;
            node.visited = true;
            if (startFromLeftWing)
            {
                if (node.leftNodeId != -1)
                {
                    indexOfCount++;
                    result = ForwardOrder(startFromLeftWing, result, GetNode(node.leftNodeId));
                }
                if (node.rightNodeId != -1)
                {
                    indexOfCount++;
                    result = ForwardOrder(startFromLeftWing, result, GetNode(node.rightNodeId));
                }
            }
            else
            {
                if (node.rightNodeId != -1)
                {
                    indexOfCount++;
                    result = ForwardOrder(startFromLeftWing, result, GetNode(node.rightNodeId));
                }
                if (node.leftNodeId != -1)
                {
                    indexOfCount++;
                    result = ForwardOrder(startFromLeftWing, result, GetNode(node.leftNodeId));
                }
            }
            return result;
        }

        private void UnvisitNodes()
        {
            foreach (var key in tree.Keys)
            {
                tree[key].visited = false;
            }
        }

        public int[] GetIdInEndOrder()
        {
            int[] result = new int[this.Count()];
            UnvisitNodes();
            indexOfCount = 0;
            result = EndOrder(result, headNode);
            return result;
        }

        private int[] EndOrder(int[] result, Node node)
        {

            if (node.leftNodeId != -1 && !GetNode(node.leftNodeId).visited)
            {
                result = EndOrder(result, GetNode(node.leftNodeId));
            }
            if ((node.leftNodeId != -1 && GetNode(node.leftNodeId).visited || node.leftNodeId == -1)
                && node.rightNodeId != -1 && !GetNode(node.rightNodeId).visited)
            {
                result = EndOrder(result, GetNode(node.rightNodeId));
            }

            if ((node.leftNodeId == -1 && node.rightNodeId == -1
               || node.leftNodeId != -1 && GetNode(node.leftNodeId).visited
               || node.rightNodeId != -1 && GetNode(node.rightNodeId).visited) && !node.visited)
            {
                result[indexOfCount] = node.nodeId;
                indexOfCount++;
                node.visited = true;
            }
            return result;
        }

        public int[] GetIdInReverseOrder()
        {
            int[] result = new int[this.Count()];
            UnvisitNodes();
            indexOfCount = 0;
            result = ReverseOrder(result, headNode);
            return result;
        }

        private int[] ReverseOrder(int[] result, Node node)
        {

            if (node.leftNodeId != -1 && !GetNode(node.leftNodeId).visited)
            {
                result = ReverseOrder(result, GetNode(node.leftNodeId));
            }


            if ((node.leftNodeId == -1 && node.rightNodeId == -1
               || node.leftNodeId != -1 && GetNode(node.leftNodeId).visited ||
               node.leftNodeId == -1) && !node.visited)
            {
                result[indexOfCount] = node.nodeId;
                indexOfCount++;
                node.visited = true;
            }
            if ((node.leftNodeId != -1 && GetNode(node.leftNodeId).visited || node.leftNodeId == -1)
                                && node.rightNodeId != -1 && !GetNode(node.rightNodeId).visited)
            {
                result = ReverseOrder(result, GetNode(node.rightNodeId));
            }
            return result;
        }
    }

    public class MathTree : Tree
    {
        public MathTree(Node[] nodes) : base(nodes)
        {
        }

        public float Calculate()
        {
            int[] idInForwardOrder = GetIdInEndOrder();
            if (idInForwardOrder.Length == 0)
            {
                return 0;
            }
            float resulty = 0f;
            for (int i = 1; i < idInForwardOrder.Length; i++)
            {
                Node node = GetNode(idInForwardOrder[i]);
                if (node.nodeType == ENodeType.MathNode)
                {
                    try
                    {
                        Node leftNode = GetNode(node.leftNodeId);
                        Node rightNode = GetNode(node.rightNodeId);
                        if (node.mathOperation == EMathOperation.Division)
                        {
                            node.nodeNum = leftNode.nodeNum / rightNode.nodeNum;
                        }
                        else if (node.mathOperation == EMathOperation.Multiplication)
                        {
                            node.nodeNum = leftNode.nodeNum * rightNode.nodeNum;
                        }
                        else if (node.mathOperation == EMathOperation.Subtraction)
                        {
                            node.nodeNum = leftNode.nodeNum - rightNode.nodeNum;
                        }
                        else if (node.mathOperation == EMathOperation.Sum)
                        {
                            node.nodeNum = leftNode.nodeNum + rightNode.nodeNum;
                        }
                        resulty = node.nodeNum;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                        return 0f;
                    }
                }
            }
            return resulty;
        }
        protected override void ShowNodes(Node node, string previousPadding)
        {
            Console.Write(previousPadding);
            string outputNodeData;
            if (node.nodeType == ENodeType.BaseNode)
            {
                outputNodeData = node.nodeId.ToString();
            }
            else if (node.nodeType == ENodeType.MathNode)
            {
                if (node.mathOperation == EMathOperation.Division)
                {
                    outputNodeData = "/";
                }
                else if (node.mathOperation == EMathOperation.Multiplication)
                {
                    outputNodeData = "*";
                }
                else if (node.mathOperation == EMathOperation.Subtraction)
                {
                    outputNodeData = "-";
                }
                else if (node.mathOperation == EMathOperation.Sum)
                {
                    outputNodeData = "+";
                }
                else
                {
                    outputNodeData = "None";
                }
            }
            else
            {
                outputNodeData = node.nodeNum.ToString();
            }
            Console.Write(outputNodeData);
            if (node.leftNodeId != -1 || node.rightNodeId != -1)
            {
                int previousPaddingLength = previousPadding.Length;
                previousPadding = "";
                for (int i = 0; i < previousPaddingLength; i++)
                {
                    previousPadding += " ";
                }
                previousPadding += "|__";
            }
            if (node.leftNodeId != -1)
            {
                Console.Write("\n");
                ShowNodes(GetNode(node.leftNodeId), previousPadding);
            }
            if (node.rightNodeId != -1)
            {
                Console.Write("\n");
                ShowNodes(GetNode(node.rightNodeId), previousPadding);
            }
        }
    }

}
