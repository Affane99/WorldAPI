using Microsoft.AspNetCore.Mvc;

namespace World.Api.Models
{
    public class CustomProblemDetails : ProblemDetails
    {
        public string Errors { get; set; } = "";
    }
}
