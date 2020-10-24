using System;

[Serializable]
public class Subject
{
    public string Name { get; set; }
    public QuestionCard[] QuestionCards { get; set; }
}