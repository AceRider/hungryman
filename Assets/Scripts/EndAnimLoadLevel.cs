using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndAnimLoadLevel : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    // Update is called once per frame
    void Update()
    {
        if(!(playableDirector.state == PlayState.Playing))
        {
            SceneManager.LoadScene(SceneUtils.Main);
        }
    }
}
