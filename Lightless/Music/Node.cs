using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightless.Music
{
    class Node
    {
        public Node(ComplexNumber value)
        {
            Value = value;
        }

        /// <summary>
        /// Linked to the node after
        /// </summary>
        public Node NextNode
        {
            get;
            set;
        }

        /// <summary>
        /// Linked to the node before
        /// </summary>
        public Node PrevNode
        {
            get;
            set;
        }

        /// <summary>
        /// To mark end point
        /// </summary>
        public bool isEndPoint
        {
            get;
            set;
        }

        /// <summary>
        /// To mark start point
        /// </summary>
        public bool isStartPoint
        {
            get;
            set;
        }

        /// <summary>
        /// The Node's value
        /// </summary>
        public ComplexNumber Value
        {
            get;
            set;
        }
    }
}
