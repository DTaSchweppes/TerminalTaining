using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DTaSchweppes.Tools
{
 public class MainMenuManager : MonoBehaviour
 {
		[SerializeField] private AudioSource _openMenuItem;
		[SerializeField] private Animator _openMenuAnim;

        private void Awake()
        {
			_openMenuAnim.enabled = false;
		}
        public void LoadChooseLeve() => StartCoroutine(LoadSceneTine(1));
		public IEnumerator LoadSceneTine(int sceneNumber)
		{
			_openMenuItem.Play();
			_openMenuAnim.enabled = true;
			yield return new WaitForSeconds(2f);
			SceneManager.LoadScene(sceneNumber);
		}
		public void LoadSettings() => StartCoroutine(LoadSceneTine(2));
		public void ExitApp() => Application.Quit();
}
}