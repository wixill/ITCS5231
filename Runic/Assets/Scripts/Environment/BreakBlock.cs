using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    [SerializeField] private GameObject fracturedBlock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(fracturedBlock, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
