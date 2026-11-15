using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var graph = new Dictionary<string, List<string>>();

        graph["Москва"] = new List<string> { "Казань", "Минск" };
        graph["Казань"] = new List<string> { "Москва", "Екатеринбург" };
        graph["Минск"] = new List<string> { "Москва", "Варшава" };
        graph["Екатеринбург"] = new List<string> { "Казань", "Новосибирск" };
        graph["Варшава"] = new List<string> { "Минск" };
        graph["Новосибирск"] = new List<string> { "Екатеринбург" };
        graph["Сочи"] = new List<string>();

        string start = "Москва";
        string end = "Новосибирск";

        List<string> path = FindShortestPath(graph, start, end);

        if (path != null)
        {
            Console.WriteLine("Кратчайший путь: " + string.Join(" -> ", path));
        }
        else
        {
            Console.WriteLine("Путь не найден. Возможно, граф несвязный.");
        }
    }

    static List<string> FindShortestPath(Dictionary<string, List<string>> graph, string start, string end)
    {
        Queue<string> queue = new Queue<string>();
        Dictionary<string, string> parents = new Dictionary<string, string>();
        HashSet<string> visited = new HashSet<string>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();

            if (current == end)
            {
                break;
            }

            if (!graph.ContainsKey(current)) continue;

            foreach (string neighbor in graph[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    parents[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        if (!parents.ContainsKey(end)) return null;

        List<string> path = new List<string>();
        string step = end;

        while (step != start)
        {
            path.Add(step);
            step = parents[step];
        }
        path.Add(start);
        path.Reverse();

        return path;
    }
}
