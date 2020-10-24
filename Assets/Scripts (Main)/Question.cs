using System;

[Serializable]
public class QuestionCard
{
    public string Question { get; set; }
    public string Answer { get; set; }

    byte proficiency;
    public byte Proficiency { get { return proficiency; } set { if (value > 3) { proficiency = 3; } else if (value < 1) { proficiency = 1; } else { proficiency = value; } } }
}