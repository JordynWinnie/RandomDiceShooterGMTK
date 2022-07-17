using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] BGM;
    [SerializeField] private AudioClip[] diceRolls;
    [SerializeField] private AudioClip gunFire;
    [SerializeField] private AudioClip abilityChime;
    [SerializeField] private AudioClip purchasedSound;
    [SerializeField] private AudioClip errorPurchaseSound;
    private AudioSource _audioSource;

    private readonly List<AudioSource> _SFX = new();

    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = MathHelper.RandomFromArray(BGM);
        for (var i = 0; i < 8; i++) _SFX.Add(gameObject.AddComponent<AudioSource>());
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = MathHelper.RandomFromArray(BGM);
            _audioSource.Play();
        }
    }

    public void PlayDiceAudio()
    {
        PlayAudio(MathHelper.RandomFromArray(diceRolls));
    }

    public void PlayGunAudio()
    {
        PlayAudio(gunFire, 0.1f, Random.Range(0.8f, 1.1f));
    }

    public void AbilityChimeAudio()
    {
        PlayAudio(abilityChime, 0.2f, Random.Range(0.8f, 1.1f));
    }

    public void PlayPurchaseSound()
    {
        PlayAudio(purchasedSound, 0.1f);
    }

    public void PlayErrorSound()
    {
        PlayAudio(errorPurchaseSound, 0.5f);
    }

    private void PlayAudio(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        for (var i = 0; i < _SFX.Count; i++)
            if (!_SFX[i].isPlaying)
            {
                _SFX[i].clip = clip;
                _SFX[i].volume = volume;
                _SFX[i].pitch = pitch;
                _SFX[i].Play();
                return;
            }

        var newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.volume = volume;
        newAudio.pitch = pitch;
        newAudio.Play();
        _SFX.Add(newAudio);
    }
}