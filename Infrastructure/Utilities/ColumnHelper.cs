using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using mambuquery.api.Core.Models;

namespace mambuquery.api.Infrastructure.Utilities
{
    public static class ColumnHelper<T>
    {
        public static List<DataField> GetFields()
        {
            var propertyInfos = typeof(T).GetProperties();
            var dataTypeMap = new Dictionary<string, string>
            {
                {"Int64", "number"},
                {"Int32", "number"},
                {"Decimal", "number"},
                {"String", "text"},
                {"DateTime", "date"},
            };
            var fields = new List<DataField>();
            foreach (var info in propertyInfos)
            {
                Debug.WriteLine(info.PropertyType.Name);
                var type = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;
                fields.Add(new DataField
                {
                    Name = info.Name,
                    Label = GetPropertyDescription(info.Name),
                    DataType = dataTypeMap[type.Name]
                });
            }
            return fields;
        }

        private static string GetPropertyDescription(string propertyName)
        {
            AttributeCollection attributes =
                TypeDescriptor.GetProperties(typeof(T))[propertyName].Attributes;
            DescriptionAttribute attribute =
                (DescriptionAttribute)attributes[typeof(DescriptionAttribute)];

            return attribute?.Description ?? propertyName;
        }
    }
}