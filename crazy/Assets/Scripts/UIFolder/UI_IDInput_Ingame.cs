using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IDInput_Ingame : IDMgr
{
    public Text textPlayer_1;
    public Text textPlayer_2;

    protected override void Awake()
    {
        base.Awake();           // ID ·Îµå

        textPlayer_1.text = strPlayer_1;
        textPlayer_2.text = strPlayer_2;
    }
}
