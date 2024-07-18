using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneUIManager : MonoBehaviour
{
    public void BackButton()
    {
        AudioManager.Instance.PlayFX(1);
        SceneManager.LoadSceneAsync("StartScene");
    }

}
