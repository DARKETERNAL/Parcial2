using UnityEngine;

namespace Parcial2.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody myRigidBody;
        private float speed;
        private int damage;
        private Player player;

        private GameObject instigator;

        

        public void SetParams(float bulletSpeed, int bulletDamage, GameObject instanceInstigator)
        {
            instigator = instanceInstigator;
            speed = bulletSpeed;
            damage = bulletDamage;
        }

        public void Toss()
        {
            myRigidBody.AddForce(player.transform.forward * speed, ForceMode.VelocityChange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == Player.Instance.gameObject)
            {
                //Collided with player
            }
            else
            {
                Enemy enemy = other.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.ReceiveDamage(damage);
                }
            }

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