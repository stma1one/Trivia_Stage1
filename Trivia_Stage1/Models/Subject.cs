using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Subject
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("subjectName")]
    [StringLength(255)]
    public string? SubjectName { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
