using UnityEngine;

public class CameraMovement: MonoBehaviour
{
    private Camera _camera;
    
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float zoomSpeed = 10f;
    
    private const float MinFov = 1f;
    private const float MaxFov = 50f;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.transform.position = new Vector3(GameManager.GetWidth() * GameManager.GetCellSize() / 2f, GameManager.GetHeight() * GameManager.GetCellSize() / 2f, -10);
    }
    private void Update()
    {
        MovementUpdate();
        CameraZoomUpdate();
    }

    private void MovementUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _camera.transform.Translate(movementSpeed * Time.deltaTime * Vector3.up);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _camera.transform.Translate(-movementSpeed * Time.deltaTime * Vector3.right);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            _camera.transform.Translate(-movementSpeed * Time.deltaTime * Vector3.up);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _camera.transform.Translate(movementSpeed * Time.deltaTime * Vector3.right);
        }
    }

    private void CameraZoomUpdate()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        
        _camera.orthographicSize -= scroll * zoomSpeed;
        
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, MinFov, MaxFov);
    }
}
