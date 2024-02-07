using UnityEngine;

namespace Mirin
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] ScoreManager scoreManager;
        [SerializeField] Camera mainCamera;

        void Start()
        {
            if(scoreManager == null)
            {
                scoreManager = MyHelper.FindComponent<ScoreManager>();
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

                bool isHit = false;
                foreach(var hit2d in hit2ds)
                {
                    if (hit2d && hit2d.transform.CompareTag("Ball"))
                    {
                        isHit = true;
                        var ball = hit2d.transform.GetComponent<Ball>();
                        ball.OnClicked();
                    }
                }
                Debug.Log(isHit ? "‚Å‚«‚½" : "‚Å‚«‚Ä‚È‚¢");
            }
        }
    }
}