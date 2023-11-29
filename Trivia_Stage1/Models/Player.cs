using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Player
{
    [Key]
    public int PlayerId { get; set; }

    [StringLength(30)]
    public string Mail { get; set; } = null!;

    [StringLength(20)]
    public string Name { get; set; } = null!;

    public int RankId { get; set; }

    public int Points { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    [ForeignKey("RankId")]
    [InverseProperty("Players")]
    public virtual Rank Rank { get; set; } = null!;
}
