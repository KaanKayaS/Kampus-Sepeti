﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string UserImage { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
