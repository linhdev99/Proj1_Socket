using Newtonsoft.Json;

namespace BKSpeed
{
    public class MessageSocket
    {
        [JsonProperty]
        private string action;
        [JsonProperty]
        private string value;

        public MessageSocket(string action, string value)
        {
            this.action = action;
            this.value = value;
        }

        public MessageSocket()
        {
        }

        public string getAction()
        {
            return this.action;
        }

        public void setAction(string action)
        {
            this.action = action;
        }

        public string getValue()
        {
            return this.value;
        }

        public void setValue(string value)
        {
            this.value = value;
        }

    }
}