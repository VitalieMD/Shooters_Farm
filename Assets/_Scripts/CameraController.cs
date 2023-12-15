using Cinemachine;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(Animator))]
    public class CameraController : MonoBehaviour
    {
        public AxisState xAxis, yAxis;
        [SerializeField] private Transform _camPosition;
        private Animator _animator;
        private int isAimingParameter = Animator.StringToHash("isAiming");

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!GameManager.gameManagerInstance.isPaused)
            {
                bool isAiming = Input.GetMouseButton(1);
                _animator.SetBool(isAimingParameter, isAiming);
            }
        }

        private void FixedUpdate()
        {
            if (!GameManager.gameManagerInstance.isPaused)
            {
                xAxis.Update(Time.deltaTime);
                yAxis.Update(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            var localEulerAngles = _camPosition.localEulerAngles;
            localEulerAngles =
                new Vector3(yAxis.Value, localEulerAngles.y, localEulerAngles.z);
            _camPosition.localEulerAngles = localEulerAngles;

            var eulerAngles = transform.eulerAngles;
            eulerAngles = new Vector3(eulerAngles.x, xAxis.Value, eulerAngles.z);
            transform.eulerAngles = eulerAngles;
        }
    }
}