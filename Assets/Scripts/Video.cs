using UnityEngine;

public class Video : MonoBehaviour
{

    public MovieTexture movieclips;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = movieclips;

        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        movieclips.Play();
    }

}