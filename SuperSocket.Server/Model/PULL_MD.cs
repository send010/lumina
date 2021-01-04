using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class PULL_MD
    {
        public uint flags { get; set; }
        public List<int> ukn_list { get; set; }
        public List<func_sig_t> funcInfos { get; set; }
        public PULL_MD(byte[] buffer1)
        {
            var buffer = Unpooled.WrappedBuffer(buffer1);
            flags = (uint)buffer.packdd();

            var ukn_list_count = buffer.packdd();
            ukn_list = new List<int>();
            for (int i = 0; i < ukn_list_count; i++)
            {
                ukn_list.Add(buffer.packdd());
            }
            funcInfos = new List<func_sig_t>();
            var funcInfosCount = buffer.packdd();
            for (int i = 0; i < funcInfosCount; i++)
            {
                func_sig_t func_Sig_T = new func_sig_t();
                func_Sig_T.version = buffer.packdd();
                var length = buffer.packdd();
                var bytes = new byte[length];
                buffer.ReadBytes(bytes);
                func_Sig_T.signature = bytes;
                funcInfos.Add(func_Sig_T);
            }
        }
    }
}
