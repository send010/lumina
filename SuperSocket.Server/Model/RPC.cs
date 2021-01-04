using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class RPC
    {
        public uint Length { get; set; }
        public TypeEnum Code { get; set; }
        //public int Version { get; set; }

        public List<byte> Data { get; set; }
        public RPC(IByteBuffer buffer)
        {
            Length = buffer.ReadUnsignedInt();
            if (Length == 0)
            {
                return;
            }
            var code = buffer.ReadByte();
            Code = (TypeEnum)(code & 0xff);
            //Version = buffer.ReadByte();
        }
        public RPC()
        {
        }
        public RPC(byte[] buffer1)
        {
            var buffer = Unpooled.WrappedBuffer(buffer1);
            Length = buffer.ReadUnsignedInt();
            if (Length == 0)
            {
                return;
            }
            var code = buffer.ReadByte();
            Code = (TypeEnum)(code & 0xff);
            //Version = buffer.ReadByte();

            var leng = buffer1.Length - 4 - 1;
            var by = new byte[leng];
            buffer.ReadBytes(by);
            Data = by.ToList();
        }


        public byte[] GetBytes()
        {
            var buffer = Unpooled.Buffer();
            buffer.WriteInt(Data.Count());
            buffer.WriteByte((int)Code);
            buffer.WriteBytes(Data.ToArray());
            var by = new Byte[buffer.ReadableBytes];
            buffer.ReadBytes(by);
            return by;
        }
    }
}
