using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parcial2
{
    public class SpecialBulletPool : MonoBehaviour
    {

        private static SpecialBulletPool instance;

        public static SpecialBulletPool Instance
        {
            get
            {
                return instance;
            }
        }

        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private Transform poolsPosition;

        [SerializeField]
        private int size;

        private List<GameObject> bullets;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                PrepareBullet();
            }
            else
                Destroy(gameObject);
        }

        private void PrepareBullet()
        {
            bullets = new List<GameObject>();
            for (int i = 0; i < size; i++)
                AddBullet();
        }

        public GameObject GetBullet()
        {
            if (bullets.Count == 0)
                AddBullet();
            return AllocateBullet();
        }

        public void ReleaseBullet(GameObject bullet)
        {
            bullet.gameObject.SetActive(false);
            bullets.Add(bullet);
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        private void AddBullet()
        {
            GameObject instance = Instantiate(bulletPrefab);
            instance.gameObject.SetActive(false);
            bullets.Add(instance);
        }

        private GameObject AllocateBullet()
        {
            GameObject bullet = bullets[bullets.Count - 1];
            bullets.RemoveAt(bullets.Count - 1);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }
}




