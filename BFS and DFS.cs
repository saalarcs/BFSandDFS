// SAALAR FAISAL 
// THIS PROGRAM IS DESIGNED TO MODEL A SUBWAY MAP SYSTEM.
// THE METHODS ARE USED TO INSERT/REMOVE STATIONS AND CONNECTIONS,
// TO RETURN THE FASTER ROUTE FROM A GIVEN POINT A TO POINT B,
// AND TO SHOW THE CRTICIAL CONNECTIONS, THAT BREAK THE MAP INTO TWO PARTS.


using System;
using System.Collections.Generic;

namespace Maps
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the subway map
            SubwayMap S = new SubwayMap();

            //TEST 1: ADDING STATIONS
            S.InsertStation("toronto");
            S.InsertStation("montreal");
            S.InsertStation("vancouver");
            S.InsertStation("kingston");
            S.InsertStation("markham");
            S.InsertStation("quebec");
            S.InsertStation("halifax");
            S.InsertStation("whitehorse");
            S.InsertStation("peterborough");
            S.InsertStation("oakville");
            S.InsertStation("niagra");
            S.InsertStation("sudbury");
            S.InsertStation("saskatoon");
            S.InsertStation("yellowknife");
            S.InsertStation("calgary");

            //TEST 2: ADDING CONNECTIONS

            S.InsertConnection("halifax","quebec",Colour.BLUE);
            S.InsertConnection("quebec","montreal",Colour.BLUE);
            S.InsertConnection("montreal","kingston",Colour.BLUE);
            S.InsertConnection("kingston", "peterborough", Colour.GREEN);
            S.InsertConnection("peterborough", "toronto", Colour.GREEN);
            S.InsertConnection("kingston", "toronto", Colour.RED);
            S.InsertConnection("toronto", "oakville", Colour.RED);
            S.InsertConnection("oakville", "niagra", Colour.RED);
            S.InsertConnection("kingston", "markham", Colour.ORANGE);
            S.InsertConnection("markham", "sudbury", Colour.GREEN);
            S.InsertConnection("toronto", "markham", Colour.GREEN);
            S.InsertConnection("markham", "saskatoon", Colour.GREEN);
            S.InsertConnection("saskatoon", "yellowknife", Colour.GREEN);
            S.InsertConnection("saskatoon", "calgary", Colour.GREEN);
            S.InsertConnection("calgary", "vancouver", Colour.GREEN);
            S.InsertConnection("vancouver", "whitehorse", Colour.GREEN);
            S.InsertConnection("saskatoon", "vancouver", Colour.PURPLE);
            S.InsertConnection("toronto", "vancouver", Colour.RED);

            //#----METHODS ARE COMMENTED OUT, AND CAN BE UNCOMMENTED TO TEST----#

            //TEST 3: STATIONS STORED IN SUBWAY MAP
            //S.PrintStations();

            // TEST 4: DUPLICATE STATIONS NOT ALLOWED
            //S.InsertStation("toronto"); // DUPLICATE FROM ABOVE
            //S.PrintStations();

            //TEST 5: CONNECTIONS BEEN SUCCESFULLY CREATED
            //        AND ARE FOR BOTH DIRECTIONS (non-directional)
            //S.PrintAllEdges();

            //TEST 6: MULTIPLE SUBWAY CONNECTIONS
            // An adjacent pair of subway stations is permitted 
            // to have multiple subway connections that are differentiated
            // by colour.
            //S.InsertConnection("toronto", "vancouver", Colour.ORANGE);
            //S.InsertConnection("toronto", "vancouver", Colour.YELLOW);
            //S.PrintEdge("toronto");

            //TEST 7: NO DUPLICATIONS OF CONNECTIONS WITH SAME COLOUR AND STATION.
            //S.InsertConnection("oakville", "niagra", Colour.RED);    // we have already entered this above
            //S.PrintEdge("oakville");

            //TEST 8: REMOVE STATION
            // ---Uncomment and test one at a time---
            // All the Stations before removing
            //S.PrintStations();
            // All the Edges before removing
            //S.PrintEdge("toronto");
            // Removing Station
            //S.RemoveStation("toronto");
            // All the Stations after removing
            //S.PrintStations();
            // All the Edges after removing
            //S.PrintAllEdges();


            //TEST 9: Remove Connection
            // First see the connections prior to removing
            //Console.WriteLine(S.ConnectionExists("toronto", "kingston", Colour.ORANGE));
            //S.RemoveConnection("toronto", "kingston", Colour.ORANGE);
            // Check again after removing           
            //Console.WriteLine(S.ConnectionExists("toronto", "kingston", Colour.ORANGE));
            //Console.WriteLine();
            // Verify with all the adjacent Stations to Toronto
            //S.PrintEdge("toronto");


            //TEST 10: Fastest PATH
            //S.FastestRoute("calgary","kingston");

            // what if we enter a route that does not exist
            //S.FastestRoute("kingston", "china");

            // Another route
            //S.FastestRoute("vancouver", "oakville");

           

            //TEST 11: CRITICAL POINTS
            // Returns points that would break the subway into two parts
            //S.CriticalConnections();

        }

        // Our Line Colours for the traversals
        public enum Colour {RED, YELLOW, GREEN, BLUE, PURPLE, ORANGE} 

        // Node that contains adjacent connection, line colour and next node.
        class Node
        {
            public Station Connection { get; set; } // Adjacent station (connection)
            public Colour Line { get; set; } // Colour of its subway line
            public Node Next { get; set; } // Link to the next adjacent station (Node)

            // Constructor
            public Node(Station connection, Colour c, Node next = null) {
                Connection = connection;
                Line = c;
                Next = next;     
            }           
}

        // Station Class, contains Name, if its Visited, and node E
        // of adjacent stations
        class Station
        {
            public string Name { get; set; } // Name of the subway station
            public bool Visited { get; set; } // Used for depth-first and breadth-first searches
            public Node E { get; set; } // Linked list of adjacent stations

            public Station(string name)   // Station constructor 
            { 
                Name = name;
                Visited = false;
                E = null;
                
            } 
        }

        // Our Subway Map and Methods
        class SubwayMap
        {
            private List<Station> S; // List of stations
            public SubwayMap()  // SubwayMap constructor 
            {
                S = new List<Station>();
            }

            // This methods take the name of a station and finds its
            // corresponding index in the List of Stations, S
            public int FindStation(string name)
            {
                int i;

                // we are looping through the list of station S, and checking
                // if the station is stored in our list
                // if it is we return the index or -1 if it does not exist

                for (i = 0; i < S.Count; i++)
                {
                    if (S[i].Name.Equals(name))
                        return i;
                }
                return -1;
            }

            // This method inserts a station if it has not already been inserted
            public void InsertStation(string name)
            {
                // first check if it already exists
                // duplicate station names not allowed
                if (FindStation(name) == -1)
                {
                    // since it does not, we can add it to our list of stations
                    Station s = new Station(name);
                    S.Add(s);
                }
            }

            // This method removes a station
            // it first removes all of its incoming and outgoing 
            // station connections
            public void RemoveStation(string name)
            {
                int i, j;
                i = FindStation(name);

                // verify if Station exists
                if (i > -1)
                {
                    // looping through all of stations
                    for (j = 0; j < S.Count; j++)
                    {
                        //traversing node
                        Node temp = S[j].E, prev = null;

                        while (temp != null)
                        {
                            //remove all adjacent stations to it
                            RemovingStationConnections(j, name);

                            prev = temp;
                            temp = temp.Next;
                        }

                        if (temp != null)
                        {
                            prev.Next = temp.Next;
                        }


                    }
                    // remove the station
                    S.RemoveAt(i);
                }

            }

            // This method supports RemoveStation()
            // it searches for all of the adjacent stations in the
            // station to be removed, and removes their references
            private void RemovingStationConnections(int j, string name)
            {

                // create point nodes to traverse
                Node temp = S[j].E, prev = null;

                // If head node is adjacent to the Station,
                // it is to be deleted

                if (temp != null && temp.Connection.Name == name)
                {
                    S[j].E = temp.Next; // move head
                    return;

                }

                // Now we will search for station to be deleted
                // and we will keep track of
                // the previous node as we need to change temp.next

                while (temp != null && temp.Connection.Name != name)
                {
                    prev = temp;
                    temp = temp.Next;
                }

                // If there was no connection for the station
                if (temp == null)
                    return;
                else
                {// Remove the connection with Adj Station
                    prev.Next = temp.Next;
                    return;
                }

            }

            // This method counts the number of adjacent station in a Station
            public int CountConnections(string name)
            {
                int count = 0;
                int i = FindStation(name);
                Node temp = S[i].E;

                // loop to count the adjacent stations
                while (temp != null)
                {
                    count++;
                    temp = temp.Next;

                }

                return count;
            }

            // This is a Boolean method to see if a connection exists
            // between two stations
            public Boolean ConnectionExists(string name1, string name2, Colour c)
            {
                int i = FindStation(name1);

                //traverse through entire edges
                Node temp = S[i].E;
                while (temp != null)
                {
                    // if match found, return true
                    if (temp.Connection.Name == name2 && temp.Line == c)
                    {
                        return true;
                    }

                    temp = temp.Next;
                }

                // If there was no connection for the station
                if (temp == null)
                    return false;

                else
                { // default
                    return true;
                }

            }

            // This method inserts a connection between two stations
            public void InsertConnection(string name1, string name2, Colour c)
            {
                int i, j;


                // check if the stations exists
                if ((i = FindStation(name1)) > -1 && (j = FindStation(name2)) > -1)
                {
                    // check if the connection exists                  
                    if (!ConnectionExists(name1, name2, c))
                    {
                        // instantiate the new connection link
                        Node e = new Node(S[j], c);

                        // set the next to our Head
                        e.Next = S[i].E;

                        // change the head
                        S[i].E = e;

                        // since its non-directional link, we have to create a reference
                        // in the adjacent station as well
                        InsertConnection(name2, name1, c);

                    }

                }

            }

            // This method removes a connection between two stations
            public void RemoveConnection(string name1, string name2, Colour c)
            {
                int i, j;

                // check if the stations exists
                if ((i = FindStation(name1)) > -1 && (j = FindStation(name2)) > -1)
                {
                    // Does the connection exists?
                    // traverse through the node, if it exists we will remove the connection

                    if (ConnectionExists(name1, name2, c))
                    {
                        // remove connection of name2 from index 'i' of name1
                        RemovingConnection(i, name2, c);

                        // since its non-directional we have to 
                        // remove connection of name1 from index 'j' of name2
                        RemovingConnection(j, name1, c);
                    }
                }

            }

            // This method supports RemoveConnection()
            // It traverses the edge of the station,
            // checks if the adjacent station's name and colour match
            // if it does it removes for both
            private void RemovingConnection(int j, string name, Colour c)
            {
                // create point nodes to traverse
                Node temp = S[j].E, prev = null;

                // If head node is adjacent to the Station,
                // it is to be deleted

                // the name needs to match first
                if (temp.Connection.Name == name)
                {
                    // does the colour match?
                    if (temp.Line == c)
                    {
                        S[j].E = temp.Next; // remove its reference
                        return;
                    }
                }

                //traverse forward
                prev = temp;
                temp = temp.Next;

                // check if it exists as we move down the edge
                while (temp != null)
                {
                    // does name match?
                    if (temp.Connection.Name == name)
                    {
                        if (temp.Line == c)
                        {
                            prev.Next = temp.Next;
                            return;
                        }
                    }

                    //traverse forward
                    prev = temp;
                    temp = temp.Next;

                    // If there was no connection for the station
                    if (temp == null)
                        return;
                }
            }

            /* This method finds the fastest route between two stations by
            * using the breadth first search algorithm
            * Our approach is to traverse begining from the start point
            * we create a queue, and mark it visited, we then return to our original point
            * and traverse to the next one, if along any point we find our final destination
            * we print its traversed path.
            */

            public void FastestRoute(string From, string To)
            {
                // will hold our To and From values
                int i, j;

                // out adjacent Station
                Station t;

                // makes all of the Stations marked as 
                // not visited
                for (i = 0; i < S.Count; i++)
                    S[i].Visited = false;

                // Array "before" holds our Stations
                // from before the traversal to next one.	
                Station[] before = new Station[S.Count];

                // our default for-loop to ensure everything in 
                // our Station array is null
                for (i = 0; i < before.Length; i++)
                    before[i] = null;   

                // verify if our Station exists
                if ((i = FindStation(From)) > -1 && (j = FindStation(To)) > -1)
                {
                    // Implement a Breadth First Search 
                    Queue<Station> Q = new Queue<Station>();
                    Station s = S[i];

                    // enqueue our starting station
                    Q.Enqueue(s);
                    s.Visited = true;

                    // this while-loop will countinue 
                    // until all adjacent stations have been visited
                    while (Q.Count != 0)
                    {
                        // dequeued is set to s
                        s = Q.Dequeue();

                        // the node "temp" we will use for traversal
                        Node temp = s.E;

                        // while loop as long as there is an adjacent station
                        while (temp.Next != null)
                        {
                            // assign the next adjacent station
                            t = temp.Next.Connection;

                            // if adjacent station has not been visited
                            // enqueue it and mark it visited

                            if(!t.Visited)
                            {
                                Q.Enqueue(t);
                                t.Visited = true;

                                // we now store s, which is the station that came before
                                // in the position of t, to reference it based on S
                                before[FindStation(t.Name)] = s;
                            }

                            // If we find our final destination then we can print it out.	
                            // We can stop the remaining traversal with a return call
                            if (t.Name == To)
                            {
                                // this method will iterate and print our fastest route
                                PrintFastestRoute(before, temp, From);
                                return;
                            }

                            // traverse to the next adjacent station
                            temp = temp.Next;
                        }
                    }
                }
                // If either of the stations do not exist or there is no path
                Console.WriteLine("Unfortunately, your route does not exist!");
            }
        
            /* This method prints our fastest route which is stored in the
             * "before" array
             */
        private void PrintFastestRoute(Station[] before, Node temp, string From)
        {
            // this will store our fastestPath of traversal
            List<Station> fastestPath = new List<Station>();

            // adding the stations that we traversed
            fastestPath.Add(temp.Next.Connection);

            // this station came before our final destination
            Station s = before[FindStation(temp.Next.Connection.Name)];

            // we are going to traverse backwards from the end
            // since it stored in that manner

            while (s.Name != From)
            {
                // lets add the station that came before
                fastestPath.Add(s);
                // we repeat, this is the station prior to the one above
                s = before[FindStation(s.Name)];
            }

            // this is our starting point of traversal
            fastestPath.Add(s);

            // Lets print the list of fastest route
            // we will print in reverse
            // as its stored in reverse

            Console.WriteLine("The Fastest Route is:\n");

            for (int i = fastestPath.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("{0}",fastestPath[i].Name);
            }
        }


            /*The purpose of this method is to find connections, if removed would
             * cause the subway to be broken into 2 parts
             * 
             * Our approach is traverse using the Dept First Search alogirithm
             * something to note is the for loop counter in the method Public DeptFirstSearch(...)
             * If a connection is broken off, it will restart
             * we are going to store the connection (each) into a list, and have a counter.
             * We will return the connections that needed to be restarted, hence we
             * get our critical points.           
             */

            public void CriticalConnections()
            {
                // holds value when we re-add our removed connections
                string holdName;
                Colour holdColour;
                

                for (int i =0; i <S.Count; i++)
                {
                    Node temp = S[i].E ;


                    while (temp != null)
                    {
                        //store the values and colour
                        holdName = temp.Connection.Name;
                        holdColour = temp.Line;

                        // removing a connection to see if it will restart the DFS
                        RemovingStationConnections(i, temp.Connection.Name);
                        
                        // if the graph breaks, it will re-iterate, we will store this
                        DepthFirstSearch(S[i].Name, temp.Connection.Name);
                        
                        // adding connection back that we removed earlier
                        InsertConnection(S[i].Name, holdName, holdColour);

                        // loop counter
                        temp = temp.Next;
                            
                    }
                }

            }

            // This method is the Depth First Search
            // It traverse all the way until the end of the adjacent stations
            // then it returns and traverses another path

            public void DepthFirstSearch(string name1, string name2)
            {
                // this is how we store our connections
                // and implement a counter
                List<string> firstStation = new List<string>();
                List<string> secondStation = new List<string>();

                // counter for splitting up
                List<int> count = new List<int>();

                // our cleaned up lists
                List<string> cleanFirstStation = new List<string>();
                List<string> cleanSecondStation = new List<string>();


                // setting all values in count to be 0
                // default: assuming we have less than 100 connections
                for (int h = 0; h < 100; h++)
                {
                    count.Add(0);
                }
                int i;

                // Set all vertices as unvisited
                for (i = 0; i < S.Count; i++)     
                    S[i].Visited = false;

                for (i = 0; i < S.Count; i++)
                {
                    // This is key note, if this reoccurs then our critical connection is found
                    if (!S[i].Visited)                  
                    {
                        // recursuion if we have not visited
                        DepthFirstSearch(S[i]);

                        // adding our removed connections to the list
                        firstStation.Add(name1);
                        secondStation.Add(name2);

                        // ideally if the index match and there is a split (critical connection)
                        // we will add the count
                        for (int x = 0; x < firstStation.Count; x++)
                        {
                            for (int y = 0; y < secondStation.Count; y++)
                            {
                                // this ensure the arrays have are matched based on connection
                                if (firstStation[x] == name1 && secondStation[y] == name2)
                                {
                                    count[y] = count[y] + 1;
                                }
                            }
                        }
                    }                                                                    
                }

                // saving the critical points
                for (int z = 0; z < count.Count; z++)
                {
                    if (count[z] >= 2)
                    {
                        // currently has repetition from crticial connections
                        // so we will add them into a cleaned list
                        cleanFirstStation.Add(firstStation[z]);
                        cleanSecondStation.Add(secondStation[z]);
                    }
                }

                // printing our critical points
                for(int z = 0; z < cleanFirstStation.Count; z+=2)
                {
                    Console.WriteLine(" Our critical connections is: ");
                    Console.WriteLine();
                    Console.WriteLine("{0} <------> {1}", cleanFirstStation[z], cleanSecondStation[z]);
                    Console.WriteLine();
                }            
            }

            // Recursive method of DepthFirstSearch() method
            // traverses the adjacent nodes
            private void DepthFirstSearch(Station s)
            {
                // adjacent station
                Station t;

                // Output station marked as visited
                s.Visited = true;    

                // our traversing node
                Node temp = s.E;

                // as long as temp has adjacent stations in dept
                while (temp != null) 
                {
                    // set to adjacent station
                    t = temp.Connection;

                    // if it hasn't been visited
                    // we will do a recursion
                    if (!temp.Connection.Visited)
                        DepthFirstSearch(t);

                    // loop counter
                    temp = temp.Next;
                }
            }


            // This method prints all of the Stations in the Subway Map
            public void PrintStations()
            {
                for (int i = 0; i < S.Count; i++)
                    Console.WriteLine(S[i].Name);
                    Console.ReadLine();
            }

            // This method prints the Edges of the each Station
            // It displays its name, adjacent connection, and line colour
            public void PrintAllEdges()
            {
                int i;

                for (i = 0; i < S.Count; i++)
                {
                    while (S[i].E != null)
                    {
                        Console.WriteLine("(" + S[i].Name + "," + S[i].E.Connection.Name + "," + S[i].E.Line + ")");
                        Console.ReadLine();
                        S[i].E = S[i].E.Next;
                    }
                }
                        
            }

            // This method prints the edge of a selected Station
            // It displays  adjacent connection, and line colour only to
            // our selected Station
            public void PrintEdge(string name)
            {
                int i = FindStation(name);

                    while (S[i].E != null)
                    {
                        Console.WriteLine("(" + S[i].Name + "," + S[i].E.Connection.Name + "," + S[i].E.Line + ")");
                        Console.ReadLine();
                        S[i].E = S[i].E.Next;
                    }
                

            }

        }

    }
}
