namespace HashCode2022.Services;

public static class OutputService
{
    private static readonly string filename = $"Output-{DateTime.Now.ToString("hh-mm-ss")}.txt";

    public static void Write(Dictionary<int, Project> dictionary)
    {
        var lines = new List<String>();

        var nbOfProjectsDone = dictionary.Count();

        lines.Add($"{nbOfProjectsDone}");
        foreach (var project in dictionary.Select(e => e.Value).ToList())
        {
            var orderedContributors = project.Contributors.Select(c => c.Value.Name).ToList();
            lines.Add(project.Name);
            lines.Add(string.Join(" ", orderedContributors));
        }

        using (StreamWriter outfile = new StreamWriter(filename))
        {
            outfile.WriteLine(string.Join("\n", lines));
        }
    }
}