using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] List<ItemPickup> chestLoot;

    public override void Interact()
    {
        base.Interact();

        if (chestLoot != null)
        {
            foreach (ItemPickup i in chestLoot)
            {
                ItemPickup droppedItem = Instantiate(i);
                droppedItem.transform.localPosition = new Vector3(Random.Range(transform.position.x + -0.6f, transform.position.x + 0.6f), 1.115f, Random.Range(transform.position.z + 1, transform.position.z + 1.35f));

                chestLoot.Remove(i);
            }
        }
        else
        {
            // do nothing
        }
    }
}
