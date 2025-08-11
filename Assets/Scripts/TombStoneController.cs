using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombStoneController : MonoBehaviour
{
    public float timeToDie = 5f;
    void Start()
    {
        timeToDie = Random.Range(3.0f, 6.0f);
        Destroy(this.gameObject, timeToDie);
    }


}
