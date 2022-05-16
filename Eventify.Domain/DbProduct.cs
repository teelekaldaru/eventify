using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventify.Domain
{
    [Table("Event")]
    public class DbEvent
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
