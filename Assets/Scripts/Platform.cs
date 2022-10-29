using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject diamond;

    // Start is called before the first frame update
    void Start()
    {

        int randDiamond = Random.Range(0, 2);

        Vector3 diamondPos = transform.position;

        diamondPos.y += 1.2f;

        if (randDiamond < 1)
        {
            //spawn diamond
            GameObject diamondInstance = Instantiate(diamond, diamondPos, diamond.transform.rotation);

            diamondInstance.transform.SetParent(gameObject.transform);
        }
        //1,2,3,4 dont spawn diamond

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("Fall", 0.2f);
        }
    }

    void Fall()
    {

        GetComponent<Rigidbody>().isKinematic = false;

        Destroy(gameObject, 1f);
    }



}
