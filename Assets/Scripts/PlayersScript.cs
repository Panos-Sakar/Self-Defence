using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayersScript : MonoBehaviour
{
    public float headRotationSpeed = 50f;

    public GameObject headInerTransform;
    public GameObject headGridTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        headGridTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
        headInerTransform.transform.Rotate(Vector3.up * (Time.deltaTime * headRotationSpeed));
    }
}
