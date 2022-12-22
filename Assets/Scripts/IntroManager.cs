using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    
    public VideoPlayer player;

    private void Update()
    {
        if (player.frame >= ((long)player.frameCount) - 4)
        {
            print("done");
            SceneManager.LoadScene(1);
        }
    }

}
