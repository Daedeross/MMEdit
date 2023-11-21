using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace MMEdit.Wpf.Utilities
{
    public class DecimalTypeConverter : IValueConverter
    {
        private readonly ISet<Type> _intLike = new HashSet<Type>
        {
            typeof(sbyte),
            typeof(byte),
            typeof(int),
            typeof(uint),
            typeof(short),
            typeof(ushort),
        };

        //public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        //{
        //    return _intLike.Contains(sourceType);
        //}

        //public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        //{
        //    return destinationType is null
        //        ? false
        //        : _intLike.Contains(destinationType);
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || !(_intLike.Contains(value.GetType())))
            {
                return default(decimal);
            }

            return System.Convert.ToDecimal(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(sbyte))
            {
                return System.Convert.ToSByte(value);
            }
            else if (targetType == typeof(byte))
            {
                return System.Convert.ToByte(value);
            }
            else if (targetType == typeof(int))
            {
                return System.Convert.ToInt32(value);
            }
            else if (targetType == typeof(uint))
            {
                return System.Convert.ToUInt32(value);
            }
            else if (targetType == typeof(short))
            {
                return System.Convert.ToInt16(value);
            }
            else if (targetType == typeof(ushort))
            {
                return System.Convert.ToUInt16(value);
            }
            else throw new InvalidCastException();
        }

        //public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        //{
        //    if (value is null) { return default(decimal); }

        //    return Convert.ToDecimal(value);
        //}

        //public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        //{
        //    if (destinationType == typeof(sbyte))
        //    {
        //        return Convert.ToSByte(value);
        //    }
        //    else if (destinationType == typeof(byte))
        //    {
        //        return Convert.ToByte(value);
        //    }
        //    else if (destinationType == typeof(int))
        //    {
        //        return Convert.ToInt32(value);
        //    }
        //    else if (destinationType == typeof(uint))
        //    {
        //        return Convert.ToUInt32(value);
        //    }
        //    else if(destinationType == typeof(short))
        //    {
        //        return Convert.ToInt16(value);
        //    }
        //    else if (destinationType == typeof(ushort))
        //    {
        //        return Convert.ToUInt16(value);
        //    }
        //    else throw new InvalidCastException();
        //}
    }
}
