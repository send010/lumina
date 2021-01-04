using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class PULL_MD_RESULT
    {
        public List<int> Found { get; set; }
        public List<func_info_t> Result { get; set; }

        public PULL_MD_RESULT()
        {
            Found = new List<int>();
            Result = new List<func_info_t>();
        }
        public byte[] GetBytes()
        {
            var buffer = Unpooled.Buffer();

            buffer.unpackdd(Found.Count);
            foreach (var item in Found)
            {
                buffer.WriteByte(item);
            }
            buffer.unpackdd(Result.Count);
            foreach (var item in Result)
            {
                buffer.WriteBytes(item?.GetBytes());
            }
            var by = new Byte[buffer.ReadableBytes];
            buffer.ReadBytes(by);
            return by;
        }
    }
}
