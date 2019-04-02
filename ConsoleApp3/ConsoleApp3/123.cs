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
        public HashSet<Tree> children = new HashSet<Tree>();
        public HashSet<Tree> children2;
        public int index, NumberOfTrees=1;
        public int number = 0, number2=0;
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
        public Tree Addchild(int amount, Tree child, bool first=true )
        {
            HashSet<Tree> children3;
            if (first) { children3 = children; }
            else { children3 = children2; }
            if (children3 == null) { children3 = new HashSet<Tree>(); }
            foreach (Tree child1 in children3)
            {
                if (child == child1)
                {
                    if (-amount ==child1.NumberOfTrees)
                    {
                        Removechild(child1,first);
                        return null;
                    }
                    else
                    {
                        child1.NumberOfTrees += amount;
                    }
                    return child1;
                }
            }
            if (ends != null)
            {
                ends.UnionWith(child.ends);
                ApplyEnds();
            }
            child.NumberOfTrees = amount;
            children3.Add(child);
            child.parent = this;
            return child;
        }
        public void SetParent(Tree P)
        {
            bool inkeys = false;
            P.ends.UnionWith(ends);
            P.ApplyEnds();
            foreach (Tree child in P.children)
            {
                if (child == this)
                {
                    child.NumberOfTrees += 1;
                    inkeys = true;
                    break;
                }
            }
            if (!inkeys) { NumberOfTrees = 1; P.children.Add(this); }
            parent = P;
        }
        public void Removechild(Tree child, bool first=true)
        {
            if (first) { children.Remove(child); }
            else { children2.Remove(child); }
            RemoveEnds(child.ends);
            if (children.Count==0)
            {
                ends.Add(this);
                ApplyEnds();
            }
            
        }
        public HashSet<Tree> Decrease(int amount, Tree child, bool first=true)
        {
            HashSet<Tree> children3;
            if (first) { children3 = children; }
            else { children3 = children2; }
            if (children3 == null) { children3 = new HashSet<Tree>(); }
            foreach (Tree child1 in children3)
            {
                if (child == child1)
                {
                    if (-amount == child1.NumberOfTrees)
                    {
                        Removechild(child1,first);
                    }
                    else
                    {
                        child1.NumberOfTrees += amount;
                    }
                    return child1.ends;
                }
            }
            Tree chld = new Tree(child);
            children3.Add(chld);
            chld.NumberOfTrees = amount;
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
        public HashSet<Tree> Neighbours()
        {
            return (parent.children);
        }
        public Tree() { }
        public Tree(Tree tree)
        {
            index = tree.index;
            number = tree.number;
            number2 = tree.number2;
            NumberOfTrees = tree.NumberOfTrees;
            if (tree.children.Count == 0 && (tree.children2 == null || tree.children2.Count == 0))
            {
                ends.Add(this);
            }
            foreach(Tree child in tree.children)
            {
                Addchild(child.NumberOfTrees,new Tree(child));//HashSet<Tree>HashSet<Tree>HashSet<Tree>HashSet<Tree>HashSet<Tree>HashSet<Tree>HashSet<Tree>
            }
            foreach (Tree child in tree.children2 ?? Enumerable.Empty<Tree>())
            {
                Addchild(child.NumberOfTrees, new Tree(child), false);
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
            else if (c1.number != c2.number || c1.number2!=c2.number2) { return false; }
            else if (c1.children == null && c2.children == null && c2.children2==null && c1.children2==null)
            {
                return true;
            }
            if (c1.children.Count != c2.children.Count || c1.children2?.Count != c2.children2?.Count) { return false; }
            foreach (Tree child1 in c1.children)
            {
                equal = false;
                foreach (Tree child2 in c2.children)
                {
                    if (!used.Contains(child2) && child1.NumberOfTrees == child2.NumberOfTrees && child1 == child2)
                    {
                        used.Add(child2);
                        equal = true;
                        break;
                    }
                }
                if (!equal) { return false; }
            }
            foreach (Tree child1 in c1.children2 ?? Enumerable.Empty<Tree>())
            {
                equal = false;
                foreach (Tree child2 in c2.children2)
                {
                    if (!used.Contains(child2) && child1.NumberOfTrees == child2.NumberOfTrees && child1 == child2)
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
