using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class func_info_t
    {
        public func_metadata MetaData { get; set; }
        public int Popularity { get; set; }

        public func_info_t()
        {
            MetaData = new func_metadata();
        }
        public byte[] GetBytes()
        {
            var buffer = Unpooled.Buffer();

            buffer.WriteBytes(MetaData.GetBytes());

            buffer.unpackdd(Popularity);

            var by = new Byte[buffer.ReadableBytes];
            buffer.ReadBytes(by);
            return by;
        }
    }
}
