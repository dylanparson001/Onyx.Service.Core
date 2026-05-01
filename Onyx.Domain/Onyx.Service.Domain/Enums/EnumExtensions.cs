using System.ComponentModel;
using System.Reflection;

namespace Onyx.Service.Domain.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            // Get the type of the enum
            Type type = value.GetType();

            // Get the member information for the specific enum value
            MemberInfo[] memberInfo = type.GetMember(value.ToString());

            if (memberInfo.Length > 0)
            {
                // Try to find the DescriptionAttribute
                var attribute = memberInfo[0].GetCustomAttribute<DescriptionAttribute>();

                if (attribute != null)
                {
                    return attribute.Description;
                }
            }

            // Fallback: return the enum's name if no description is found
            return value.ToString();
        }
    }
}
