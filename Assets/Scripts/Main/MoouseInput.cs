using UnityEngine;

namespace Mirin
{
    public class MoouseInput : MonoBehaviour
    {
        [SerializeField] ScoreManager scoreManager;
        [SerializeField] Camera mainCamera;
        [SerializeField] int score = 100000000;

        void Start()
        {
            if(scoreManager == null)
            {
                scoreManager = MyStatic.FindComponent<ScoreManager>();
            }
            if(mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hit2ds = Physics2D.RaycastAll(ray.origin, ray.direction);

                foreach(var hit2d in hit2ds)
                {
                    if (hit2d)
                    {
                        var clickedGameObject = hit2d.transform.gameObject;
                        if (clickedGameObject.CompareTag("Ball") == false) return;
                        clickedGameObject.SetActive(false);
                        scoreManager.GetScore(score);
                    }
                }
            }
        }
    }
}