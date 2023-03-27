﻿using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace JwtWithIdentityDemo.WebApi.Responses
{
    public class BadRequestResponse : IActionResult
    {
        public int Status { get;} = StatusCodes.Status400BadRequest;
        public string Message { get; set; }
        public IDictionary<string, List<string>> Errors { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(new
            {
                Status,
                Message,
                Errors
            })
            {
                StatusCode = Status,
                ContentTypes = new MediaTypeCollection { "application/json" }
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
