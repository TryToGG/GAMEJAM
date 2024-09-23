using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of different items to place
    private int currentItemIndex = 0; // Current item type index

    public float moveSpeed = 5f; // Cursor movement speed

    void Update()
    {
        // Cursor movement using WASD
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Cycle item type using Z key
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CycleItem();
        }

        // Place item using X key
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlaceItem();
        }
    }

    void CycleItem()
    {
        // Cycle through item types
        currentItemIndex = (currentItemIndex + 1) % itemPrefabs.Length;
        Debug.Log("Current item: " + itemPrefabs[currentItemIndex].name);
    }

    void PlaceItem()
    {
        // Place the selected item at the cursor's position
        Instantiate(itemPrefabs[currentItemIndex], transform.position, Quaternion.identity);
    }
}
