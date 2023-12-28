using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject, 2f);
        }
    }
}
