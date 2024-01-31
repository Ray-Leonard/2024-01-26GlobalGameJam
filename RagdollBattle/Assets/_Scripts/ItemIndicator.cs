using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIndicator : MonoBehaviour
{
    private static ItemIndicator _instance;

    public static ItemIndicator Instance { get => _instance; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }


    public Transform item; // The target item's transform
    public Image indicatorImage; // The UI Image component for the indicator
    [SerializeField] private Vector2 margin;


    // Update is called once per frame
    void Update()
    {
        if(item == null)
        {
            return;
        }

        Vector3 itemPositionScreen = Camera.main.WorldToScreenPoint(item.position);
        bool isOffScreen = itemPositionScreen.x <= 0 || itemPositionScreen.x >= Screen.width ||
                           itemPositionScreen.y <= 0 || itemPositionScreen.y >= Screen.height;

        indicatorImage.enabled = isOffScreen;

        if (isOffScreen)
        {
            // Direction from the center of the screen to the item
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
            Vector3 direction = itemPositionScreen - screenCenter;

            // Normalize direction to get a unit vector
            direction = direction.normalized;

            // Find intersection point
            float screenAspect = Screen.width / (float)Screen.height;

            float screenY = screenCenter.y - margin.y;
            float screenX = screenCenter.x - margin.x;

            Vector3 screenBounds;
            // obtuse angle, x is extreme
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            { // Horizontal bounds
                screenBounds = direction.x > 0 ?
                    new Vector3(Screen.width - margin.x, screenCenter.y + (screenX / direction.x * direction.y), 0) :
                    new Vector3(margin.x, screenCenter.y - (screenX / direction.x * direction.y), 0);
            }
            // sharp angle, y is extreme
            else
            { // Vertical bounds
                screenBounds = direction.y > 0 ?
                    new Vector3(screenCenter.x + (screenY / direction.y * direction.x), Screen.height - margin.y, 0) :
                    new Vector3(screenCenter.x - (screenY / direction.y * direction.x), margin.y, 0);
            }

            // Convert screen position to world position for UI element
            indicatorImage.transform.position = new Vector3(screenBounds.x, screenBounds.y, 0);


            // Calculate rotation towards the item
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //indicatorImage.rectTransform.localEulerAngles = new Vector3(0, 0, angle);

            indicatorImage.transform.up = direction;
        }
    }


    public void UpdateItem(Transform t)
    {
        item = t;
    }
}
