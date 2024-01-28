using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    bool IsPickedUp { get; set; }
    int PlayerID { get; set; }
    public void OnPickupInitialization(int playerID, Transform parent);
}
