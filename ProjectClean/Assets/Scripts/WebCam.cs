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
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length != 0)
        {
            _webCamDevice = devices[0];
        }


        _webCamTexture = new WebCamTexture(_webCamDevice.name);
        _webCamTexture.Play();
        transform.GetComponent<Renderer>().material.mainTexture = _webCamTexture;
    }

    // Update is called once per frame
    void Update()
    {

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
            Debug.Log("Name: " + device.name);
        }
    }
}
