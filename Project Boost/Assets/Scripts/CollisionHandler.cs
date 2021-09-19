using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //long delay so we can add SFX and Particle Effects upon crash or success
 [SerializeField] float levelLoadDelay = 2f;
 [SerializeField] AudioClip success;
 [SerializeField] AudioClip crash;
 [SerializeField] ParticleSystem successParticles;
 [SerializeField] ParticleSystem crashParticles;

 AudioSource audioSource;

 bool isTransitioning = false;
 bool collisionDisabled = false;

 void Start()
    {
     audioSource = GetComponent<AudioSource>();
    }
 void RespondToDebugKeys()
    {
     if (Input.GetKeyDown(KeyCode.L))
        {
         LoadNextLevel();
        }
     else if (Input.GetKeyDown(KeyCode.C))
        {
         collisionDisabled = !collisionDisabled; //this will toggle collision on and off
        }
    }

 void Update() 
    {
     RespondToDebugKeys();
    }

 void OnCollisionEnter(Collision other)
    {
     if (isTransitioning || collisionDisabled)
        {
         return;
        }
     switch (other.gameObject.tag)
        {
         case "Friendly":
            Debug.Log("This thing is friendly");
            break;
         case "Finish":
            StartSuccessSequence();
            break;
         default:
            StartCrashSequence();
            break;
        }
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);     
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}    
