using UnityEngine;
using System.Collections;

public class NavigationSoundManager : MonoBehaviour {

    public AudioSource up;
    public AudioSource down;
    public AudioSource right;
    public AudioSource left;

    public void playUpSound()
    {
        up.Play();
    }

    public void playDownSound()
    {
        down.Play();
    }

    public void playRightSound()
    {
        right.Play();
    }

    public void playLeftSound()
    {
        left.Play();
    }
}
