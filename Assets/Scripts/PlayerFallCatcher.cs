using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace KinematicCharacterController.Examples
{
    public class PlayerFallCatcher : MonoBehaviour
    {

        public GameObject Player;
        public int respawnLayer = -200;

        private void Update()
        {
            if(Player.transform.position.y <= respawnLayer)
            {
                Player.GetComponent<MyCharacterController>().MoveThePlayer(this.transform.position);

            }
        }





    }
}