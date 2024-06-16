using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ������������ ��� ����, ����� �������������, ��� � ���������� ����� ������ ���� ��������� ������ SoundManager
    public static SoundManager Instance { get; private set; }

    // ����� ��������� ��� ��������� ������
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource attackButtonSound;
    [SerializeField] private AudioSource ultimateButtonSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ��� �������� ����� ����
        }
        else
        {
            Destroy(gameObject); // ���������� ���� ������, ���� ��� ���������� ���������
            return;
        }
    }

    // ��������������� ��������� �������
    public void PlaySoundEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    // ��������������� ������ � ������������ ������������
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop; //������������
        musicSource.Play();
    }

    // ��������� ������
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ��������� ��������� �������� ��������
    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = 0.3f;
    }

    // ��������� ��������� ������
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}