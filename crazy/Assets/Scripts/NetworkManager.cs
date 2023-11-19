using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text rankText1;
    public Text rankText2;
    public Text rankText3;
    public Text rankScore1;
    public Text rankScore2;
    public Text rankScore3;


    void Awake()
    {
        Ranking();
    }

    void Start()
    {
        ConnectToPhoton();
    }

    void Ranking()
    {

    }


    void ConnectToPhoton()
    {
        Debug.Log("Connecting to Photon Server...");

        // 포톤 서버에 연결
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("포톤 서버 연결 완료");

        // 연결되면 로비로 입장
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Disconnected from Photon Server. Reason: {cause}");

        // 연결이 끊기면 다시 연결 시도
        ConnectToPhoton();
    }


}
