using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{

    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider whatDidIHit)
    {
        if  (whatDidIHit.tag == "OneTimeAttack")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
