using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwkBooking.Api.Middleware;

public class DateTimeHeader
{
    private readonly RequestDelegate _next;

    public DateTimeHeader(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Request.Headers.Add("my-middleware-header", DateTime.Now.ToString());
        await _next(httpContext);
    }
}
public static class DateTimeHeaderExtensions
{
    public static IApplicationBuilder UseDateTimeHeader(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DateTimeHeader>();
    }
}
