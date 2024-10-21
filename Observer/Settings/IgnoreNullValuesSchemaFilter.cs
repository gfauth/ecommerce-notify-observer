using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Observer.Settings
{
    public class IgnoreNullValuesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            var propertiesToRemove = new List<string>();
            foreach (var property in schema.Properties)
            {
                // Verifique se a propriedade é nula ou se o tipo da propriedade é um objeto e possui propriedades nulas
                if (property.Value.Type != "string" && (property.Value.Nullable || property.Value.Type == "object" && IsObjectNullable(property.Value)))
                    propertiesToRemove.Add(property.Key);
            }

            foreach (var property in propertiesToRemove)
                schema.Properties.Remove(property);
        }

        private bool IsObjectNullable(OpenApiSchema property)
        {
            if (property.Properties == null)
                return false; // Se não houver propriedades, o objeto não é anulável

            foreach (var prop in property.Properties)
            {
                if (prop.Value.Nullable || prop.Value.Type == "object" && IsObjectNullable(prop.Value) && prop.Value.Type != "string")
                    return true; // Se alguma propriedade for anulável ou se um objeto aninhado for anulável, retorna true
            }

            return false;
        }
    }
}