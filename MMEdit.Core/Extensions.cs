namespace MMEdit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Extensions
    {
        public static byte GetByte(this byte[] data, DatumInfo info)
        {
            if (info.Width != 1) throw new ArgumentOutOfRangeException(nameof(info));
            return data[info.Offset];
        }

        public static ushort GetUInt16(this byte[] data, DatumInfo info)
        {
            if (info.Width > 2 || info.Width < 1) throw new ArgumentOutOfRangeException(nameof(info));
            return info.Width == 1
                ? data[info.Offset]
                : BitConverter.ToUInt16(data, info.Offset);
        }

        public static uint GetUInt32(this byte[] data, DatumInfo info)
        {
            return (info.Width) switch
            {
                4 => BitConverter.ToUInt32(data, info.Offset),
                2 => BitConverter.ToUInt16(data, info.Offset),
                1 => data[info.Offset],
                _ => throw new ArgumentNullException(nameof(info.Width))
            };
        }

        public static string GetString(this byte[] data, DatumInfo info)
        {
            return Encoding.ASCII.GetString(data, info.Offset, info.Width);
        }

        public static T[] Slice<T>(this byte[] data, DatumInfo[] infos, Func<byte[], DatumInfo, T> getter)
            where T : struct
        {
            var result = new T[infos.Length];

            for (int i = 0; i < infos.Length; i++)
            {
                result[i] = getter(data, infos[i]);
            }

            return result;
        }

        public static byte[] SliceBytes(this byte[] data, DatumInfo[] infos)
        {
            return Slice(data, infos, GetByte);
        }

        public static void SetByte(this byte[] data, byte b, DatumInfo info)
        {
            if (info.Width > 1) throw new ArgumentOutOfRangeException(nameof(info));
            data[info.Offset] = b;
        }

        public static void SetUInt16(this byte[] data, ushort s, DatumInfo info)
        {
            if (info.Width == 1)
            {
                data[info.Offset] = BitConverter.GetBytes(s)[0];
            }
            else if (info.Width == 2)
            {
                Array.Copy(BitConverter.GetBytes(s), 0, data, info.Offset, (int)info.Width);
            }
            else throw new ArgumentOutOfRangeException(nameof(info));
        }

        public static void SetUInt32(this byte[] data, uint i, DatumInfo info)
        {
            if (info.Width > 4 || info.Width < 1) throw new ArgumentOutOfRangeException(nameof(info.Width));
            Array.Copy(BitConverter.GetBytes(i), 0, data, info.Offset, info.Width);
        }

        public static void SetString(this byte[] data, string s, DatumInfo info)
        {
            string output = s.Length < info.Width
                ? s.PadRight(info.Width, (char)0x20)
                : s[..info.Width];

            Array.Copy(Encoding.ASCII.GetBytes(output), 0, data, info.Offset, info.Width);
        }

        public static void UnSlice<T>(this byte[] data, IEnumerable<(T, DatumInfo)> source, Action<byte[], T, DatumInfo> setter)
            where T : struct
        {
            foreach (var (item, info) in source)
            {
                setter(data, item, info);
            }
        }

        public static void UnSlice<T>(this byte[] data, ICollection<T> source, ICollection<DatumInfo> infos, Action<byte[], T, DatumInfo> setter)
            where T : struct
        {
            if (source.Count != infos.Count) throw new ArgumentException($"{nameof(source)} and {nameof(infos)} must have the same count.");

            UnSlice(data, source.Zip(infos, (s, i) => (s, i)), setter);
        }

        public static void UnSliceBytes(this byte[] data, ICollection<byte> source, ICollection<DatumInfo> infos)
        {
            UnSlice(data, source, infos, SetByte);
        }
    }
}
