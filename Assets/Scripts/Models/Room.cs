using System.Collections.Generic;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace BKSpeed
{
    public class Room
    {
        [JsonProperty]
        private int id;
        [JsonProperty]
        private int password;
        [JsonProperty]
        private bool isWaiting;
        [JsonProperty]
        private string idMaster;
        [JsonProperty]
        private Dictionary<string, UserDataRealtime> userDataMap;
        [JsonProperty]
        private List<ChatMessage> chatting;
        [JsonProperty]
        private Socket roomSocket;
        [JsonProperty]
        private int portData;
        [JsonProperty]
        private int portRoom;

        public Room(int id, int password, string idMaster)
        {
            this.id = id;
            this.password = password;
            this.idMaster = idMaster;
        }
        public Room(int id, int password, bool isWaiting, string idMaster, Dictionary<string, UserDataRealtime> userDataMap, List<ChatMessage> chatting, Socket roomSocket, int portData, int portRoom)
        {
            this.id = id;
            this.password = password;
            this.isWaiting = isWaiting;
            this.idMaster = idMaster;
            this.userDataMap = userDataMap;
            this.chatting = chatting;
            this.roomSocket = roomSocket;
            this.portData = portData;
            this.portRoom = portRoom;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getPassword()
        {
            return this.password;
        }

        public void setPassword(int password)
        {
            this.password = password;
        }

        public bool isIsWaiting()
        {
            return this.isWaiting;
        }

        public void setIsWaiting(bool isWaiting)
        {
            this.isWaiting = isWaiting;
        }

        public string getIdMaster()
        {
            return this.idMaster;
        }

        public void setIdMaster(string idMaster)
        {
            this.idMaster = idMaster;
        }

        public List<ChatMessage> getChatting()
        {
            return this.chatting;
        }

        public void setChatting(List<ChatMessage> chatting)
        {
            this.chatting = chatting;
        }
        public Socket getRoomSocket()
        {
            return this.roomSocket;
        }

        public void setRoomSocket(Socket roomSocket)
        {
            this.roomSocket = roomSocket;
        }

        public int getPortData()
        {
            return this.portData;
        }

        public void setPortData(int portData)
        {
            this.portData = portData;
        }

        public int getPortRoom()
        {
            return this.portRoom;
        }

        public void setPortRoom(int portRoom)
        {
            this.portRoom = portRoom;
        }
    }
}
