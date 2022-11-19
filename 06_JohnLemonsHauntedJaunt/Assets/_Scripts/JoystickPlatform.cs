#if UNITY_ANDROID || UNITY_IOS
#define USING_MOBILE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if USING_MOBILE     
        this.gameObject.SetActive(true);
#else
        this.gameObject.SetActive(false);
#endif
    }
}
