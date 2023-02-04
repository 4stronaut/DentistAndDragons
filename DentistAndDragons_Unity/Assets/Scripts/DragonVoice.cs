using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonVoice : MonoBehaviour {

    public static DragonVoice Instance;

    [SerializeField]
    private List<AudioClip> _hurtAudio = new List<AudioClip> ();

    [SerializeField]
    private AudioSource _dragonSource;

    private void Awake () {
        if ( Instance != null )
            Destroy ( gameObject );
        else {
            Instance = this;
        }
    }

    public void PlayDragonHurtSound () {
        if ( !_dragonSource.isPlaying && _hurtAudio.Count > 0 ) {
            int t = Random.Range ( 0, _hurtAudio.Count );
            _dragonSource.PlayOneShot ( _hurtAudio [ t ] );
        }
    }
}
