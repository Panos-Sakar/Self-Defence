using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;

    private Transform _myTransform;

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            _myTransform.position += transform.forward * (Time.deltaTime * speed);
        }
    }
}
