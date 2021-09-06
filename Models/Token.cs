using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace tovuti_api.Models
{
    [Table("token")]
    public partial class Token
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Token()
        {
            
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int tid { get; set; }

        [Column("Token")]
        [Required]
        [StringLength(250)]
        public string Text { get; set; }
        public int uid { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastTimeUsed { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       // public virtual User User  { get; set; }
    }
}