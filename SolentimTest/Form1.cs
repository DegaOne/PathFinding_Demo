using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolentimTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Pathfinding graphs
        struct MinPriorityQueue {
            public List<GraphEdge> m_edges;
            public List<int> m_costs;

            public GraphEdge Add(GraphEdge edge, int cost) {
                m_edges.Add(edge);
                m_costs.Add(cost);

                return edge;
            }

            public GraphEdge Pop() {
                //Pop the one with lowest priority
                int lowestPriority = int.MaxValue;
                int index = -1;
                for (int i = 0; i < m_edges.Count; i++) {
                    //Size should be the same as the cost list.
                    if (m_costs[i] < lowestPriority)
                    {
                        lowestPriority = m_costs[i];
                        index = i;
                    }
                }
                GraphEdge returnedEdge = m_edges[index];
                m_edges.RemoveAt(index);
                m_costs.RemoveAt(index);
                return returnedEdge;
            }
        }
        struct GraphEdge {

            public GraphEdge(int from, int to) {
                m_from = from;
                m_to = to;
            }
            public int m_from;//Connected nodes
            public int m_to;
            //public float m_cost;//Not used
        }
        struct GraphNode {

            public GraphNode(int index) {
                m_index = index;
                m_edges = new List<GraphEdge>();
            }
            public int m_index;
            public List<GraphEdge> m_edges;//adjacency list
        }
        struct NavGraph {

            public List<int> CalculateDFS(int sourceNode, int targetNode) {
                //@Breadth first search, inefficient to search
                List<int> m_path = new List<int>();
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        m_path.Add(-1);//= null value
                    }
                }
                List<bool> m_visited = new List<bool>();
                for (int y = 0; y < 64; y++) {
                    for (int x = 0; x < 64; x++) {
                        m_visited.Add(false);
                    }
                }
               Stack<GraphEdge> m_graphEdges = new Stack<GraphEdge>();

                //Add all adjacent to the source node and mark as visited
                m_visited[sourceNode] = true;
                foreach(GraphEdge edge in m_nodes[sourceNode].m_edges) {
                    m_graphEdges.Push(edge);
                }

                //While there are elements in our stack
                while (m_graphEdges.Count > 0) {
                    GraphEdge edge = m_graphEdges.Pop();
                    m_path[edge.m_to] = edge.m_from;
                    m_visited[edge.m_to] = true;
                    if (edge.m_to == targetNode)
                        return m_path;
                    else {
                        //Add all new adjacent edges to the stack
                        foreach (GraphEdge edges in m_nodes[edge.m_to].m_edges) {
                            if (m_visited[edges.m_to] == false) m_graphEdges.Push(edges);
                        }
                    }
                }
                //If we got here, there is no path;
                return null;
            }

            public List<int> CalculateBFS(int sourceNode, int targetNode)
            {
                //@Depth first search, unlikely to find best path
                List<int> m_path = new List<int>();
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        m_path.Add(-1);//= null value
                    }
                }
                List<bool> m_visited = new List<bool>();
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        m_visited.Add(false);
                    }
                }
                Queue<GraphEdge> m_graphEdges = new Queue<GraphEdge>();

                //Add all adjacent to the source node and mark as visited
                m_visited[sourceNode] = true;
                foreach (GraphEdge edge in m_nodes[sourceNode].m_edges)
                {
                    m_graphEdges.Enqueue(edge);
                }

                uint iteration = 0;
                //While there are elements in our stack
                while (m_graphEdges.Count > 0)
                {
                    iteration++;
                    GraphEdge edge = m_graphEdges.Dequeue();
                    m_path[edge.m_to] = edge.m_from;
                    m_visited[edge.m_to] = true;
                    if (edge.m_to == targetNode)
                        return m_path;
                    else
                    {
                        //Add all new adjacent edges to the stack
                        foreach (GraphEdge edges in m_nodes[edge.m_to].m_edges)
                        {
                            if ((m_visited[edges.m_to] == false) && !m_graphEdges.Contains(edges)) m_graphEdges.Enqueue(edges);
                        }
                    }
                }
                //If we got here, there is no path;
                return null;
            }

            //@NOT WORKING CORRECTLY.
            public List<int> CalculateAStar(int sourceNode, int targetNode) {
                //@A*
                List<int> m_path = new List<int>();
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        m_path.Add(-1);//= null value
                    }
                }

                List<int> m_costs = new List<int>();
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        m_costs.Add(int.MaxValue);
                    }
                }


                List<GraphEdge> m_alreadyTraversed = new List<GraphEdge>();

                MinPriorityQueue m_mpq = new MinPriorityQueue();
                m_mpq.m_edges = new List<GraphEdge>();
                m_mpq.m_costs = new List<int>();

                m_costs[sourceNode] = 0;
                //@Calculate only once
                int targetX = targetNode % 64;
                int targetY = targetNode / 64;

                int sourceX = sourceNode % 64;
                int sourceY = sourceNode / 64;

                //Add all adjacent nodes to the source, to the min priority queue
                
                for (int i = 0; i < m_nodes[sourceNode].m_edges.Count; i++)
                {
                    GraphEdge edge = m_nodes[sourceNode].m_edges[i];
                    //Input manhattan heuristic cost
                    int nodeX = edge.m_to % 64;
                    int nodeY = edge.m_to / 64;

                    int heuristicCost = Math.Abs(nodeX - targetX) + Math.Abs(nodeY - targetY);
                    m_mpq.Add(edge, 1 + heuristicCost);//All costs are initially one (Grid based solution)
                }
                bool TargetNodeFound = false;
                while (m_mpq.m_edges.Count > 0) {
                    //Pop element from MPQueue and put into traversedEdges list
                    GraphEdge curEdge = m_mpq.Pop();
                    m_alreadyTraversed.Add(curEdge);

                    //Check if the cost of the node this edge leads to is greater than the cost of the previous node plus the cost of the edge
                    if (m_costs[curEdge.m_to] > m_costs[curEdge.m_from] + 1) {
                        m_path[curEdge.m_to] = curEdge.m_from;
                        m_costs[curEdge.m_to] = m_costs[curEdge.m_from] + 1;
                        if (targetNode == curEdge.m_to)
                        {
                            TargetNodeFound = true;//Not ideal to terminate loop YET
                            break;
                        }
                        else {
                            //Add all adjacent edges to the queue using a for loop
                            GraphNode curNode = m_nodes[curEdge.m_to];
                            for (int i = 0; i < curNode.m_edges.Count; i++) {
                                //If the edge is on the already traversed queue or the min-priority queue then do not add
                                if (m_alreadyTraversed.Contains(curNode.m_edges[i]))
                                {
                                    continue;
                                }
                                if (m_mpq.m_edges.Contains(curNode.m_edges[i]))
                                {
                                    continue;
                                }
                                //Otherwise add to the queue and set its priority as the current node's cost plus the cost of this adjacent edge
                                //In our case all edges cost 1.
                                //Input manhattan heuristic cost
                                int nodeX = curNode.m_edges[i].m_to % 64;
                                int nodeY = curNode.m_edges[i].m_to / 64;

                                int heuristicCost = Math.Abs(nodeX - targetX) + Math.Abs(nodeY - targetY);
                                m_mpq.Add(curNode.m_edges[i], m_costs[curNode.m_index] + 1 + heuristicCost);
                            }
                        }
                    }

                }
                if (TargetNodeFound) return m_path;
                else return null;
            }

            //Variables
            public List<GraphNode> m_nodes;
        }


        NavGraph m_graph;
        Graphics m_graphics = null;
        uint[,] m_map = new uint[64, 64];
        int m_startPosX = 1;
        int m_startPosY = 32;
        int m_endPosX = 63;
        int m_endPosY = 25;
        //
        private void m_placeButton_Click(object sender, EventArgs e)
        {
            //@Place object at position on map 2d array;
            //@Whenever we place obstacles, we clear the graph
            GenerateGraph();
            m_map[(int)m_posX.Value, (int)m_posY.Value] = 1;
            m_panel.Refresh();
        }

        private void m_panel_Paint(object sender, PaintEventArgs e)
        {
            m_graphics = m_panel.CreateGraphics();
            int width = m_panel.Width;
            int height = m_panel.Height;
            //Draw errthing all over again, looping from Map Array
            for (uint y = 0; y < 64; y++) {
                for (uint x = 0; x < 64; x++) {
                    switch (m_map[x, y]) {
                        case 0:
                            //Empty space
                            break;
                        case 1:
                            //Occupied, draw black rectangle
                            m_graphics.DrawRectangle(Pens.Black, x * width / 64, y * height / 64, 5, 5);
                            break;
                        case 2:
                            //Start pos, red
                            m_graphics.DrawRectangle(Pens.Red, x * width / 64, y * height / 64, 5, 5);
                            break;
                        case 3:
                            //End pos, green
                            m_graphics.DrawRectangle(Pens.Green, x * width / 64, y * height / 64, 5, 5);
                            break;
                        case 4:
                            //Explored pos, blue
                            m_graphics.DrawRectangle(Pens.Blue, x * width / 64, y * height / 64, 5, 5);
                            break;
                        case 5:
                            //Path followed pos, orange
                            m_graphics.DrawRectangle(Pens.Orange, x * width / 64, y * height / 64, 5, 5);
                            break;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Start and end positions
            m_map[m_startPosX, m_startPosY] = 2;
            m_map[m_endPosX, m_endPosY] = 3;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(m_posX.Value > 0)if (e.KeyChar == 'a') m_posX.Value--;
            if(m_posX.Value < 63)if (e.KeyChar == 'd') m_posX.Value++;
            if (m_posY.Value > 0)if (e.KeyChar == 'w') m_posY.Value--;
            if(m_posY.Value < 63)if (e.KeyChar == 's') m_posY.Value++;

        }

        public void GenerateGraph() {
            //@Generate graph
            m_graph.m_nodes = new List<GraphNode>();
            m_graph.m_nodes.Clear();
            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    //Repaint start and endPositions.
                    //Clear investigated or path bits
                    //Leave obstacles as they are
                    if (m_map[x, y] == 4 || m_map[x,y] == 5) m_map[x, y] = 0;
                    if (x == m_startPosX && y == m_startPosY) m_map[x, y] = 2;
                    else if (x == m_endPosX && y == m_endPosY) m_map[x, y] = 3;

                    //Each node has four possible neighbours, which could be out of bounds, or occupied.
                    GraphNode newNode = new GraphNode(x + y * 64);//index
                    if (y > 0) if (m_map[x, y - 1] != 1) newNode.m_edges.Add(new GraphEdge(newNode.m_index, newNode.m_index - 64));
                    if (y < 63) if (m_map[x, y + 1] != 1) newNode.m_edges.Add(new GraphEdge(newNode.m_index, newNode.m_index + 64));
                    if (x > 0) if (m_map[x - 1, y] != 1) newNode.m_edges.Add(new GraphEdge(newNode.m_index, newNode.m_index - 1));
                    if (x < 63) if (m_map[x + 1, y] != 1) newNode.m_edges.Add(new GraphEdge(newNode.m_index, newNode.m_index + 1));
                    m_graph.m_nodes.Add(newNode);
                }
            }
        }

        public void PaintPath(List<int> path) {
            if (path == null) MessageBox.Show("NO PATH!");
            else
            {

                //Paint all explored nodes, by converting them to 2d screen coords
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        int nodeInt = path[x + y * 64];
                        if (nodeInt != -1)
                        {
                            m_map[x, y] = 4;//Path bit
                        }
                    }
                }

                //@Now track down from end node, up to starting node, painting the followed upon path
                int endNodeFrom = path[m_endPosX + 64 * m_endPosY];
                m_map[m_endPosX, m_endPosY] = 5;
                int newNodeFrom = path[endNodeFrom];
                while (path[newNodeFrom] != (m_startPosX + 64 * m_startPosY))
                {
                    //Paint 2d map
                    m_map[newNodeFrom % 64, newNodeFrom / 64] = 5;
                    //Assign 1d array;
                    newNodeFrom = path[newNodeFrom];
                }
            }
        }
        private void m_DFSButton_Click(object sender, EventArgs e)
        {
            //Clear graph
            GenerateGraph();
            List<int> path = m_graph.CalculateDFS(m_startPosX + 64 * m_startPosY, m_endPosX + 64 * m_endPosY);
            PaintPath(path);
            m_panel.Refresh();
        }

        private void m_BFSButton_Click(object sender, EventArgs e)
        {
            GenerateGraph();
            List<int> path = m_graph.CalculateBFS(m_startPosX + 64 * m_startPosY, m_endPosX + 64 * m_endPosY);
            PaintPath(path);
            m_panel.Refresh();
        }

        private void m_AStarButton_Click(object sender, EventArgs e)
        {
            GenerateGraph();
            List<int> path = m_graph.CalculateAStar(m_startPosX + 64 * m_startPosY, m_endPosX + 64 * m_endPosY);
            PaintPath(path);
            m_panel.Refresh();
            
        }
    }
}
