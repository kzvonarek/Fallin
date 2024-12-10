using UnityEngine;

public class ParentColorMatchBehavior : MonoBehaviour
{
    // sign and floor color #'s (to select color)
    [HideInInspector] public int leftFloorColor;
    [HideInInspector] public int middleFloorColor;
    [HideInInspector] public int rightFloorColor;
    [HideInInspector] public int signColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftFloorColor = Random.Range(0, 2); // colors 0 and 1
        middleFloorColor = Random.Range(2, 4); // colors 2 and 3
        rightFloorColor = Random.Range(4, 6); // colors 4 and 5
        signColor = Random.Range(0, 3);
    }
}
