using System;
using System.ComponentModel;
using System.Linq;
using UNICAP.SiteCurso.Application.DTOs.EnumFolder;

namespace UNICAP.SiteCurso.Application.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                return null;
            }

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs.First()).Description;
                }
            }

            return description;
        }

        public static string GetDescription(this Enum enumValue)
        {

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs.First()).Description;
                }
            }

            return description;
        }

        public static EnumDTO GetDescriptionAndValue(this Enum enumValue)
        {
            var enumDTO = new EnumDTO();
            var value = Convert.ChangeType(enumValue, Enum.GetUnderlyingType(enumValue.GetType()));
            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs.First()).Description;
                    enumDTO.Id = (int)value;
                    enumDTO.Name = description;
                }
            }

            return enumDTO;
        }

        public static bool IsDefined<T>(this T enumValue)
            where T : struct, IConvertible
        {
            return Enum.IsDefined(typeof(T), enumValue);
        }
    }
}
