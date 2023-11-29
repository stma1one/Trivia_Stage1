using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("pswrd")]
    [StringLength(50)]
    public string Pswrd { get; set; } = null!;

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("points")]
    public int? Points { get; set; }

    [Column("questionsadded")]
    public int? Questionsadded { get; set; }

    [Column("rankid")]
    public int? Rankid { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    [ForeignKey("Rankid")]
    [InverseProperty("Users")]
    public virtual Rank? Rank { get; set; }
}
