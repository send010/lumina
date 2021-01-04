using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSocket.Lumina
{
    public static class PackHelper
    {
        private static List<KeyValuePair<int, int>> Data { get; set; }
        static PackHelper()
        {
            Data = new List<KeyValuePair<int, int>>();
            Data.Add(new KeyValuePair<int, int>(0, 0xff));
            Data.Add(new KeyValuePair<int, int>(0, 0xff));
            Data.Add(new KeyValuePair<int, int>(0, 0xff));
            Data.Add(new KeyValuePair<int, int>(0, 0xff));
            Data.Add(new KeyValuePair<int, int>(1, 0x7f));
            Data.Add(new KeyValuePair<int, int>(1, 0x7f));
            Data.Add(new KeyValuePair<int, int>(3, 0x3f));
            Data.Add(new KeyValuePair<int, int>(4, 0x00));
        }

        public static int packdd(this IByteBuffer buffer)
        {
            var b = (int)buffer.ReadByte();

            var data1 = Data[b >> 5];

            var extrabytes = data1.Key;
            var mask = data1.Value;

            var num = b & mask;

            for (int i = 0; i < extrabytes; i++)
            {
                var b1 = (int)buffer.ReadByte();
                num = (num << 8) + b1;
            }
            return num;
        }
        public static Int64 packdq(this IByteBuffer buffer)
        {
            var low = packdd(buffer);
            var high = packdd(buffer);
            Int64 num = (high << 32) | low;
            return num;
        }

        public static string GetString(this IByteBuffer buffer)
        {
            int length = 0;
            foreach (var item in buffer.Array.Skip(buffer.ReaderIndex))
            {
                if (item == 0x0)
                {
                    break;
                }
                length++;
            }
            var str = buffer.ReadString(length, Encoding.UTF8);
            buffer.ReadByte();
            return str;
        }

        public static int unpackdd(this IByteBuffer buffer, int obj)
        {
            var x = (long)obj;
            int nbytes = 0;
            if (x > 0x1FFFFFFF)
            {

                x |= 0xFF00000000;
                nbytes = 5;
            }
            else if (x > 0x3FFF)
            {
                x |= 0xC0000000;
                nbytes = 4;
            }
            else if (x > 0x7F)
            {
                x |= 0x8000;
                nbytes = 2;
            }
            else
            {
                nbytes = 1;
            }
            for (int i = nbytes; i > 0; i--)
            {
                //stream_write(stream, int2byte((x >> (8 * (i - 1))) & 0xFF), 1, path)
                buffer.WriteByte((byte)(x >> (8 * (i - 1)) & 0xFF));
            }

            return obj;
        }


    }
}
