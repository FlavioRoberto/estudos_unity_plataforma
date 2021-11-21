using System.Collections;
using System.Collections.Generic;
using Assembly_CSharp.Assets.Scripts;
using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

public class Wateer : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D colider)
   {
       if(colider.CompareTag(ETag.PLAYER))
        GameController.Instance.GameOver();
   }
}
