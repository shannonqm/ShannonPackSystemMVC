using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ZStart.Tool
{
    public class SceneLoadTool : MonoBehaviour
    {
        public string loadScene = "LauncherStage";
        public float delay = 0.2f;
        public string sceneName = "";
        public float speed = 50f;
        public Transform loadingImage = null;

        public Vector2 screen = new Vector2(1920, 1080);
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneLoaded;
            Screen.SetResolution((int)screen.x, (int)screen.y,true);
        }

        void Start()
        {
            StartCoroutine(InitInspector());
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("SceneLoaded....." + scene.name + ";mode = " + mode);
            sceneName = scene.name;
            if (loadingImage != null)
                loadingImage.gameObject.SetActive(false);
        }

        IEnumerator InitInspector()
        {
            yield return new WaitForSeconds(delay);
            if (loadingImage != null)
                loadingImage.gameObject.SetActive(true);
            AsyncOperation async = SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Single);
            async.allowSceneActivation = false;
            while (!async.isDone)
            {
                if (loadingImage != null)
                {
                    loadingImage.Rotate(new Vector3(0, 0, -Time.deltaTime * speed));
                }
                if (async.progress > 0.8999f)
                    async.allowSceneActivation = true;
                yield return null;
            }
            yield return null;
            if (loadingImage != null)
                loadingImage.gameObject.SetActive(false);
            
        }
    }
}
