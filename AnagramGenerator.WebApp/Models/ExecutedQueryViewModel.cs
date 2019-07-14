using Core.DTO.Responses;
using System.Collections.Generic;

namespace AnagramGenerator.WebApp.Models
{
    public class ExecutedQueryViewModel
    {
        public List<UserLogResponse> UserLogs { get; set; }
    }
}
