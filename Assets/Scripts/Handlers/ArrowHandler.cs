using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Handlers
{
    public class ArrowHandler : MonoBehaviour
    {
        public static ArrowHandler Instance;
        [SerializeField] private Sprite Image;
        private Vector3 InitialScale;
        private float CameraZ;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InitialScale = transform.localScale;
            CameraZ = Camera.main.WorldToScreenPoint(transform.position).z;
        }

        private void Update()
        {
            if (!gameObject.GetComponentInChildren<Image>().enabled)
                return;
            var mouseInput = Input.mousePosition;
            var mouseScreenPosition = new Vector3(mouseInput.x, mouseInput.y, CameraZ);
            var mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            if(Input.GetMouseButton(0))
            {
                float distance = Vector3.Distance(gameObject.transform.position, mousePosition);
                gameObject.transform.localScale = new Vector3(InitialScale.x, distance, InitialScale.z);
                transform.up = mousePosition - gameObject.transform.position + (Vector3.left * 0.5f);
            }
            if(Input.GetMouseButtonUp(0))
            {
                gameObject.GetComponentInChildren<Image>().enabled = false;
            }
        }
        public void CreateArrow(Vector3 position)
        {
            position = new Vector3(position.x, position.y, CameraZ);
            gameObject.GetComponentInChildren<Image>().enabled = true;
            gameObject.transform.SetPositionAndRotation(position , Quaternion.identity);
        }
    }
}