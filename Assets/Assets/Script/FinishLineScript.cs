    using UnityEngine;

    public class FinishLineScript : MonoBehaviour
    {
        public int level = 1;
        void Start()
        {
            //GameManager.Instance.LoadGame();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Activator"))
            {
                GameManager.Instance.currentLevel = level;

                GameManager.Instance.LevelFinish();
            }
        }
    }
