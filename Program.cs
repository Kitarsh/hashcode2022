//var fileName = "./../../../Inputs/a_an_example.in.txt";
var fileName = "./../../../Inputs/b_better_start_small.in.txt";
//var fileName = "./../../../Inputs/c_collaboration.in.txt";
//var fileName = "./../../../Inputs/d_dense_schedule.in.txt";
//var fileName = "./../../../Inputs/e_exceptional_skills.in.txt";
//var fileName = "./../../../Inputs/f_find_great_mentors.in.txt";

InputService.SetInputFileName(fileName);

var projects = InputService.GetProjects();
Dictionary<int, Project> orderedProjects = new Dictionary<int, Project>();

for (int j = 0; j < 30; j++)
{
    var projectsToDo = projects.Where(p => !p.IsDone)
                               .OrderBy(p => p.RequiredSkills.Max(rs => rs.Value.Level))
                               .ThenBy(p => p.RequiredSkills.Count())
                               .Take(5)
                               .ToList();

    for (int i = 0; i < projectsToDo.Count; i++)
    {
        AssignementService.PopulateContributorsForProject(projectsToDo[i]);
    }
    AssignementService.CompleteAllProjects(orderedProjects);
}

//AssignementService.PopulateContributorsForProject(projects[2]);
//AssignementService.CompleteAllProjects(orderedProjects);
//AssignementService.PopulateContributorsForProject(projects[0]);
//AssignementService.CompleteAllProjects(orderedProjects);

OutputService.Write(orderedProjects);