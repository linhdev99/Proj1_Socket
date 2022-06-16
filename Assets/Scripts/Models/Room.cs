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
        private int portData;
        [JsonProperty]
        private int portRoom;
        public Room(
            int id,
            int password,
            bool isWaiting,
            string idMaster,
            Dictionary<string, UserDataRealtime> userDataMap,
            List<ChatMessage> chatting,
            int portData,
            int portRoom
        )
        {
            this.id = id;
            this.password = password;
            this.isWaiting = isWaiting;
            this.idMaster = idMaster;
            this.userDataMap = userDataMap;
            this.chatting = chatting;
            this.portData = portData;
            this.portRoom = portRoom;
        }

        public Room(int password, string idMaster)
        {
            this.password = password;
            this.idMaster = idMaster;
        }

        public Room()
        {
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
        public Dictionary<string, UserDataRealtime> getUserDataMap()
        {
            return this.userDataMap;
        }

        public void setUserDataMap(Dictionary<string, UserDataRealtime> userDataMap)
        {
            this.userDataMap = userDataMap;
        }

        public List<ChatMessage> getChatting()
        {
            return this.chatting;
        }

        public void setChatting(List<ChatMessage> chatting)
        {
            this.chatting = chatting;
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
