using System.Reflection;
using Dapper;
using Travaloud.Core.Attributes;

namespace Travaloud.Core.Mappings
{
    public class DapperMappings<T>
    {
        public static void MapEntity()
        {
            var map = new CustomPropertyTypeMap(typeof(T), (type, columnName) => type.GetProperties().FirstOrDefault(prop => GetDapperColumnFromAttribute(prop) == columnName.ToLower()));
            SqlMapper.SetTypeMap(typeof(T), map);
        }

        private static string GetDapperColumnFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var dapperColumnMapAttribute = (DapperColumnMapAttribute)Attribute.GetCustomAttribute(member, typeof(DapperColumnMapAttribute), false);
            return (dapperColumnMapAttribute?.ColumnName ?? member.Name).ToLower();
        }
    }
}

