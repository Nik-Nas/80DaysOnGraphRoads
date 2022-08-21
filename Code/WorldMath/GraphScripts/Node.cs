using System.Collections.Generic;

namespace ITCampFinalProject.Code.WorldMath.GraphScripts
{
    public class Node
    {
        public int index { get; private set; }
        public HashSet<Node> attachedToNodes;   
    }
}