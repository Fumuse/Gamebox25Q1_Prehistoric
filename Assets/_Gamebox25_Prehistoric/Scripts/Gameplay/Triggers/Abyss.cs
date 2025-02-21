using UnityEngine;
using UnityEngine.SceneManagement;

public class Abyss : MonoBehaviour
{
    [SerializeField] private Collider abyssCollider;

    private void OnValidate()
    {
        abyssCollider ??= GetComponent<Collider>();
        if (abyssCollider != null)
        {
            abyssCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}