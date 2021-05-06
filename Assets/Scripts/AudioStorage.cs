using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStorage : Singleton<AudioStorage>
{
    #region Fields

    [SerializeField] private AudioClip sfx_SpaceShip_Engine;
    [SerializeField] private AudioClip sfx_SpaceShip_Death;
    [SerializeField] private AudioClip sfx_Finish;

    #endregion

    #region Properties

    public AudioClip SFX_SpaceShip_Engine => sfx_SpaceShip_Engine;
    public AudioClip SFX_SpaceShip_Death => sfx_SpaceShip_Death;
    public AudioClip SFX_Finish => sfx_Finish;

    #endregion
}
