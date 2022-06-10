using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class NetworkRoom
{
    string id;
    Dictionary<string, OtherClient> room;
    public NetworkRoom()
    {
        id = CreateID();
        room = new Dictionary<string, OtherClient>();
    }
    public string ID()
    {
        return id;
    }
    public Dictionary<string, OtherClient> Room()
    {
        return room;
    }
    public void JoinRoom(OtherClient client)
    {
        if (room.ContainsKey(client.clientID))
        {
            return;
        }
        room.Add(client.clientID, client);
    }
    public void ExitRoom(string id)
    {
        if (room.ContainsKey(id))
        {
            room.Remove(id);
        }
    }
    public void ChangeDataUser(OtherClient client)
    {
        if (room.ContainsKey(client.clientID))
        {
            room[client.clientID] = client;
        }
    }
    public string CreateID()
    {
        string value = DateTime.Now.ToString();
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }
}
