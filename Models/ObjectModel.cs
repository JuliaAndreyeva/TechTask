using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonTree.Models
{
    public class ObjectModel
    {
        [Key]
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string? Value { get; set; }
        public bool IsRoot { get; set; }
    }
}
