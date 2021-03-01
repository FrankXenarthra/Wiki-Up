﻿using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using System;

namespace WikiUpload
{
    public class JsonHtmlStringConverter : JsonConverter<string>
    {
        private readonly HtmlParser _parser;
        private readonly IHtmlDocument _parseContext;

        public JsonHtmlStringConverter()
        {
            _parser = new HtmlParser();
            _parseContext = _parser.ParseDocument("<html><body></body></html>");
        }

        public override bool CanWrite => false;

        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var nodes = _parser.ParseFragment((string)reader.Value, _parseContext.Body);
            var text = nodes[0].TextContent;
            if (text.EndsWith("\n"))
                text = text.Substring(0, text.Length - 1);
            return text;
        }

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
            => throw new NotImplementedException();
    }
}
