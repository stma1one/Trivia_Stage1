using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Question
{
    [StringLength(30)]
    public string Subject { get; set; } = null!;

    [StringLength(100)]
    public string Text { get; set; } = null!;

    [Key]
    public int QuestionId { get; set; }

    [StringLength(50)]
    public string Ranswer { get; set; } = null!;

    [StringLength(50)]
    public string Wanswer1 { get; set; } = null!;

    [StringLength(50)]
    public string Wanswer2 { get; set; } = null!;

    [StringLength(50)]
    public string Wanswer3 { get; set; } = null!;

    [StringLength(50)]
    public string CreatedBy { get; set; } = null!;

    public int StatusId { get; set; }

    public int SubjectId { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("Questions")]
    public virtual Player CreatedByNavigation { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Questions")]
    public virtual StatusQuestion Status { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("Questions")]
    public virtual SubjectQuestion SubjectNavigation { get; set; } = null!;
}
