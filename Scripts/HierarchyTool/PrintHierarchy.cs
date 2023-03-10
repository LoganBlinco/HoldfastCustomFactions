﻿using SceneChecker.Scripts.Logging;
using UnityEngine;

namespace SceneChecker.HierarchyTool
{
    public class PrintHierarchy : MonoBehaviour
    {
       
        public void Update()
        {
            return;
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
            {
                DebugSpewEverything();
            }
        }


        private void DebugSpewEverything()
        {
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                Debug.Log($"=====  SCENE {UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name} =====");
                GameObject[] allObjects = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).GetRootGameObjects();
                foreach (GameObject gameObj in allObjects)
                {
                    int level = 0;
                    PrintAllChildNames(gameObj, level);
                }
            }
        }

        public static void PrintAllChildNames(GameObject gameObj, int level)
        {
            level++;
            string indent = "";
            for (int i = 1; i <= level; i++)
            {
                indent += " - ";
            }
            Debug.Log($"{indent} {gameObj.name} enabled? {gameObj.active}");
            foreach (Transform transform in gameObj.transform.GetComponentInChildren<Transform>())
            {
                if (transform.gameObject.activeInHierarchy)
                {
                    PrintAllChildNames(transform.gameObject, level);
                }
            }
        }
    }
}