package UDPsocket;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.util.Arrays;

public class serverUDP {

    public static void main(String args[]) throws Exception {
        // Tạo socket phía server với số hiệu cổng 1108
        DatagramSocket serverSocket = new DatagramSocket(1108);
        // tạo biến receiveData để nhận dữ liệu từ gói tin đến
        byte[] receiveData = new byte[1024];
        // tạo sendData để nhận dữ liệu gửi lên gói tin đi
        byte[] sendData = new byte[1024];
        while (true) {
            //
            // tạo biến receivePacket để nhận gói tin từ socket
            DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);
            // nhận gói tin qua phương thức receive()
            serverSocket.receive(receivePacket);
            // receiveData.clear();
            // Chuyển dữ liệu vừa nhận về dạng String
            String sentence = new String(receivePacket.getData());
            System.out.println("receivePacket: " + sentence + "\t length: " + sentence.length());
            //
            // Lấy địa chỉ IP của bên gửi
            InetAddress IPAddress = receivePacket.getAddress();
            // Lấy số hiệu cổng bên gửi
            int port = receivePacket.getPort();
            // Xử lý dữ liệu vừa nhận
            String sentence_to_client = sentence;

            // tạo gói tin để gửi đi client
            sendData = sentence_to_client.getBytes();
            System.out.println("server accpeted length: " + sendData.length);
            DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.length, IPAddress, port);
            // Gửi gói tin đi
            serverSocket.send(sendPacket);
            // sendData.clear();
            Arrays.fill(receiveData, (byte) 0);
            Arrays.fill(sendData, (byte) 0);
        }
    }
}
