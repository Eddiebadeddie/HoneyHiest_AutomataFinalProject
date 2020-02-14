using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Audio : MonoBehaviour
{
    public static SFX_Audio sfx = null;
    AudioSource sfx_audio;

    [SerializeField] AudioClip mouse_over_button;
    [SerializeField] AudioClip mouse_click_button;

    void Awake(){
        if(sfx == null){
            sfx = this;
        }
        else{
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sfx_audio = GetComponent<AudioSource>();
    }

    public void PlayOver(){
        sfx_audio.clip = mouse_over_button;
        sfx_audio.Play();
    }

    public void PlayClick(){
        sfx_audio.clip = mouse_click_button;
        sfx_audio.Play();
    }
}
