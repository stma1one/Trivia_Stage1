using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("Player")]
public partial class Player
{
    [Key]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    [StringLength(10)]
    public string Name { get; set; } = null!;

    public int Rank { get; set; }

    public int Points { get; set; }

    [StringLength(8)]
    public string Password { get; set; } = null!;

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [ForeignKey("Rank")]
    [InverseProperty("Players")]
    public virtual Ranking RankNavigation { get; set; } = null!;
}
