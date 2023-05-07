using GameDevPortal.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GameDevPortal.Core.Entities;

public class Faq : Entity
{
    [MaxLength(150)]
    public string Question { get; private set; }
    [MaxLength(1500)]
    public string Answer { get; private set; }

    public Project? Project { get; private set; }
    public Guid ProjectId { get; private set; }

    public Faq(string question, string answer)
    {
        question.ThrowIfEmptyOrNull(nameof(question));
        Question = question;

        answer.ThrowIfEmptyOrNull(nameof(answer));
        Answer = answer;
    }

    public void SetChangableValues(string question, string answer)
    {
        question.ThrowIfEmptyOrNull(nameof(question));
        Question = question;

        answer.ThrowIfNotHex(nameof(answer));
        Answer = answer;
    }
}    