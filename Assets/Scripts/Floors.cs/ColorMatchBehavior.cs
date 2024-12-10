using UnityEngine;
using UnityEngine.SceneManagement;
public class ColorMatchBehavior : MonoBehaviour
{
    Renderer objRenderer;

    // list of colors
    Color[] colorList;

    // sign and floor booleans (to adjust their colors respectively)
    private bool isLeftFloor = false;
    private bool isMiddleFloor = false;
    private bool isRightFloor = false;

    // access to ParentColorMatchBehavior.cs
    private ParentColorMatchBehavior pCmBscript;

    void Start()
    {
        // allow for access to floor color integeres
        pCmBscript = transform.parent.gameObject.GetComponent<ParentColorMatchBehavior>();

        // get obj's renderer to change color
        objRenderer = GetComponent<Renderer>();

        // set colors in colorList
        colorList = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };
    }

    void Update()
    {
        // check which type of floor obj is, or if obj is the sign
        // assign respective bool to true, and assign colors
        if (gameObject.CompareTag("Color Left Floor"))
        {
            isLeftFloor = true;
            objRenderer.material.color = colorList[pCmBscript.leftFloorColor];
        }
        else if (gameObject.CompareTag("Color Middle Floor"))
        {
            isMiddleFloor = true;
            objRenderer.material.color = colorList[pCmBscript.middleFloorColor];
        }
        else if (gameObject.CompareTag("Color Right Floor"))
        {
            isRightFloor = true;
            objRenderer.material.color = colorList[pCmBscript.rightFloorColor];
        }
        else if (gameObject.CompareTag("Color Sign"))
        {
            // if signColor = 0 [match with left], = 1 [match with middle], = 2 [match with right]
            if (pCmBscript.signColor == 0)
            {
                pCmBscript.signColor = pCmBscript.leftFloorColor;
            }
            else if (pCmBscript.signColor == 1)
            {
                pCmBscript.signColor = pCmBscript.middleFloorColor;
            }
            else if (pCmBscript.signColor == 2)
            {
                pCmBscript.signColor = pCmBscript.rightFloorColor;
            }
            objRenderer.material.color = colorList[pCmBscript.signColor];
        }
    }

    // if floor color matches with sign color, do not kill player 
    // else, kill player [similar to FloorCollisions.cs]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isLeftFloor && pCmBscript.leftFloorColor != pCmBscript.signColor)
            {
                // reset scene (temporary [look at notes])
                SceneManager.LoadScene("Main Scene");
            }
            else if (isMiddleFloor && pCmBscript.middleFloorColor != pCmBscript.signColor)
            {
                SceneManager.LoadScene("Main Scene");
            }
            else if (isRightFloor && pCmBscript.rightFloorColor != pCmBscript.signColor)
            {
                SceneManager.LoadScene("Main Scene");
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
