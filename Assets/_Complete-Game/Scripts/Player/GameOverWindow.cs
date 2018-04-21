using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class GameOverWindow : MonoBehaviour
    {
        public PlayerHealth PlayerHealth;
        public MobilePhone MobilePhone;
        public GameObject Root;

        void Awake()
        {
            PlayerHealth.Died += OnDie;
        }

        void OnValidate()
        {
            if (!MobilePhone)
            {
                MobilePhone = Object.FindObjectOfType<MobilePhone>();
            }

        }


        private void OnDie()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Root.gameObject.SetActive(true);
        }

        public void RestartLevel ()
        {
            // Reload the level that is currently loaded.
            SceneManager.LoadScene (0);
        }
    }
}