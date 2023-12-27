using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models
{
	public class ProductItems
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public Guid? ColorId { get; set; }
		public Guid? SizeId { get; set; }
		public Guid CategoryId { get; set; }
		public int? AvaiableQuantity { get; set; }
		public int? PriceAfterReduction { get; set; }
		public int? CostPrice { get; set; }
		public int Status { get; set; }

		public Products Products { get; set; }
		public Category Categorys { get; set; }
		public Color Colors { get; set; }
		public Size Size { get; set; }
		public virtual ICollection<Image> Images { get; set; }
		public virtual ICollection<CartItems> CartItems { get; set; }
		public virtual ICollection<BillItems> BillItems { get; set; }
		public virtual ICollection<PromotionsItem> PromotionsProducts { get; set; }
	}
}
