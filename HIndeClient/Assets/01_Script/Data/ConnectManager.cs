using System;
using System.Collections;
using UnityEngine;

    public class ConnectManager : MonoBehaviour
    {
        static string local = "127.0.0.1/";
        static string aws = "52.78.19.47/";
        static string GateName = "ServerGate/";

        void Awake()
        {
            StartCoroutine(Send_Get("testUserInfo"));
        }

        static public string ServerURL = local + GateName;

        static public IEnumerator Send_Get(string _addUrl)
        {
            using (WWW www = new WWW(ServerURL + _addUrl))
            {
                yield return www;

                Recv_Packet(www);
                
            }
            yield break;
        }

        static public void Send_Post(byte[] _postdata)
        {

        }

        static public void Recv_Packet(WWW recv)
        {
            string hi = JsonUtility.FromJson<JsonPacket.UserInfo>(recv.bytes.ToString()).ToString();
            Debug.Log(hi);
        }
    }