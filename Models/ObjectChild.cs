using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonTree.Models
{
    public class ObjectChild
    {
        //[Key]
        [ForeignKey("ParentObject")]

        public int ParentId { get; set; }
        [Key]
        [ForeignKey("ChildObject")]
        public int ChildId { get; set; }

        public virtual ObjectModel ParentObject { get; set; }
        public virtual ObjectModel ChildObject { get; set; }
    }
}

