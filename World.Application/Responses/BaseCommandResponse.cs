using System;
using System.Collections.Generic;
using System.Text;

namespace World.Application.Responses
{
    public class BaseCommandResponse
    {
        public Guid Id { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
