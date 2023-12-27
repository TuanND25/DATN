using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models
{
	public class Category
	{

		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Status { get; set; }
		public virtual ICollection<ProductItems> ProductItems { get; set; }


	}
}
