using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class OrlogeTics : UnityEngine.MonoBehaviour
{
    float hourDelta, minutDelta, secundDelta;
    Quaternion hourRotate, minutRotate, secundRotate;


    // Start is called before the first frame update
    void Start()
    {
        hourRotate = Quaternion.AngleAxis(hourDelta, Vector3.right);
        minutRotate = Quaternion.AngleAxis(minutDelta, Vector3.right);
        secundRotate = Quaternion.AngleAxis(secundDelta, Vector3.right);

    }

    // Update is called once per frame
    void Update()
    {
        hourRotate = Quaternion.AngleAxis(hourDelta, Vector3.right);
        minutRotate = Quaternion.AngleAxis(minutDelta, Vector3.right);
        secundRotate = Quaternion.AngleAxis(secundDelta, Vector3.right);

    }
}
