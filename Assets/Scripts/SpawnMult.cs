using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMult : MonoBehaviour
{

    [SerializeField]
    private List<BonusZone> BonusToDorp = new List<BonusZone>();





    #region REGARDE_PAS_WSH
    /*
        private float screenLeft, screenRight, screenUp, screenDown;

        void Start()
        {
            screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            screenUp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, 0, 0)).y;
            screenDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        }

        public void SpawnAtRandomPos()
        {
            float spawnX = Random.Range(screenLeft, screenRight);
            float spawnY = Random.Range(screenDown, screenUp);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            Instantiate(BonusToDorp[GetRandomSpawn()].gameObject, spawnPosition, Quaternion.identity);
        }

        public int GetRandomSpawn()
        {
            float random = UnityEngine.Random.Range(0, 1f);
            float numForAdding = 0;
            float total = 0;

            foreach (BonusZone item in BonusToDorp)
            {
                total += item.spawnRate;
            }

            for (int i = 0; i < BonusToDorp.Count; i++)
            {
                if (BonusToDorp[i].spawnRate / total + numForAdding >= random)
                {
                    return i;
                }
                else
                {
                    numForAdding += BonusToDorp[i].spawnRate / total;
                }
            }
            return 0;
        }
    */
    #endregion
}
