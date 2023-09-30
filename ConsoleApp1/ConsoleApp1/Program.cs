using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            if (File.Exists("INPUT.TXT"))
            {
                string[] inputLines = File.ReadAllLines("INPUT.TXT");
                string[] input = inputLines[0].Split();
                if (input.Length >= 3)
                {
                    int N = int.Parse(input[0]);
                    int K = int.Parse(input[1]);
                    int M = int.Parse(input[2]);

                    if (K <= 0 || K >= N || N >= 25 || M < 0 || M > N * (N - 1) / 2)
                    {
                        Console.WriteLine("Wrong inputs. Important: 0 < K < N < 25 и 0 ≤ M ≤ N(N-1)/2.");
                        return;
                    }


                    List<int>[] friends = new List<int>[N + 1];

                    for (int i = 0; i < M; i++)
                    {
                        string[] pair = inputLines[i + 1].Split();
                        int person1 = int.Parse(pair[0]);
                        int person2 = int.Parse(pair[1]);

                        if (friends[person1] == null)
                            friends[person1] = new List<int>();
                        if (friends[person2] == null)
                            friends[person2] = new List<int>();

                        friends[person1].Add(person2);
                        friends[person2].Add(person1);
                    }

                    List<int> team1 = new List<int>();
                    List<int> team2 = new List<int>();
                    List<int> currentTeam = new List<int>();
                    bool[] used = new bool[N + 1];

                    void Dfs(int v)
                    {
                        used[v] = true;
                        currentTeam.Add(v);

                        foreach (int u in friends[v])
                        {
                            if (!used[u])
                            {
                                Dfs(u);
                            }
                        }
                    }

                    for (int i = 1; i <= N; i++)
                    {
                        if (!used[i])
                        {
                            currentTeam.Clear();
                            Dfs(i);

                            if (currentTeam.Count > team1.Count)
                            {
                                team2.AddRange(team1);
                                team1.Clear();
                                foreach (int person in currentTeam)
                                {
                                    team1.Add(person);
                                }
                            }
                            else
                            {
                                team2.AddRange(currentTeam);
                            }
                        }
                    }

                    File.WriteAllText("OUTPUT.TXT", string.Join(" ", team1.Take(K)));
                }
                else
                {
                    Console.WriteLine("File INPUT.TXT has not enough data");
                }
            }
            else
            {
                Console.WriteLine("File INPUT.TXT not found.");

            }
        }
    }
}
