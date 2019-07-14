using Core.Domain;
using Core.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class ExecutedQueryViewModel
    {
        public List<UserLogResponse> UserLogs { get; set; }
    }
}
