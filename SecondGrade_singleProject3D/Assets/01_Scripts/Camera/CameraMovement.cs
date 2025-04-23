using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [field: SerializeField] private PlayerInputSO playerInput;
    [SerializeField] private float moveSpeed = 5;

    private void Update()
    {
        Vector3 moveDir = new Vector3(playerInput.MovementKey.x, 0, playerInput.MovementKey.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
