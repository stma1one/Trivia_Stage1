using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Rank
{
    [Key]
    [Column("rankid")]
    public int Rankid { get; set; }

    [Column("rankName")]
    [StringLength(255)]
    public string? RankName { get; set; }

    [InverseProperty("Rank")]
    public virtual ICollection<User> Users { get; } = new List<User>();
}
