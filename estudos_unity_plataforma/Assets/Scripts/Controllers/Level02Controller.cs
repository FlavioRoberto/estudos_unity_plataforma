using Assembly_CSharp.Assets.Scripts.Components;
using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Controller
{
    public class Level02Controller : MonoBehaviour
    {
        public ButtonDoor ButtonDoor;
        public DoorBarrier DoorBarrier;
        public Transform playerPosition;

        private void Start()
        {
            GameController.Instance.SetCheckPoint(playerPosition);

            var player = GameObject.FindGameObjectWithTag(ETag.PLAYER).transform;

            if (player != null)
                player.position = playerPosition.position;

            ButtonDoor.OnButtonPressed(DoorBarrier.Open);
            ButtonDoor.OnButtonUnpressed(DoorBarrier.Close);
        }
    }
}