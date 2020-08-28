using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace libloaderapi
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum) return;
            model.Enum.Clear();
            Enum.GetNames(context.Type)
                .ToList()
                .ForEach(n => model.Enum.Add(new OpenApiString(n)));
        }
    }
}