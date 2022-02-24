namespace HashCode2022.Services;

public static class AssignementService
{
    public static void CompleteAllProjects(Dictionary<int, Project> doneProjects)
    {
        var projects = InputService.GetProjects().Where(p => !p.IsDone);

        foreach (var project in projects)
        {
            project.IsDone = project.hasContributorsForEachSkills();
            if (project.IsDone)
            {
                project.FreeContributors();
                doneProjects.Add(doneProjects.LastOrDefault().Key + 1, project);
            }
        }
    }

    public static void PopulateContributorsForProject(Project project)
    {
        var avCo = InputService.GetContributors()
                               .Where(c => !c.IsAssignToProject)
                               .ToList();

        foreach (KeyValuePair<int, Skill> skill in project.RequiredSkills)
        {
            var contribMatch = avCo.FirstOrDefault(c => c.Skills.Any(s => s.Name == skill.Value.Name && s.Level >= skill.Value.Level));
            if (contribMatch != null && !contribMatch.IsAssignToProject)
            {
                project.Contributors.Add(skill.Key, contribMatch);// il faut aussi préciser que le slot pour ce skill du projet est désormais pris
                contribMatch.IsAssignToProject = true;
            }
        }
    }

    // pour chaque projet
    // on cherche les contributeur dispo qui ont le bon skill
    // on assigne au projet
    // quand tous les postes sont pris, début du projet
    // fin du projet => on donne les points aux participants qui ont merité
}