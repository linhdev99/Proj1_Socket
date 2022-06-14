using Newtonsoft.Json;

namespace BKSpeed
{
    public class UserDataRealtime
    {
        [JsonProperty]
        private string clientId;
        [JsonProperty]
        private bool isConnected;
        [JsonProperty]
        private ClientTransform clientTransform;

        public UserDataRealtime(string clientId, bool isConnected, ClientTransform clientTransform = null)
        {
            this.clientId = clientId;
            this.isConnected = isConnected;
            this.clientTransform = clientTransform;
        }

        public UserDataRealtime()
        {
        }

        public string getclientId()
        {
            return this.clientId;
        }

        public void setclientId(string clientId)
        {
            this.clientId = clientId;
        }

        public bool getIsConnected()
        {
            return this.isConnected;
        }

        public void setIsConnected(bool isConnected)
        {
            this.isConnected = isConnected;
        }
        public ClientTransform getClientTransform()
        {
            return this.clientTransform;
        }

        public void setClientTransform(ClientTransform clientTransform)
        {
            this.clientTransform = clientTransform;
        }
    }
}