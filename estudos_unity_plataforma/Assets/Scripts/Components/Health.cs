using Assembly_CSharp.Assets.Scripts.Enums;
using Assembly_CSharp.Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Player _player;
    public List<Image> Hearths;
    public Sprite Hearth;
    public Sprite EmptyHearth;
    private int HeartsCount = 3;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(ETag.PLAYER).GetComponent<Player>();
        _player.SetHealth(HeartsCount);
    }

    void Update()
    {
        foreach(var hearth in Hearths)
        {
            var index = Hearths.IndexOf(hearth);
            var showHearth = (HeartsCount - index) > 0;

            hearth.sprite = _player.Health > index ? Hearth : EmptyHearth;
            hearth.enabled = showHearth;
        }
    }
}
