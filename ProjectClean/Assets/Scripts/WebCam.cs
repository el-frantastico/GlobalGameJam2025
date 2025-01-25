using System.Collections;
using UnityEngine;

public class WebCam : MonoBehaviour
{

    private WebCamTexture _webCamTexture;
    private WebCamDevice _webCamDevice;

    [SerializeField]
    private Material _webCamMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetWebCam());
        //Get webcam

        _webCamTexture = new WebCamTexture(_webCamDevice.name);
        _webCamTexture.Play();
        transform.GetComponent<Renderer>().material.mainTexture = _webCamTexture;
    }


    IEnumerator GetWebCam()
    {
        FindWebCams();

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("webcam found");
        }
        else
        {
            Debug.Log("webcam not found");
        }

    }

    void FindWebCams()
    {
        foreach (var device in WebCamTexture.devices)
        {
            _webCamTexture = new WebCamTexture(device.name);
        }
    }
}
