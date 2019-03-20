using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ConsoleApp3
{
    public static class _123
    {
        public static void function(int a)
        {
            Console.WriteLine(a);
        }
    }
    public class Tree
    {
        public Dictionary<Tree, int> children = new Dictionary<Tree, int>();
        public int index;
        public int number = 0;
        public HashSet<Tree> ends = new HashSet<Tree>() { };

        public Tree parent;
        public void ApplyEnds()
        {
            if (parent?.index != null)
            {
                parent.ends.UnionWith(ends);
                parent.ApplyEnds();
            }
        }
        void RemoveEnds(HashSet<Tree> endsToRemove)
        {
            foreach (Tree end in endsToRemove)
            {
                ends.Remove(end);
            }
            parent?.RemoveEnds(endsToRemove);
        }
        public Tree Addchild(int amount, Tree child)
        {
            
            if (children == null) { children = new Dictionary<Tree, int>(); }
            foreach (Tree child1 in children.Keys)
            {
                if (child == child1)
                {
                    if (-amount == children[child1])
                    {
                        Removechild(child1);
                        return null;
                    }
                    else
                    {
                        children[child1] += amount;
                    }
                    return child1;
                }
            }
            if (ends != null)
            {
                ends.UnionWith(child.ends);
                ApplyEnds();
            }
            children[child] = amount;
            child.parent = this;
            return child;
        }
        public void SetParent(Tree P)
        {
            bool inkeys = false;
            P.ends.UnionWith(ends);
            P.ApplyEnds();
            foreach (Tree child in P.children.Keys)
            {
                if (child == this)
                {
                    P.children[child] += 1;
                    inkeys = true;
                    break;
                }
            }
            if (!inkeys) { P.children[this] = 1; }
            parent = P;
        }
        public void Removechild(Tree child)
        {
            children.Remove(child);
            RemoveEnds(child.ends);
        }
        public HashSet<Tree> Decrease(int amount, Tree child)
        {
            if (children == null) { children = new Dictionary<Tree, int>(); }
            foreach (Tree child1 in children.Keys)
            {
                if (child == child1)
                {
                    if (-amount == children[child1])
                    {
                        Removechild(child1);
                    }
                    else
                    {
                        children[child1] += amount;
                    }
                    return child1.ends;
                }
            }
            Tree chld = new Tree(child);
            children[chld] = amount;
            chld.parent = this;
            if (ends != null)
            {
                ends.UnionWith(chld.ends);
                ApplyEnds();
            }
            return new HashSet<Tree>();
        }

        public Tree AddNeighbour(int amount, Tree neighbour)
        {
            return parent.Addchild(amount, neighbour);
        }
        public Dictionary<Tree, int> Neighbours()
        {
            return (parent.children);
        }
        public Tree() { }
        public Tree(Tree tree)
        {
            index = tree.index;
            number = tree.number;
            if (tree.children.Count == 0)
            {
                ends.Add(this);
            }
            foreach(Tree child in tree.children.Keys)
            {
                Addchild(tree.children[child],new Tree(child));
            }
        }
        public static bool operator ==(Tree c1, Tree c2)
        {
            HashSet<Tree> used=new HashSet<Tree>();// Помогает ли это скорости?
            bool equal=true;
            /*if(c1?.index==null ^ c2?.index == null)
            {
                return false;
            }*/
            if (c1?.index != c2?.index)
            {
                return false;
            }
            else if(c1?.index==null)
            {
                return true;
            }
            else if (c1.number != c2.number) { return false; }
            else if (c1.children == null && c2.children == null)
            {
                return true;
            }
            else if (c1.children.Count == c2.children.Count)
            {
                foreach (Tree child1 in c1.children.Keys)
                {
                    equal = false;
                    foreach (Tree child2 in c2.children.Keys)
                    {
                        if (!used.Contains(child2) && c1.children[child1] == c2.children[child2] && child1 == child2)
                        {
                            used.Add(child2);
                            equal = true;
                            break;
                        }
                    }
                    if (!equal) { return false; }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Tree c1, Tree c2)
        {
            return !(c1 == c2);
        }
    }
    public class prog
    {
        public List<string> s = new List<string>();
        public int Length = 0;
        public static prog operator +(prog c1, string c2)
        {
            c1.s[c1.s.Count - 1] += c2;
            c1.Length += c2.Length;
            return c1;
        }
        public static prog operator +(prog c1, char c2)
        {
            c1.s[c1.s.Count - 1] += c2;
            c1.Length ++;
            return c1;
        }
        public static prog operator *(prog c1, string c2)
        {
            c1.s.Add(c2);
            c1.Length += c2.Length;
            return c1;
        }
        public char Last()
        {
            return s[s.Count - 1][s[s.Count - 1].Length - 1];
        }
        public prog Subprog(int i)
        {
            return new prog { s=s.GetRange(0, i) };

        }
    }

}
namespace Overby.Collections
{
    public class TreeNode<T>
    {
        private readonly T _value;
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            _value = value;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode<T> Parent { get; private set; }

        public T Value { get { return _value; } }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }
}