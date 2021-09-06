using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace tovuti_api.Models
{
    [Table("product")]
    public partial class products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public products()
        {
            //Tokens = new HashSet<Token>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int pid { get; set; }
        [Required]
        public int cid { get; set; }
        [Required]
        public int uid { get; set; }

        [Required]
        [StringLength(50)]
        public string pname { get; set; }
        [NotMapped]
        public string cname { get; set; }
        public string created_by { get; set; }
 
        public string modified_by { get; set; }
        [Required]
        public bool has_attr { get; set; }
        [NotMapped]
        public int attr_id { get; set; }
        [NotMapped]
        public string color { get; set; }
        [NotMapped]
        public string size { get; set; }
        [NotMapped]
        public decimal price { get; set; }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //  public virtual ICollection<Token> Tokens { get; set; }
    }
}