using Assets.Script.classes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class ProdacrCotrole : MonoBehaviour
{
    [SerializeField][Range(0.0f, 0.010f)] float lerp = 0.03f;
    [SerializeField][Range(0.0f, 0.10f)] float hight;
    private Outline Outlineoutline;
    private Vector3 starthight;
    private PanelControl panel;

    private bool isinPostion;
    private bool isdriction;
    private bool ishovring;

    private Product product;
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        // Set the product to null
        product = null;
        // Get the Outline component of the game object
        Outlineoutline = gameObject.GetComponent<Outline>();
        // Disable the outline by default
        Outlineoutline.enabled = false;
        // Store the initial position of the game object
        starthight = this.gameObject.transform.position;
        // Initialize the hover state to false and the position state to true
        ishovring = false; isinPostion = true;
    }

    /// <summary>
/// Update is called once per frame.
/// Handles the activation of the game object and panel initialization.
/// </summary>
void Update()
{
    // Deactivate the game object if no product is assigned
    if (product == null) 
        this.gameObject.SetActive(false);

    // Check for mouse click, hovering state, and selection status
    if (Input.GetMouseButtonDown(0) && ishovring && !ValusePasser.isSelect)
    {
        // Initialize the panel with the current product
        panel.initpanel(product);
        // Set position and direction flags
        isinPostion = false;
        isdriction = true;
    }

    // If no product is selected, reset position and direction flags
    if (!ValusePasser.isSelect)
    {
        isinPostion = false;
        isdriction = false;
    }

    // Update the position of the product
    makeprodactup();

    // Enable the outline if the object has moved from start or is hovering
    Outlineoutline.enabled = (Vector3.Distance(this.gameObject.transform.position, starthight) > 0.02f || ishovring);
}

    /// <summary>
    /// Called when the mouse enters the game object.
    /// </summary>
    void OnMouseEnter()
    {
        // If no product is selected, set the hover state to true
        if (!ValusePasser.isSelect)
            ishovring = true;
    }

    /// <summary>
    /// Called when the mouse exits the game object.
    /// </summary>
    void OnMouseExit()
    {
        // Reset the hover state to false
        ishovring = false;
    }

    /// <summary>
/// Updates the position of the game object to simulate a "product update" animation or effect.
/// </summary>
void makeprodactup()
{
    // Initialize the additional height to zero
    float addtohiht = 0;

    // If the object is already in position, exit the function
    if (isinPostion)
        return;

    // If the direction flag is set, add the defined height to the additional height
    if (isdriction)
        addtohiht += hight;

    // Calculate the target position by adding the additional height to the starting height
    Vector3 tomove = new Vector3(starthight.x, starthight.y + addtohiht, starthight.z);

    // Smoothly interpolate the current position towards the target position
    this.gameObject.transform.position = Vector3.Lerp(
              this.gameObject.transform.position, tomove, lerp);

    // If the object is very close to the target position, snap it to the target and mark as in position
    if (Vector3.Distance(this.gameObject.transform.position, tomove) < 0.00002f)
    {
        this.gameObject.transform.position = tomove;
        isinPostion = false;
    }
}
        /// <summary>
        /// Initializes the ProdacrCotrole model with the specified Product and PanelControl.
        /// </summary>
        /// <param name="prodact">The Product to assign to the model.</param>
        /// <param name="panel">The PanelControl to assign to the model.</param>
    public void GetData(Product prodact, PanelControl panel)
    {
        // Assign the specified product to the model
        this.product = prodact;

        // Assign the specified panel to the model
        this.panel = panel;

        // Set the game object to active
        this.gameObject.SetActive(true);
    }

}
