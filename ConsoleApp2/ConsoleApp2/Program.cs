﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    public class Program
    {
        public static void RunLab2(string inputPath, string outputPath)
        {
            if (File.Exists(inputPath))
            {
                string[] inputLines = File.ReadAllLines(inputPath);
                string[] input = inputLines[0].Split();
                if (input.Length >= 2)
                {
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
                        if (road.Length != 2)
                        {
                            Console.WriteLine($"Error in line {i}: {inputLines[i]}");
                            return;
                        }
                        int village1 = int.Parse(road[0]);
                        int village2 = int.Parse(road[1]);

                        graph[village1].Add(village2);
                        graph[village2].Add(village1);
                    }


                    int result = CalculateMinimumRoadsToIsolateGroup(graph, N, P);

                    File.WriteAllText(outputPath, result.ToString());
                    Console.WriteLine("lab2 was executed successfuly");
                    Console.WriteLine("result is in " + outputPath);

                }
                else
                {
                    Console.WriteLine("File INPUT.TXT has not enough data");
                    Console.WriteLine(inputLines);
                    Console.WriteLine(inputPath);
                }
            }
            else
            {
                Console.WriteLine("File INPUT.TXT not found.");

            }
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

        static void Main()
        {
            RunLab2("INPUT.TXT", "OUTPUT.TXT");
        }
    }
}
