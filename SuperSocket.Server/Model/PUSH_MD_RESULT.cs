using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class PUSH_MD_RESULT
    {
        public List<int> ResultsFlags { get; set; }

        public PUSH_MD_RESULT()
        {
            ResultsFlags = new List<int>();
        }
        public byte[] GetBytes()
        {
            var buffer = Unpooled.Buffer();

            buffer.unpackdd(ResultsFlags.Count);
            foreach (var item in ResultsFlags)
            {
                buffer.WriteByte(item);
            }
            var by = new Byte[buffer.ReadableBytes];
            buffer.ReadBytes(by);
            return by;
        }
    }
}
