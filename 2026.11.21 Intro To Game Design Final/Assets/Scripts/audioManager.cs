using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager _Instance;
    public List<AudioClip> audioClips;
    public AudioSource sfx_Source;
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        sfx_Source = gameObject.GetComponent<AudioSource>();
    }
    public void PlaySound(string s)
    {
        for(int i = 0; i < audioClips.Count; i++)
        {
            if (s == audioClips[i].name)
            {
                sfx_Source.PlayOneShot(audioClips[i]);
            }
        }
    }
}
