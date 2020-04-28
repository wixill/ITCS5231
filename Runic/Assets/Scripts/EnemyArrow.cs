using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyArrow : MonoBehaviour
{

    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    private bool hitSomething;
    private float playerHits;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitSomething = false;
        //trans.LookAt(rb.velocity);
        trans.Rotate(-10f, 0.0f, 0.0f, Space.Self);

    }

    // Update is called once per frame
    void Update()
    {
        if (!hitSomething)
        {

            trans.LookAt(rb.velocity);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            playerHits++;
            if (playerHits == 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}
