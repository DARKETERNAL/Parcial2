using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parcial2.Game
{
    public class SpecialBullet : MonoBehaviour
    {

        private Rigidbody myRigidBody;
        private float speed;
        private int damage;
        private Player player;
        private Enemy[] enemies;

        private GameObject instigator;

        

        public void SetParams(float bulletSpeed, int bulletDamage, GameObject instanceInstigator)
        {
            instigator = instanceInstigator;
            speed = bulletSpeed;
            damage = bulletDamage;
        }

        public void Toss()
        {
            myRigidBody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }

        private void Implode()
        {
            Collider[] otherColliders = Physics.OverlapSphere(transform.position, 4F);
            //Debug.Log(otherColliders.Length);

            for (int i = 0; i < otherColliders.Length; i++)
            {
                if (otherColliders[i].CompareTag("Enemy"))
                {
                    Debug.Log("Enemy detected");
                    otherColliders[i].GetComponent<Enemy>().ReceiveDamage(damage);
                   
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            Implode();

            if (!other.CompareTag("Player"))
            {
                BulletPool.Instance.ReleaseBullet(this.gameObject);
                player.canShoot = true;
                player.shootButton.interactable = true;
            }
        }

        // Use this for initialization
        private void Awake()
        {
            myRigidBody = GetComponent<Rigidbody>();
            player = FindObjectOfType<Player>();
            
        }

        private void OnDestroy()
        {
            myRigidBody = null;
        }
    }

}