using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    private bool spawned = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !spawned)
        {
            Instantiate(ship, new Vector3(-6, 32, -93), Quaternion.identity);
            spawned = true;
        }
    }
}
