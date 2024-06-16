using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // используется для того, чтобы гарантировать, что в приложении будет только один экземпляр класса SoundManager
    public static SoundManager Instance { get; private set; }

    // Аудио источники для различных звуков
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource attackButtonSound;
    [SerializeField] private AudioSource ultimateButtonSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при загрузке новых сцен
        }
        else
        {
            Destroy(gameObject); // Уничтожаем этот объект, если уже существует экземпляр
            return;
        }
    }

    // Воспроизведение звукового эффекта
    public void PlaySoundEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    // Воспроизведение музыки с возможностью зацикливания
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop; //зацикливание
        musicSource.Play();
    }

    // Остановка музыки
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Изменение громкости звуковых эффектов
    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = 0.3f;
    }

    // Изменение громкости музыки
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}