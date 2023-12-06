using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

[Table("Ranking")]
public partial class Ranking
{
    [StringLength(10)]
    public string RankName { get; set; } = null!;

    [Key]
    public int RankId { get; set; }

    [InverseProperty("RankNavigation")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
