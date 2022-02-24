namespace HashCode2022.Models;

public class Project
{
    public int BestBefore { get; set; }
    public Dictionary<int, Contributor> Contributors { get; } = new Dictionary<int, Contributor>();
    public double Duration { get; set; }
    public bool IsDone { get; set; } = false;
    public string Name { get; set; }
    public Dictionary<int, Skill> RequiredSkills { get; } = new Dictionary<int, Skill>();
    public int Score { get; set; }

    public void FreeContributors()
    {
        foreach (var contributor in this.Contributors)
        {
            contributor.Value.EndProject(RequiredSkills[contributor.Key]);
        }
    }

    public bool hasContributorsForEachSkills()
    {
        // A implémenter : les mentorats.
        foreach (var skillKv in RequiredSkills)
        {
            if (!this.Contributors.ContainsKey(skillKv.Key))
            {
                return false;
            }

            var contributor = this.Contributors[skillKv.Key];

            var isSkillCovered = contributor.Skills.Any(s => s.Name == skillKv.Value.Name && s.Level >= skillKv.Value.Level);

            if (!isSkillCovered)
            {
                return false;
            }
        }
        return true;
    }
}