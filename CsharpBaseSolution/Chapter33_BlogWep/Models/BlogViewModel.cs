﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chapter33_BlogWep.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OverView { get; set; }
        public string AuthorName { get; set; }
    }
}