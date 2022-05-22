using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventify.Domain
{
	[Table("PaymentMethod")]
	public class DbPaymentMethod
	{
		[Key]
        [MaxLength(256)]
		public string Name { get; set; }
	}
}
