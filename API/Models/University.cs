using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_universities")]

public class University : BaseEntity
{
    [Key]
    [Column("guid")]
    public Guid Guid { get; set; }
    [Column("code", TypeName = "nvarchar(50)")]
    public string Code { get; set; }
    [Column("name", TypeName = "nvarchar(100)")]
    public string Name { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }

    // Cardinality
    public ICollection<Education>? Educations { get; set; }
}

