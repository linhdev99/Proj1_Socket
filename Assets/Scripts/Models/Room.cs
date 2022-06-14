using System.Collections.Generic;
using System.Net.Sockets;

namespace BKSpeed
{
    public class Room
    {
        private int id;
        private int password;
        private bool isWaiting;
        private string idMaster;

        private Dictionary<string, UserDataRealtime> userDataMap;
        private List<ChatMessage> chatting;
        private Socket roomSocket;
        private int portData;   // only receive
        private int portRoom;	// only send
    }
}
