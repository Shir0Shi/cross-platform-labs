using System;
using System.IO;

namespace ConsoleApp3
{
    public class Program
    {
        public static void RunLab3(string inputPath, string outputPath)
        {

            if (!File.Exists(inputPath))
            {
                Console.WriteLine("No Input file");
                return;
            }

            string[] inputLines = File.ReadAllLines(inputPath);

            if (inputLines.Length < 1)
            {
                Console.WriteLine("Input file is empty");
                return;
            }

            string[] mnk = inputLines[0].Split();

            if (mnk.Length != 3)
            {
                Console.WriteLine("incorrect type of input");
                Console.WriteLine(inputLines);
                return;
            }

            int m = int.Parse(mnk[0]);
            int n = int.Parse(mnk[1]);
            int k = int.Parse(mnk[2]);

            if (m < 1 || m > 16 || n < 1 || n > 100 || k < 1 || k > 200)
            {
                Console.WriteLine("m, n and k should be: 1 ≤ m ≤ 16, 1 ≤ n ≤ 100, 1 ≤ k ≤ 200.");
                return;
            }

            int[,] board = new int[m, n];

            for (int i = 1; i <= m; i++)
            {
                string[] row = inputLines[i].Split();
                for (int j = 1; j <= n; j++)
                {
                    board[i - 1, j - 1] = int.Parse(row[j - 1]);
                }
            }

            int[,,] dp = new int[m, n, k + 1];

            for (int x = 1; x <= k; x++)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        dp[i, j, x] = board[i, j];

                        if (i > 0)
                            dp[i, j, x] += Math.Max(dp[i - 1, j, x - 1], dp[i - 1, j, x]);

                        if (j > 0)
                            dp[i, j, x] += Math.Max(dp[i, j - 1, x - 1], dp[i, j - 1, x]);


                        for (int l = 2; l <= x; l++)
                        {
                            if (i >= l)
                                dp[i, j, x] = Math.Max(dp[i, j, x], dp[i - l, j, x - 1] + Sum(board, i - l + 1, i, j, j));

                            if (j >= l)
                                dp[i, j, x] = Math.Max(dp[i, j, x], dp[i, j - l, x - 1] + Sum(board, i, i, j - l + 1, j));
                        }
                    }
                }
            }

            File.WriteAllText(outputPath, dp[m - 1, n - 1, k].ToString());
            Console.WriteLine("lab3 was executed successfuly");
            Console.WriteLine("result id in "+ outputPath);
        }

        static int Sum(int[,] board, int rowStart, int rowEnd, int colStart, int colEnd)
        {
            int sum = 0;
            for (int i = rowStart; i <= rowEnd; i++)
            {
                for (int j = colStart; j <= colEnd; j++)
                {
                    sum += board[i, j];
                }
            }
            return sum;
        }
        static void Main()
        {
            RunLab3("INPUT.TXT", "OUTPUT.TXT");
        }
    }
}
