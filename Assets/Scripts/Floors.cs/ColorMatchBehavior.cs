using UnityEngine;
using UnityEngine.SceneManagement;
public class ColorMatchBehavior : MonoBehaviour
{
    Renderer objRenderer;

    // list of colors
    Color[] colorList;

    // sign and floor color #'s (to select color)
    [SerializeField] int leftFloorColor;
    [SerializeField] int middleFloorColor;
    [SerializeField] int rightFloorColor;
    [SerializeField] int signColor;

    // sign and floor booleans (to adjust their colors respectively)
    [SerializeField] bool isLeftFloor = false;
    [SerializeField] bool isMiddleFloor = false;
    [SerializeField] bool isRightFloor = false;
    [SerializeField] bool isSign = false;

    void Start()
    {
        // get obj's renderer to change color
        objRenderer = GetComponent<Renderer>();

        // set colors in colorList
        colorList = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };

        leftFloorColor = Random.Range(0, 2); // colors 0 and 1
        middleFloorColor = Random.Range(2, 4); // colors 2 and 3
        rightFloorColor = Random.Range(4, 6); // colors 4 and 5

        // if signColor = 0 [match with left], = 1 [match with middle], = 2 [match with right]
        signColor = Random.Range(0, 3);
        if (signColor == 0)
        {
            signColor = leftFloorColor;

        }
        else if (signColor == 1)
        {
            signColor = middleFloorColor;
        }
        else if (signColor == 2)
        {
            signColor = rightFloorColor;
        }

        // check which type of floor obj is, or if obj is the sign
        // assign respective bool to true for color assignment
        if (gameObject.CompareTag("Color Left Floor"))
        {
            isLeftFloor = true;
        }
        else if (gameObject.CompareTag("Color Middle Floor"))
        {
            isMiddleFloor = true;
        }
        else if (gameObject.CompareTag("Color Right Floor"))
        {
            isRightFloor = true;
        }
        else if (gameObject.CompareTag("Color Sign"))
        {
            isSign = true;
        }

        // assign respective randomized colors
        if (isLeftFloor)
        {
            objRenderer.material.color = colorList[leftFloorColor];
        }

        else if (isMiddleFloor)
        {
            objRenderer.material.color = colorList[middleFloorColor];
        }

        else if (isRightFloor)
        {
            objRenderer.material.color = colorList[rightFloorColor];
        }

        else if (isSign)
        {
            objRenderer.material.color = colorList[signColor];
        }
    }

    // if floor color matches with sign color, do not kill player 
    // else, kill player [similar to FloorCollisions.cs]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isLeftFloor && leftFloorColor != signColor)
            {
                // reset scene (temporary [look at notes])
                SceneManager.LoadScene("Main Scene");
                // for testing (remove later)
                Debug.Log("Game Over");
            }
            else if (isMiddleFloor && middleFloorColor != signColor)
            {
                SceneManager.LoadScene("Main Scene");
                Debug.Log("Game Over");
            }
            else if (isRightFloor && rightFloorColor != signColor)
            {
                SceneManager.LoadScene("Main Scene");
                Debug.Log("Game Over");
            }
        }

        // destroy floor on collision with 'Destroy Zone'
        if (other.gameObject.CompareTag("Destroy Zone"))
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
