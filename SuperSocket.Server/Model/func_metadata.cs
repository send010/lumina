using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class func_metadata
    {
        public func_metadata(IByteBuffer buffer)
        {
            func_name = buffer.GetString();

            func_size = buffer.packdd();

            var length = buffer.packdd();

            serialized_data = new byte[length];

            buffer.ReadBytes(serialized_data);
        }

        public func_metadata()
        {
            func_name = "";

            func_size = 0;

            var length = 0;

            serialized_data = new byte[length];
        }


        public byte[] GetBytes()
        {
            var buffer = Unpooled.Buffer();

            buffer.WriteString(func_name, Encoding.UTF8);
            buffer.WriteByte(0x00);
            buffer.unpackdd(func_size);

            buffer.unpackdd(serialized_data.Length);

            buffer.WriteBytes(serialized_data);

            var by = new Byte[buffer.ReadableBytes];
            buffer.ReadBytes(by);
            return by;
        }

        public string func_name { get; set; }
        public int func_size { get; set; }
        public byte[] serialized_data { get; set; }
    }
}
