using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
 using System.Net;
using Newtonsoft.Json;

public class Mannger : MonoBehaviour
{
    ProductList productslist;
    public ProdacrCotrole[] prodacatModel;
    public PanelControl panel;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// the first call to Update().
    /// </summary>
    void Start()
    {
        // Start a coroutine to get the products JSON from the API
        StartCoroutine(GetRequest("https://homework.mocart.io/api/products"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Coroutine to send a GET request to the specified URI and handle the response.
    /// </summary>
    /// <param name="uri">The URI to send the GET request to.</param>
    /// <returns>An IEnumerator for coroutine control.</returns>
    IEnumerator GetRequest(string uri)
    {
        // Create a UnityWebRequest for the specified URI
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Send the request and wait for the response
            yield return webRequest.SendWebRequest();

            // Split the URI to get the last part (page name)
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            // Handle the different possible outcomes of the request
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    // Log any connection or data processing errors
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    // Log any HTTP protocol errors
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    // On success, initialize data and log the response
                    InitData(webRequest.downloadHandler.text);
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    /// <summary>
    /// Initialize data from the JSON response of the API GET request.
    /// </summary>
    /// <param name="json">The JSON response of the API GET request.</param>
    private void InitData(string json)
    {
        // Deserialize the JSON data into a ProductList object
        productslist = JsonConvert.DeserializeObject<ProductList>(json);
        
        // Loop through each product in the list and assign it to a ProdacrCotrole model
        for (int i = 0; i < productslist.products.Length; i++)
        {
            prodacatModel[i].GetData(productslist.products[i], panel);
        }
    }
}
