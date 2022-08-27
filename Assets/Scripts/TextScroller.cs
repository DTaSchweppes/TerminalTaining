using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTaSchweppes.Tools
{
 public class TextScroller : MonoBehaviour
 {
	[SerializeField] private string _word;
    [SerializeField] private Text _text;
    [SerializeField] private AudioClip _scrollSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _enableScrollingSound;
    [SerializeField] private bool _enableAutoTextScrolling;
    private float _speedScrolling;

        private void Awake()
        {
            if (_enableScrollingSound!=false) _audioSource.clip = _scrollSound;
            if (_enableAutoTextScrolling != false) StartScrollingCoroutine(_text.text);
        }
        public void StartScrollingCoroutine(string word,float speedScrolling = 0.1f)
        {
            _text.text = "";
            _word = word;
            _speedScrolling = speedScrolling;
            StartCoroutine(ScrollingTextTine());
        }
        private IEnumerator ScrollingTextTine()
        {
            for (int i=0;i < _word.Length; i++ )
            {
                _text.text += _word[i];
                if (_enableScrollingSound != false) _audioSource.Play();
                yield return new WaitForSeconds(_speedScrolling);
            }
        }
        public IEnumerator RunInstructionEnumerator(params (string message, float delay)[] data)
        {
            foreach (var (message, delay) in data)
            {
                StartScrollingCoroutine(message);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}