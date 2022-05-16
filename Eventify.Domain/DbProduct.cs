using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventify.Domain
{
    [Table("Product")]
    public class DbProduct
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
