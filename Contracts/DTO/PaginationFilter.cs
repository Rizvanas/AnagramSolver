﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DTO
{
    public class PaginationFilter
    {
        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}
