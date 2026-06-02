using Microsoft.Data.SqlClient;
using System.Data;

namespace Onyx.Service.Infrastructure.DataAccess.Extensions
{
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Gets a value from the reader using the enum member name as the column name.
        /// Returns default if the column is DBNull.
        /// </summary>
        public static T? Get<T, TEnum>(this SqlDataReader reader, TEnum column)
            where TEnum : Enum
        {
            string columnName = column.ToString();
            object value = reader[columnName];

            if (value is DBNull)
                return default;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Returns true if the column value is DBNull.
        /// </summary>
        public static bool IsNull<TEnum>(this SqlDataReader reader, TEnum column)
            where TEnum : Enum
            => reader[column.ToString()] is DBNull;
    }
}