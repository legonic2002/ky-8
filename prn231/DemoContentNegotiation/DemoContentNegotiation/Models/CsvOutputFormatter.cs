using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Formats.Asn1;
using System.Globalization;
using System;
using System.Text;
using CsvHelper;
using System.Text.Json;
using System.Reflection.Metadata;
using System.Xml;

namespace DemoContentNegotiation.Models
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
            => typeof(Blog).IsAssignableFrom(type)
                || typeof(IEnumerable<Blog>).IsAssignableFrom(type);
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<Blog>)
            {
                foreach (var Blog in (IEnumerable<Blog>)context.Object)
                {
                    FormatCsv(buffer, Blog);
                }
            }
            else
            {
                FormatCsv(buffer, (Blog)context.Object);
            }
            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }
        private static void FormatCsv(StringBuilder buffer, Blog blog)
        {
            foreach (var blogPost in blog.BlogPosts)
            {
                buffer.AppendLine($"{blog.Name},\"{blog.Description},\"{blogPost.Title},\"{blogPost.Published}\"");
            }
        }

        /* public CsvOutputFormatter()
         {
             SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/xml"));
             SupportedEncodings.Add(Encoding.UTF8);
             SupportedEncodings.Add(Encoding.Unicode);
         }

         protected override bool CanWriteType(Type? type)
         {
             return typeof(Blog).IsAssignableFrom(type)
                 || typeof(IEnumerable<Blog>).IsAssignableFrom(type);
         }

         public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
         {
             var response = context.HttpContext.Response;
             var buffer = new StringBuilder();

             // Check the media type to determine the formatting approach
             var contentType = context.HttpContext.Request.Headers["Content-Type"].ToString();
             if (!string.IsNullOrEmpty(contentType) && contentType == "text/xml")
             {
                 if (context.Object is IEnumerable<Blog>)
                 {
                     foreach (var Blog in (IEnumerable<Blog>)context.Object)
                     {
                         FormatJson(buffer, Blog);
                     }

                 }
                 else
                 {
                     FormatJson(buffer, (Blog)context.Object);
                 }
             }

             await response.WriteAsync(buffer.ToString(), selectedEncoding);
         }
         private void FormatJson(StringBuilder buffer, Blog blog)
         {
             var options = new JsonSerializerOptions
             {
                 WriteIndented = true
             };

             var json = JsonSerializer.Serialize(blog, options);
             buffer.Append(json);
         }
         private void FormatXml(StringBuilder buffer, Blog blogs)
         {
             using (var writer = XmlWriter.Create(buffer, new XmlWriterSettings() { Indent = true }))
             {
                 writer.WriteStartDocument();
                 writer.WriteStartElement("Blogs");


                 writer.WriteStartElement("Blog");
                 writer.WriteElementString("Name", blogs.Name);
                 writer.WriteElementString("Description", blogs.Description);

                 foreach (var blogPost in blogs.BlogPosts)
                 {
                     writer.WriteStartElement("BlogPost");
                     writer.WriteElementString("Title", blogPost.Title);
                     writer.WriteElementString("Published", blogPost.Published.ToString());
                     writer.WriteEndElement();
                 }

                 writer.WriteEndElement();


                 writer.WriteEndElement();
                 writer.WriteEndDocument();
             }
         }*/


    }
}
