using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Question
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("text")]
    [StringLength(255)]
    public string? Text { get; set; }

    [Column("rightAnswer")]
    [StringLength(255)]
    public string? RightAnswer { get; set; }

    [Column("wrongAnswer1")]
    [StringLength(255)]
    public string? WrongAnswer1 { get; set; }

    [Column("wrongAnswer2")]
    [StringLength(255)]
    public string? WrongAnswer2 { get; set; }

    [Column("wrongAnswer3")]
    [StringLength(255)]
    public string? WrongAnswer3 { get; set; }

    [Column("userId")]
    public int? UserId { get; set; }

    [Column("statusId")]
    public int? StatusId { get; set; }

    [Column("subjectId")]
    public int? SubjectId { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Questions")]
    public virtual Status? Status { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("Questions")]
    public virtual Subject? Subject { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Questions")]
    public virtual User? User { get; set; }
}
