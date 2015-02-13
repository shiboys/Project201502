using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicKnowledge
{
    /*
     GetEnumerator 方法期望返回的类型是IEmumerator,而 yield return 返回的是string类型的值。
     * 其内部机制是 编译器生成一个状态机(state machine)来维护其迭代器的状态，foreach每次循环
     * 调用MoveNext方法时，都能保证是从前一个yield return语句停止的地方开始执行。
     * yield return 并没有专门的IL指令，编译器遇到该语句，会产生一个实现了IEnumerator(T Current.bool MoveNext,Reset())接口的嵌套类。
     * 
     */
    class Yield_Return_Test
    {
        public static void Test_Yield_Return()
        {
            UserInfo userInfo = new UserInfo();
            foreach (var name in userInfo)
            {
                Console.WriteLine(name);
            }

            Persons ps=new Persons(new string[]{"shi","weijie"});
            foreach (var person in ps)
            {
                Console.WriteLine(person);
            }
        }

        public static void Test_yieldReturn_BinarySearchTree()
        {
            Node<string> root = new Node<string>("A", null, null);
            Node<string> leftNode=new Node<string>("B",new Node<string>("D",null,null), null);
            Node<string> rightNode = new Node<string>("C", new Node<string>("E",null,null), new Node<string>("F",null,null));

            root.m_leftNode = leftNode;
            root.m_rightNode = rightNode;

            Search_Binary_Tree<string> sbt=new Search_Binary_Tree<string>(root);
            foreach (var item in sbt.Order)
            {
                Console.Write("{0}\t", item);
            }

            //sbt.Search(root);// D B A E C F

        }

    }

    public class UserInfo :IEnumerable
    {
        private string[] users = {"张三","李四","王五"};



        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < users.Length; i++)
            {
                yield return users[i];
            }
            yield break;
        }
    }

    public class Persons : IEnumerable
    {
        private string[] _names;

        public Persons(string[] names)
        {
            _names = new string[names.Length];
            Array.Copy(names,_names,names.Length);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var name in _names)
            {
                yield return name;
            }
        }

    }

    /// <summary>
    /// 二叉树的节点。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        public T m_item;
        public Node<T> m_leftNode;
        public Node<T> m_rightNode;

        public Node(T item, Node<T> leftNode, Node<T> rightNode)
        {
            m_item = item;
            m_leftNode = leftNode;
            m_rightNode = rightNode;
        }
    }

    /*递归遍历二叉树*/
    public class Search_Binary_Tree<T>
    {
        public Node<T> _root;

        public Search_Binary_Tree(Node<T> root)
        {
            _root = root;
        }

        public IEnumerable<T> Order
        {
            get { return Search(_root); }
        }

        private IEnumerable<T> Search(Node<T> root)
        {
            if (root != null)
            {
                if (root.m_leftNode != null)
                {
                    foreach (var node in Search(root.m_leftNode))
                    {
                        yield return node;
                    }    
                }
                yield return root.m_item;

                if (root.m_rightNode != null)
                {
                    foreach (var node in Search(root.m_rightNode))
                    {
                        yield return node;
                    }
                }

            }
             
        }
    }
}
