using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopSystem.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }
        [Display(Name = "ユーザID")]
        [Required(ErrorMessage ="ユーザIDを入力してください！")]
       
        public string Username { set; get; }

        [Display(Name = "パスワード")]
        [Required(ErrorMessage = "パスワードを入力してください！")]
        [StringLength(20,MinimumLength =8, ErrorMessage ="パスワードは８文字以上")]
        public string Password { get; set; }

        [Display(Name = "パスワード確認")]
        [Required(ErrorMessage = "パスワードを入力してください！")]
        [Compare("Password", ErrorMessage ="パスワードが間違います。")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "氏名")]

        [Required(ErrorMessage = "氏名を入力してください！")]
        public string Name { set; get; }

        [Display(Name = "住所")]
        public string Address { set; get; }

        [Display(Name = "メールアドレス")]
        [Required(ErrorMessage = "メールアドレスを入力してください！")]
        public string Email { set; get; }

        [Display(Name = "電話番号")]
        public string Phone { set; get; }

    }
}