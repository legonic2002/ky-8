﻿using DemoJsonMediaTypeFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

public class VcardInputFormatter : TextInputFormatter
{
    public VcardInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanReadType(Type type)
        => type == typeof(Product);

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(
        InputFormatterContext context, Encoding effectiveEncoding)
    {
        var httpContext = context.HttpContext;
        var serviceProvider = httpContext.RequestServices;

        var logger = serviceProvider.GetRequiredService<ILogger<VcardInputFormatter>>();

        using var reader = new StreamReader(httpContext.Request.Body, effectiveEncoding);
        string? nameLine = null;

        try
        {
            await ReadLineAsync("BEGIN:VCARD", reader, context, logger);
            await ReadLineAsync("VERSION:", reader, context, logger);

            nameLine = await ReadLineAsync("N:", reader, context, logger);

            var split = nameLine.Split(";".ToCharArray());
            var contact = new Product(1,"Tester",111);

            await ReadLineAsync("FN:", reader, context, logger);
            await ReadLineAsync("END:VCARD", reader, context, logger);

            logger.LogInformation("nameLine = {nameLine}", nameLine);

            return await InputFormatterResult.SuccessAsync(contact);
        }
        catch
        {
            logger.LogError("Read failed: nameLine = {nameLine}", nameLine);
            return await InputFormatterResult.FailureAsync();
        }
    }

    private static async Task<string> ReadLineAsync(
        string expectedText, StreamReader reader, InputFormatterContext context,
        ILogger logger)
    {
        var line = await reader.ReadLineAsync();

        if (line is null || !line.StartsWith(expectedText))
        {
            var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

            context.ModelState.TryAddModelError(context.ModelName, errorMessage);
            logger.LogError(errorMessage);

            throw new Exception(errorMessage);
        }

        return line;
    }
}