﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.ViewModels
{
    public class GHnA
    {
        public ICollection<onlyreason> nobody { get; set; }
        public GHnA() 
        {
            this.nobody = new List<onlyreason>();
        }
    }
}