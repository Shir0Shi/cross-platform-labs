using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string[] inputLines = File.ReadAllLines("INPUT.TXT");
        string[] input = inputLines[0].Split();
        int N = int.Parse(input[0]);
        int P = int.Parse(input[1]);

        List<int>[] graph = new List<int>[N + 1];

        for (int i = 1; i <= N; i++)
        {
            graph[i] = new List<int>();
        }

        for (int i = 1; i < inputLines.Length; i++)
        {
            string[] road = inputLines[i].Split();
            int village1 = int.Parse(road[0]);
            int village2 = int.Parse(road[1]);

            graph[village1].Add(village2);
            graph[village2].Add(village1);
        }

        int result = CalculateMinimumRoadsToIsolateGroup(graph, N, P);

        File.WriteAllText("OUTPUT.TXT", result.ToString());
    }

    static int CalculateMinimumRoadsToIsolateGroup(List<int>[] graph, int N, int P)
    {
        int minRoadsToIsolate = 150;

        for (int startCity = 1; startCity <= N; startCity++)
        {
            bool[] visited = new bool[N + 1];
            List<int> currentCity = new List<int>();
            List<int> city1 = new List<int>();
            List<int> city2 = new List<int>();
            Dfs(graph, startCity, visited, currentCity);

            if (currentCity.Count >= P)
            {
                city1.AddRange(currentCity.GetRange(0, P));
                city2.AddRange(currentCity.GetRange(P, currentCity.Count - P));
                int roadsToIsolate = CalculateRoadsToIsolate(graph, city1);
                minRoadsToIsolate = Math.Min(minRoadsToIsolate, roadsToIsolate);
            }
        }

        return minRoadsToIsolate;
    }

    static void Dfs(List<int>[] graph, int v, bool[] visited, List<int> currentCity)
    {
        visited[v] = true;
        currentCity.Add(v);

        foreach (int u in graph[v])
        {
            if (!visited[u])
            {
                Dfs(graph, u, visited, currentCity);
            }
        }
    }

    static int CalculateRoadsToIsolate(List<int>[] graph, List<int> с1)
    {
        int roadsToIsolate = 0;

        foreach (int v in с1)
        {
            foreach (int u in graph[v])
            {
                if (!с1.Contains(u))
                {
                    roadsToIsolate++;
                }
            }
        }

        return roadsToIsolate;
    }
}
