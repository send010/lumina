using SuperSocket.Lumina.Model;
using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina
{
    public class MyPackageFilter : FixedHeaderPipelineFilter<RPC>
    {
        /// <summary>
        /// Header size is 5
        /// 1: OpCode
        /// 2-3: body length
        /// 4-5: sequence
        /// </summary>
        public MyPackageFilter()
            : base(5)
        {

        }

        protected override int GetBodyLengthFromHeader(ref ReadOnlySequence<byte> buffer)
        {
            var reader = new SequenceReader<byte>(buffer);
            var array = buffer.ToArray();
            if (array[3] != 0xa && array[4] == 0xe)
            {
                //reader.Advance(1);
            }
            //reader.Advance(1); // skip the first byte for OpCode
            reader.TryReadBigEndian(out int len);
            //reader.Advance(2); // skip the two bytes for Sequence

            return len;
        }

        protected override RPC DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            return new RPC(buffer.ToArray());
            //return buffer.ToArray();
        }
    }
}
