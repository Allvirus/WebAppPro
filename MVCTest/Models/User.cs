using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models
{
    public class User
    {
        public int ID { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "密码")]
        public string UserPwd { get; set; }
    }
}
