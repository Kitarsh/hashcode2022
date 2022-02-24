namespace HashCode2022.Models;

public class Contributor
{
    public bool IsAssignToProject { get; set; } = false;
    public string Name { get; set; }

    public List<Skill> Skills { get; } = new List<Skill>();

    public void EndProject(Skill skill)
    {
        IsAssignToProject = false;
        LearnSkill(skill);
    }

    private void LearnSkill(Skill skill)
    {
        foreach (var learntSkill in Skills)
        {
            if (learntSkill.Name == skill.Name)
            {
                if (skill.Level >= learntSkill.Level)
                {
                    learntSkill.Level++;
                }
                return;
            }
        }

        // si le skill n'a pas été trouvé dans les skills déjà appris, on l'ajout à la personne au niveau 1.
        var newSkill = new Skill
        {
            Name = skill.Name,
            Level = 1,
        };
        Skills.Add(newSkill);
    }
}