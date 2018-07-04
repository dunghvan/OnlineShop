namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Display(Name = "ID")]
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name="商品名")]
        [Required(ErrorMessage ="商品の名前を入力してください")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "商品コード")]
        [Required(ErrorMessage = "商品コードを入力してください")]
        public string Code { get; set; }

        [StringLength(250)]
        [Display(Name = "メータタイトル")]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "説明")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "写真")]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        [Display(Name = "写真リスト")]
        public string MoreImages { get; set; }
        [Display(Name = "値段")]
        public decimal? Price { get; set; }

        [Display(Name = "税込")]
        public bool? IncludedVAT { get; set; }

        [Display(Name = "セール値段")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "在庫")]
        public int? Quanlity { get; set; }

        [Display(Name = "カテゴリー")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "詳細")]
        public string Detail { get; set; }

        [Display(Name = "保証")]
        public int? Warranty { get; set; }

        [Display(Name = "作成日時")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "作成者")]
        public long? CreateBy { get; set; }

        [Display(Name = "編集日時")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "編集者")]
        public long? ModifiedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "メータキワード")]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "メータタイトル")]
        public string MetaDescriptions { get; set; }

        [Display(Name = "状態")]
        public bool Status { get; set; }
        [Display(Name = "トップホット")]
        public DateTime? TopHot { get; set; }

        [Display(Name = "ビュー数")]
        public int? ViewCount { get; set; }
        [Display(Name = "言語")]
        public string Language { get; set; }
    }
}
