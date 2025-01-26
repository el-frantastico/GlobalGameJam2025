using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public void Reset()
    {
        OverworldMusic.Instance.Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AudioBox")
        {
            OverworldMusic.Instance.EnterBox(other.gameObject);
        }
    }
}
