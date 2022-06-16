namespace BKSpeed
{
    public class BaseConstant
    {
        public const string IP_ADDRESS = "35.87.152.15";
        public const int PORT_SERVER = 2222;
        public const int SOCKET_UDP_SEND_TIMEOUT = 5000;
        public const int SOCKET_UDP_RECEIVE_TIMEOUT = 5000;
        public const int SOCKET_TCP_SEND_TIMEOUT = 5000;
        public const int SOCKET_TCP_RECEIVE_TIMEOUT = 5000;
        public const string SOCKET_EVENT_CREATE_ROOM = "CREATE_ROOM";
        public const string SOCKET_EVENT_CREATE_ROOM_SUCCESS = "CREATE_ROOM_SUCCESS";
        public const int MAX_BUFFER_SIZE = 4096;
        public const int MAX_USER_ROOM = 4;
    }
}