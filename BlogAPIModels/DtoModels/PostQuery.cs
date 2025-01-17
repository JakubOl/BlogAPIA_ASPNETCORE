﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIModels.DtoModels
{
    public class PostQuery
    {
        public string SearchPhrase { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
        public string SortBy { get; set; } = string.Empty;
        public bool SortDirection { get; set; } = false;
    }
}
