using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekThreeAssignmentTextFileAndLinkList
{
    public class DoubleLinkedList <T>
    {
        //Creates the Linked Lists of the Beginning and End
        public DoubleLinkedListNode<T>? First { get; private set; }
        public DoubleLinkedListNode<T>? Last { get; private set; }
        public int Count { get; private set; }

        public void Add(T value)
        {
            //Creates nodes and updates what goes next or previously
            var newNode = new DoubleLinkedListNode<T>(value);
            if (First == null)
            {
                First = newNode;
                Last = newNode;
            }
            else
            {
                Last.Next = newNode;
                newNode.Previous = Last;
                Last = newNode;
            }
            Count++;
        }

        public void Remove(DoubleLinkedListNode<T> node)
        {
            //Checks the node input and will remove nodes whether going to the next one or goes back to one
            if (node == null)
                return;

            if (node == First)
                First = node.Next;

            if (node == Last)
                Last = node.Previous;

            if (node.Previous != null)
                node.Previous.Next = node.Next;

            if (node.Next != null)
                node.Next.Previous = node.Previous;
        }
        public DoubleLinkedListNode<T> Find(T value)
        {
            //Finds the First Node then goes through the list to find the specific node 
            var current = First;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                    return current;
                current = current.Next;
            }
            return null;
        }

        public IEnumerable<T> AsEnumerable()
        {
            //Shows the values within the current Node and will move on to the next one
            var current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }
}
