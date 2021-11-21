using Assembly_CSharp.Assets.Scripts.Components;
using UnityEngine;

namespace Assembly_CSharp.Assets.Scripts.Controller
{
    public class Level02Controller : MonoBehaviour
    {
        public ButtonDoor ButtonDoor;
        public DoorBarrier DoorBarrier;

        private void Start()
        {
            ButtonDoor.OnButtonPressed(DoorBarrier.Open);
            ButtonDoor.OnButtonUnpressed(DoorBarrier.Close);
        }
    }
}