using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent != null) {
            GameObject gameObject = other.transform.parent.gameObject;
            if (gameObject.tag == "Monster" || gameObject.tag == "FlyMonster") {
                Player.instance.Hit();
                Destroy(gameObject);
            }
        }
    }
}
