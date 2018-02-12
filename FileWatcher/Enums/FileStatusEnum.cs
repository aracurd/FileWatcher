using System.ComponentModel;
using FileWatcher.Resourses.Converters;

namespace FileWatcher.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum FileStatus : byte
    {
        [Description("Unknown")]
        Unknown = 1,
        [Description("Created")]
        Created,
        [Description("Updated")]
        Updated
    }

}