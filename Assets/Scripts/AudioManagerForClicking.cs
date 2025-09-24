using UnityEngine;

public class AudioManagerForClicking : MonoBehaviour
{

    public AudioSource select;
    public AudioSource deselect;
    public static AudioManagerForClicking instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

}
