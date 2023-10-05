using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekThreeAssignmentTextFileAndLinkList
{
    public class DoubleLinkedListNode <T>
    {
        public T Value { get; set; }
        //This is for the linked list to go next or go back
        public DoubleLinkedListNode<T> Next { get; set; }
        public DoubleLinkedListNode<T> Previous { get; set; }

        public DoubleLinkedListNode(T value)
        {

            Value = value;
        }
    }
}
