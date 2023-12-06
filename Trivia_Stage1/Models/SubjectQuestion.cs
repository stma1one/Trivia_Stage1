using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class SubjectQuestion
{
    [Key]
    public int SubjectId { get; set; }

    [StringLength(10)]
    public string SubjectName { get; set; } = null!;

    [InverseProperty("SubjectNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
