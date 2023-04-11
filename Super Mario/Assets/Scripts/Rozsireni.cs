using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rozsireni
{
   private static LayerMask layermask = LayerMask.GetMask("Default"); 
   
   public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.92f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, layermask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }
}
