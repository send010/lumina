using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class RPC_HELO
    {
        public uint protocole { get; set; }
        public string hexrays_licence { get; set; }
        public uint hexrays_id { get; set; }
        public uint watermak { get; set; }
        public uint field_0x36 { get; set; }
        public RPC_HELO(byte[] buffer1)
        {
            var buffer= Unpooled.WrappedBuffer(buffer1);
            protocole = (uint)PackHelper.packdd(buffer);
            var length = PackHelper.packdd(buffer);
            hexrays_licence = buffer.ReadString(length, Encoding.UTF8);
            hexrays_id = buffer.ReadUnsignedInt();
            watermak = buffer.ReadUnsignedShort();
            field_0x36 = (uint)PackHelper.packdd(buffer);
        }
    }
}
