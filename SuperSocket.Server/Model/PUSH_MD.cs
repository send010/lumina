using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class PUSH_MD
    {
        public uint field_0x10 { get; set; }
        public string idb_filepath { get; set; }
        public string input_filepath { get; set; }
        public Byte[] input_md5 { get; set; }
        public string hostname { get; set; }
        public List<func_md_t> funcInfos { get; set; }
        public List<Int64> funcEas { get; set; }
        public PUSH_MD(byte[] buffer1)
        {
            var buffer = Unpooled.WrappedBuffer(buffer1);

            field_0x10 =(uint)buffer.packdd();

            idb_filepath = buffer.GetString();

            input_filepath = buffer.GetString();

            input_md5 = new byte[16];
            buffer.ReadBytes(input_md5);

            hostname = buffer.GetString();

            funcInfos = new List<func_md_t>();
            var funcInfosCount = buffer.packdd();
            for (int i = 0; i < funcInfosCount; i++)
            {
                func_md_t func_md_t = new func_md_t(buffer);
                funcInfos.Add(func_md_t);
            }

            var length = buffer.packdd();
            funcEas = new List<long>();
            for (int i = 0; i < length; i++)
            {
                funcEas.Add(buffer.packdq());
            }

        }
    }
}
