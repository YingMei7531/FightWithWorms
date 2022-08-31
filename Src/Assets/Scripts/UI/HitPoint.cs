using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoint : MonoBehaviour
{
    public Slider slider;

    void Update()
    {
        Turn();
    }

    void Turn() {
        Vector3 lookForward = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(lookForward.normalized);
        //transform.rotation = Quaternion.LookRotation(lookForward.normalized, Camera.main.transform.up.normalized);
    }

    public void SetValue(float ratio) {
        slider.value = ratio;
    }
}
