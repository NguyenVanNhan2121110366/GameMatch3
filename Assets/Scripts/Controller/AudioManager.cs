using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<AudioManager>();
            }
            return _instance;
        }
    }
    #region Variable
    private AudioSource _audio;
    [SerializeField] private AudioClip destroyEffect;
    [SerializeField] private AudioClip fireBallEffect;
    [SerializeField] private AudioClip lightningBoltEffect;
    [SerializeField] private AudioClip destroyLightnighBoltEffect;
    [SerializeField] private AudioClip attackEffect;
    #endregion
    #region Public
    public AudioSource AudioSrc { get => _audio; set => _audio = value; }
    public AudioClip DestroyEffect { get => destroyEffect; set => destroyEffect = value; }
    public AudioClip FireBallEffect { get => fireBallEffect; set => fireBallEffect = value; }
    public AudioClip LightningBoltEffect { get => lightningBoltEffect; set => lightningBoltEffect = value; }
    public AudioClip DestroyLightnighBoltEffect { get => destroyLightnighBoltEffect; set => destroyLightnighBoltEffect = value; }
    public AudioClip AttackEffect { get => attackEffect; set => attackEffect = value; }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }



}
