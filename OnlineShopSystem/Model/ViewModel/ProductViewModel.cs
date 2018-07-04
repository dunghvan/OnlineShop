using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductViewModel
    {
        public long ID { get; set; }

        
        public string Name { get; set; }

       
        public string Code { get; set; }

      
        public string MetaTitle { get; set; }

        public string Description { get; set; }

        
        public string Image { get; set; }

        
        public string MoreImages { get; set; }

        public decimal? Price { get; set; }

        public bool? IncludedVAT { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? Quanlity { get; set; }

        public string CategoryName { get; set; }

      
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        
        public long? ModifiedBy { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
        public string Language { get; set; }
    }
}
