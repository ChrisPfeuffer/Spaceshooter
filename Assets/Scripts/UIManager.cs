using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _gameOverText;
    [SerializeField] Text _restartText;
    [SerializeField] Sprite[] _liveSprites;
    [SerializeField] Image _livesImg;
    [SerializeField] GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.Log("GameManager is null");
        }

    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            ShowGameOverText();
        }
    }

    public void ShowGameOverText()
    {
        _gameManager.GameOver();
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }

    }
}
