﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "必须提供姓名")]
        [MaxLength(20, ErrorMessage = "姓名的最大长度为20个字符")]
        public string Name { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string BirthPlace { get; set; }

        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
    }
}
