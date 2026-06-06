using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class CollectableBaseCoin : CollectableBase
{
    public Collider collider;
    public int amount = 1;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.COIN);
        collider.enabled = false;
    }
}
