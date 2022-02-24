namespace HashCode2022.Services;

public static class InputService
{
    private static List<Contributor>? contributors = null;
    private static string filename;
    private static int NbOfContributors = 0;
    private static int NbOfContributorsLines = 0;

    private static int NbOfProjects = 0;
    private static List<Project>? projects = null;

    public static List<Contributor> GetContributors()
    {
        if (contributors == null)
        {
            ReadInputs();
        }

        return contributors;
    }

    public static List<Project> GetProjects()
    {
        if (projects == null)
        {
            ReadInputs();
        }

        return projects;
    }

    public static void SetInputFileName(string newInputFilename)
    {
        filename = newInputFilename;
    }

    private static void ReadContributors(string[] lines)
    {
        contributors = new List<Contributor>();
        var skillCount = 0;
        for (int numeroCont = 0; numeroCont < NbOfContributors; numeroCont++)
        {
            Contributor contributor = new Contributor();

            var contributorIndex = numeroCont + skillCount;

            var contributorParams = lines[contributorIndex].Split(" ");
            contributor.Name = contributorParams[0];
            var nbOfSkills = Int32.Parse(contributorParams[1]);

            for (int numeroSkill = 0; numeroSkill < nbOfSkills; numeroSkill++)
            {
                Skill skill = new Skill();
                var skillParams = lines[contributorIndex + numeroSkill + 1].Split(" ");
                skill.Name = skillParams[0];
                skill.Level = Int32.Parse(skillParams[1]);
                contributor.Skills.Add(skill);
                skillCount++;
            }
            contributors.Add(contributor);
            NbOfContributorsLines += nbOfSkills + 1;
        }
    }

    private static void ReadInputs()
    {
        string fileContent = File.ReadAllText(filename);
        var lines = fileContent.Split("\n");

        var firstLine = lines[0].Split(" ");

        NbOfContributors = Int32.Parse(firstLine[0]);
        Console.WriteLine($"Contributors : {NbOfContributors}");
        NbOfProjects = Int32.Parse(firstLine[1]);
        Console.WriteLine($"Projects : {NbOfProjects}");

        var contributorsLines = lines.Skip(1).ToArray();

        ReadContributors(contributorsLines);

        var projectsLines = lines.Skip(NbOfContributorsLines + 1).ToArray();

        ReadProjects(projectsLines);
    }

    private static void ReadProjects(string[] lines)
    {
        projects = new List<Project>();
        var skillCount = 0;
        for (var numeroProject = 0; numeroProject < NbOfProjects; numeroProject++)
        {
            Project project = new Project();
            var projectIndex = numeroProject + skillCount;

            var projectParams = lines[projectIndex].Split(" ");

            project.Name = projectParams[0];
            project.Duration = Int32.Parse(projectParams[1]);
            project.Score = Int32.Parse(projectParams[2]);
            project.BestBefore = Int32.Parse(projectParams[3]);
            var nbOfRoles = Int32.Parse(projectParams[4]);

            for (var numeroOfRole = 0; numeroOfRole < nbOfRoles; numeroOfRole++)
            {
                Skill skill = new Skill();
                var skillParams = lines[projectIndex + numeroOfRole + 1].Split(" ");
                skill.Name = skillParams[0];
                skill.Level = Int32.Parse(skillParams[1]);
                project.RequiredSkills.Add(numeroOfRole, skill);
                skillCount++;
            }
            projects.Add(project);
        }
    }
}