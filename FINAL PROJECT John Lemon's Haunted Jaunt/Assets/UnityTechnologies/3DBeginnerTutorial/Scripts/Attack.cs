using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject electroSlashPrefab;


    // Start is called before the first frame update
    void Start()
    {
        electroSlashPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OneTimeAttack();
    }

    void OneTimeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            electroSlashPrefab.SetActive(true);
            StartCoroutine(SlashStop());
        }
    }

    IEnumerator SlashStop()
    {
        yield return new WaitForSeconds(2f);
        Destroy(electroSlashPrefab);

    }

    private void OnTriggerEnter(Collider whatDidIHit)
    {
        if (whatDidIHit.tag == "Enemy")
        {
            Destroy(whatDidIHit.gameObject);

        }
    }

}
