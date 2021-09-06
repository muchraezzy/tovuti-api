using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace tovuti_api.Models
{
    [Table("attributes")]
    public partial class attributes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public attributes()
        {
            //Tokens = new HashSet<Token>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int attr_id { get; set; }
        [Required]
        public int pid { get; set; }
        [StringLength(50)]
        public string color { get; set; }
        public string size { get; set; }
        public string gender { get; set; }
        public decimal price { get; set; }
        [NotMapped]
        public string pname { get; set; }
        public string created_by { get; set; }
     
        public string modified_by { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       // public virtual ICollection<Token> Tokens { get; set; }
    }
}