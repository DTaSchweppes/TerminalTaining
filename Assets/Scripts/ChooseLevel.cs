using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DTaSchweppes.Tools
{
 public class ChooseLevel : MonoBehaviour
 {
		[SerializeField] private AudioSource _openMenuItem;
		public IEnumerator LoadSceneTine(int sceneNumber)
		{
			_openMenuItem.Play();
			yield return new WaitForSeconds(3f);
			SceneManager.LoadScene(sceneNumber);
		}

		public void LoadLevel(int levelNum) => StartCoroutine(LoadSceneTine(levelNum));
	}
}