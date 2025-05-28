using System;
using System.Collections;
using _01_Scripts.Entities;
using UnityEngine;

namespace _01_Scripts.Players.Bullet
{
    public class BulletMove : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float speed = 15f;
        
        private void Start()
        {
            StartCoroutine(BulletLifeTime());
        }

        private IEnumerator BulletLifeTime()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            transform.position += transform.forward * (speed * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                var contact = collision.contacts[0];;
                Destroy(gameObject);
            }
        }
    }
}