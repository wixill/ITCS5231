using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator anim;
    [SerializeField] int nextSceneIndex;

    public void OnTriggerEnter(Collider other)
    {
        // When player enters trigger, switch to the desired next scene
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("FadeOut");
        }
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
