using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina
{
    public enum TypeEnum
    {
        RPC_OK = 0xa,
        RPC_FAIL = 0xb,
        RPC_NOTIFY = 0xc,
        RPC_HELO = 0xd,
        PULL_MD = 0xe,
        PULL_MD_RESULT = 0xf,
        PUSH_MD = 0x10,
        PUSH_MD_RESULT = 0x11,
// below messages are not implemented or not used by Lumina. Enjoy yourselves ;)
        GET_POP = 0x12,
        GET_POP_RESULT = 0x13,
        LIST_PEERS = 0x14,
        LIST_PEERS_RESULT = 0x15,
        KILL_SESSIONS = 0x16,
        KILL_SESSIONS_RESULT = 0x17,
        DEL_ENTRIES = 0x18,
        DEL_ENTRIES_RESULT = 0x19,
        SHOW_ENTRIES = 0x1a,
        SHOW_ENTRIES_RESULT = 0x1b,
        DUMP_MD = 0x1c,
        DUMP_MD_RESULT = 0x1d,
        CLEAN_DB = 0x1e,
        DEBUGCTL = 0x1f
    }
}
