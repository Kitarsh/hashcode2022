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
        var requiredSkills = project.RequiredSkills.OrderByDescending(rs => rs.Value.Level)
                                                   .ToDictionary(rs => rs.Key, rs => rs.Value);

        foreach (KeyValuePair<int, Skill> skill in requiredSkills)
        {
            if (project.Contributors.ContainsKey(skill.Key))// il faut aussi préciser que le slot pour ce skill du projet est désormais pris
            {
                continue;
            }
            // A-t-on un contrib avec un niveau suffisant ?
            var contribMatch = avCo.FirstOrDefault(c => c.HasRequiredSkill(skill.Value));
            if (contribMatch != null && !contribMatch.IsAssignToProject)
            {
                project.Contributors.Add(skill.Key, contribMatch);
                contribMatch.IsAssignToProject = true;
            }
        }

        foreach (KeyValuePair<int, Skill> skill in requiredSkills)
        {
            if (project.Contributors.ContainsKey(skill.Key))// il faut aussi préciser que le slot pour ce skill du projet est désormais pris
            {
                continue;
            }
            // A-t-on un contrib qui peut être mentoré ?
            var contribInProject = project.Contributors.Select(c => c.Value).ToList();

            var contribMatch = avCo.FirstOrDefault(c => c.CanBeMentored(skill.Value, contribInProject));
            if (contribMatch != null && !contribMatch.IsAssignToProject)
            {
                project.Contributors.Add(skill.Key, contribMatch);
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