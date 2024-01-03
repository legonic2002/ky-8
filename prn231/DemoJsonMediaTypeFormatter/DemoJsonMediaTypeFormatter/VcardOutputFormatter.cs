using DemoJsonMediaTypeFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

public class VcardOutputFormatter : TextOutputFormatter
{
    public VcardOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type? type)
        => typeof(Product).IsAssignableFrom(type)
            || typeof(IEnumerable<Product>).IsAssignableFrom(type);

    public override async Task WriteResponseBodyAsync(
        OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var httpContext = context.HttpContext;
        var serviceProvider = httpContext.RequestServices;

        var logger = serviceProvider.GetRequiredService<ILogger<VcardOutputFormatter>>();
        var buffer = new StringBuilder();

        if (context.Object is IEnumerable<Product> contacts)
        {
            foreach (var contact in contacts)
            {
                FormatVcard(buffer, contact, logger);
            }
        }
        else
        {
            FormatVcard(buffer, (Product)context.Object!, logger);
        }

        await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
    }

    private static void FormatVcard(
        StringBuilder buffer, Product contact, ILogger logger)
    {
        buffer.AppendLine("BEGIN:VCARD");
        buffer.AppendLine("VERSION:2.1");
        buffer.AppendLine($"N:{contact.Id};{contact.Id}");
        buffer.AppendLine($"FN:{contact.Name} {contact.Name}");
        buffer.AppendLine($"UID:{contact.Id}");
        buffer.AppendLine("END:VCARD");
        buffer.AppendLine("===========================");
    }
}