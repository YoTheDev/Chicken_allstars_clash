using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    
    [SerializeField] private GameObject attackBox;
    [SerializeField] private GameObject attack2Box;
    [SerializeField] private GameObject playerPivot;
    [SerializeField] private bool isNearGrounded,isJumpPressed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float nearGroundedRange;

    private bool _isGrounded;
    private float _saveSpeed;
    private float _axisX;
    private bool _attack;
    private bool _airAttack;
    private Rigidbody _rb;
    private CharacterController _characterController;
    private Vector3 _playerVelocity;

    void Start() {
        attackBox.SetActive(false); attack2Box.SetActive(false);
        _rb = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _saveSpeed = playerSpeed;
        isJumpPressed = false;
    }
    
    private void Update() {
        isNearGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), nearGroundedRange);
        if (_isGrounded && _airAttack) {
            CancelInvoke("AttackCooldown");
            attack2Box.SetActive(false);
            _attack = false;
        }
        Move();
    }

    private void Move() {
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0) {
            _playerVelocity.y = 0f;
        }
        _characterController.Move(new Vector3(-_axisX, 0, 0) * Time.deltaTime * playerSpeed);
        if (isJumpPressed && _isGrounded) {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            isJumpPressed = false;
        }
        _playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue Moving) {
        var rotation = transform.rotation;
        _axisX = Moving.Get<float>();
        if (_attack) return;
        if (_axisX < 0) {
            playerPivot.transform.rotation = Quaternion.Euler(rotation.x,180,rotation.z);
        }
        else if (_axisX > 0) {
            playerPivot.transform.rotation = Quaternion.Euler(rotation.x,0,rotation.z);
        }
    }

    public void OnJump() {
        if (isNearGrounded && !_attack) {
            isJumpPressed = true;
        }
    }

    public void OnAttack()
    {
        if (_attack) return;
        if (isNearGrounded) {
            attackBox.SetActive(true);
            playerSpeed = 0;
            _attack = true;
            Invoke("AttackCooldown",0.5f);
        }
        else {
            attack2Box.SetActive(true);
            _airAttack = true;
            Invoke("AttackCooldown",0.4f);
        }
    }

    public void AttackCooldown() {
        attackBox.SetActive(false); attack2Box.SetActive(false);
        playerSpeed = _saveSpeed;
        _attack = false; _airAttack = false;
    }
}
