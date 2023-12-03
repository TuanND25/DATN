using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
	public class ProductItem_Show_VM
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string ProductCode { get; set; }
		public string Name { get; set; }
		public Guid? ColorId { get; set; }
		public string ColorName { get; set; }
		public Guid? SizeId { get; set; }
		public string SizeName { get; set; }
		public Guid CategoryID { get; set; }
		public string CategoryName { get; set; }
		public Guid PromotionItemId { get; set; }
		public int?AvaiableQuantity { get; set; }
		public int? PriceAfterReduction { get; set; }
		public int? CostPrice { get; set; }
		public int Status { get; set; }
	}
}
