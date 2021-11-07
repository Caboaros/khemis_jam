using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip[] airHits;
        [Space] [SerializeField] private AudioClip[] attackSounds;
        [Space] [SerializeField] private AudioClip damageSound;
        [Space] [SerializeField] private AudioClip[] snowStepSounds;
        [SerializeField] private AudioClip[] grassStepSounds;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayAirHitSound()
        {
            _audioSource.PlayOneShot(airHits[Random.Range(0, airHits.Length)]);
        }

        public void PlayAttackSounds()
        {
            _audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Length)], .4f);
        }

        public void PlayDamageSound()
        {
            _audioSource.PlayOneShot(damageSound, .4f);
        }

        public void PlayStepsSounds(WorldEnum world)
        {
            _audioSource.PlayOneShot(world == WorldEnum.Br ? 
                grassStepSounds[Random.Range(0, grassStepSounds.Length)] : 
                snowStepSounds[Random.Range(0, snowStepSounds.Length)], .35f);
        }
    }
}