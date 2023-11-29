using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class Status
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("currentStatus")]
    [StringLength(255)]
    public string? CurrentStatus { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
