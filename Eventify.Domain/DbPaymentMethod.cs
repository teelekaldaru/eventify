using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventify.Domain
{
	[Table("PaymentMethod")]
	public class DbPaymentMethod
	{
		[Key]
		public string Name { get; set; }
	}
}
