using Assets.Script.classes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PanelControl : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI decription;
    public TextMeshProUGUI Price;
    public Product CurrentProduct;

    public GameObject editPanel;
    public TextMeshProUGUI erroLable;
    public TMP_InputField newName;
    public TMP_InputField newPrice;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        editPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Initialize the edit mode of the panel.
    /// </summary>
    public void initEditMode()
    {
        // Set the edit panel to active
        editPanel.SetActive(true);
    }

    /// <summary>
    /// Close the edit panel after a short delay.
    /// </summary>
    public void closeEditpanel()
    {
        // Clear the error label and input fields
        erroLable.text = "";
        newName.text = "";
        newPrice.text = "";

        // Wait for 1.5 seconds and then deactivate the edit panel
        StartCoroutine(WaitAndDeactive(1.5f, editPanel));
    }
    /// <summary>
    /// Submits the edit of the product.
    /// </summary>
    public void sumbitEdit()
    {
        // Check if the price is a valid number
        if (!float.TryParse(newPrice.text, out float result))
        {
            // If not, show an error message
            erroLable.text = "Error: the price can only be a number";
            return;
        }
        // Check if the price is greater than 0
        if (result <= 0)
        {
            // If not, show an error message
            erroLable.text = "Error: the price must be greater than 0";
            return;
        }
        // Check if the name is not empty
        if (newName.text == "")
        {
            // If so, show an error message
            erroLable.text = "Error: you must give a name to the product";
            return;
        }
        // Update the product with the new data
        CurrentProduct.price = result;
        CurrentProduct.name = newName.text;
        // Show a success message
        erroLable.text = "Success. The product details have been changed";
        // Update the UI with the new data
        text.text = CurrentProduct.name;
        decription.text = CurrentProduct.description;
        Price.text = CurrentProduct.price + "$";
    }
    /// <summary>
    /// Initialize the panel with the specified product.
    /// </summary>
    /// <param name="prod">The product to show in the panel.</param>
    public void initpanel(Product prod)
    {
        if (prod == null) return;
        // Set the current product to the specified product
        this.CurrentProduct = prod;
        // Indicate that a product is selected
        ValusePasser.isSelect = true;
        // Set the text and description of the panel to the product's name and description
        text.text = prod.name;
        decription.text = prod.description;
        // Set the price of the panel to the product's price
        Price.text = prod.price + "$";
        // Activate the panel
        gameObject.SetActive(true);
    }
    /// <summary>
    /// Closes the panel.
    /// </summary>
    public void closepanel()
    {
        // Set the current product to null, indicating that no product is selected
        this.CurrentProduct = null;
        // Indicate that no product is selected
        ValusePasser.isSelect = false;
        // Clear the text, description and price of the panel
        text.text = "";
        decription.text = "";
        Price.text = "";
        // Wait for 0.8 seconds and then deactivate the panel
        StartCoroutine(WaitAndDeactive(0.8f, gameObject));        
    }
    /// <summary>
    /// Waits for the specified amount of time and then deactivates the specified game object.
    /// </summary>
    /// <param name="time">The amount of time to wait.</param>
    /// <param name="obj">The game object to deactivate.</param>
    /// <returns>An IEnumerator that waits for the specified time and then deactivates the game object.</returns>
    IEnumerator WaitAndDeactive(float time, GameObject obj)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(time);

        // This line will execute after the wait
        obj.SetActive(false);
    }
}
