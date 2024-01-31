using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyIndicatorTargetChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemIndicator.Instance.UpdateItem(transform);
    }

    private void OnDisable()
    {
        ItemIndicator.Instance.UpdateItem(null);
    }
}
