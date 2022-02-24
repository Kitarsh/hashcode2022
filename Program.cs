//var fileName = "./../../../Inputs/a_an_example.in.txt";
var fileName = "./../../../Inputs/b_better_start_small.in.txt";
//var fileName = "./../../../Inputs/c_collaboration.in.txt";
//var fileName = "./../../../Inputs/d_dense_schedule.in.txt";
//var fileName = "./../../../Inputs/e_exceptional_skills.in.txt";
//var fileName = "./../../../Inputs/f_find_great_mentors.in.txt";

InputService.SetInputFileName(fileName);

var projects = InputService.GetProjects();
Dictionary<int, Project> orderedProjects = new Dictionary<int, Project>();

//var temps = 0;

//for (var i = 0; i < 1000; i++)
//{
//    // si un des projet est
//    pr
//    temps++
//}
for (int i = 0; i < projects.Count; i++)
{
    AssignementService.PopulateContributorsForProject(projects[i]);
    AssignementService.CompleteAllProjects(orderedProjects);
}

//AssignementService.PopulateContributorsForProject(projects[2]);
//AssignementService.CompleteAllProjects(orderedProjects);
//AssignementService.PopulateContributorsForProject(projects[0]);
//AssignementService.CompleteAllProjects(orderedProjects);

OutputService.Write(orderedProjects);