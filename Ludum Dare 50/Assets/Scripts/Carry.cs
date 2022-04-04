using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry : MonoBehaviour
{
    public Carryable heldCarryable;

    public GameObject arm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (heldCarryable == null)
        {
            Carryable carryScript = collision.gameObject.GetComponent<Carryable>();
            if (carryScript != null)
            {
                if (carryScript.Pickup())
                {
                    heldCarryable = carryScript;
                }
            }
        }
    }

    private void Update()
    {
        if (heldCarryable != null)
        {
            heldCarryable.transform.position = arm.transform.position + (Vector3)heldCarryable.pickupOffset;

            if (Input.GetMouseButtonDown(0))
            {
                heldCarryable.Drop();
                heldCarryable = null;
            }
        }

    }
}
