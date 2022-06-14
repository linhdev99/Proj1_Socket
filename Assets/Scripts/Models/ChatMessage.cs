using Newtonsoft.Json;

namespace BKSpeed
{
    public class ChatMessage
    {
        [JsonProperty]
        private string id;
        [JsonProperty]
        private string userId;
        [JsonProperty]
        private string message;

        public ChatMessage(string id, string userId, string message)
        {
            this.id = id;
            this.userId = userId;
            this.message = message;
        }

        public ChatMessage()
        {
        }

        public string getId()
        {
            return this.id;
        }

        public void setId(string id)
        {
            this.id = id;
        }

        public string getUserId()
        {
            return this.userId;
        }

        public void setUserId(string userId)
        {
            this.userId = userId;
        }

        public string getMessage()
        {
            return this.message;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }

    }
}