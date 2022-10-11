using UnityEngine;
using UnityEngine.InputSystem;

public class Player_management : MonoBehaviour {
    
    [SerializeField] private GameObject attackBox;
    [SerializeField] private GameObject attack2Box;
    [SerializeField] private GameObject playerPivot;
    [SerializeField] private bool isNearGrounded,isJumpPressed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpSpeed;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float airattackjumpHeight;
    [SerializeField] private float doubleJumpHeight;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float dJumpgravityValue = -9.81f;
    [SerializeField] private float nearGroundedRange;

    private bool _isGrounded;
    private float _saveSpeed;
    private float _saveGravityValue;
    private float _axisX;
    private bool _attack;
    private bool _airAttack;
    private bool _canAirAttack;
    private bool _saveAxisXpositive;
    private bool _doubleJump;
    private CharacterController _characterController;
    private Vector3 _playerVelocity;

    void Start() {
        attackBox.SetActive(false); attack2Box.SetActive(false);
        _characterController = GetComponent<CharacterController>();
        _saveSpeed = playerSpeed;
        _saveGravityValue = gravityValue;
        isJumpPressed = false;
    }

    private void Update() {
        isNearGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), nearGroundedRange);
    }

    private void FixedUpdate()
    {
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0) {
            _playerVelocity.y = 0f;
            _doubleJump = true;
            _canAirAttack = false;
            gravityValue = _saveGravityValue;
        }
        _characterController.Move(new Vector3(-_axisX, 0, 0) * Time.deltaTime * playerSpeed);
        if (isJumpPressed && _isGrounded) {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            isJumpPressed = false;
            _canAirAttack = true;
        }
        _playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue Moving) {
        var rotation = transform.rotation;
        _axisX = Moving.Get<float>();
        if (_axisX < 0) {
            if (!_attack && !_airAttack) {
                playerPivot.transform.rotation = Quaternion.Euler(rotation.x, 180, rotation.z);
            }
            _saveAxisXpositive = false;
        }
        else if (_axisX > 0) {
            if (!_attack && !_airAttack) {
                playerPivot.transform.rotation = Quaternion.Euler(rotation.x,0,rotation.z);
            }
            _saveAxisXpositive = true;
        }
    }

    public void OnJump() {
        if (isNearGrounded && !_attack) {
            isJumpPressed = true;
        }
        if (!_isGrounded && _doubleJump) {
            _playerVelocity.y = 0f;
            gravityValue = dJumpgravityValue;
            _playerVelocity.y += Mathf.Sqrt(doubleJumpHeight * -3.0f * gravityValue);
            _doubleJump = false;
            isJumpPressed = false;
            _canAirAttack = true;
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
        else if (_canAirAttack) {
            _doubleJump = false;
            gravityValue = _saveGravityValue;
            attack2Box.SetActive(true);
            _airAttack = true;
            _canAirAttack = false;
            _playerVelocity.y = 0;
            _playerVelocity.y += Mathf.Sqrt(airattackjumpHeight * -3.0f * gravityValue);
            Invoke("AttackCooldown",0.4f);
        }
    }

    public void AttackCooldown() {
        var rotation = transform.rotation;
        if (_saveAxisXpositive) {
            playerPivot.transform.rotation = Quaternion.Euler(rotation.x,0,rotation.z);
        }
        else {
            playerPivot.transform.rotation = Quaternion.Euler(rotation.x,180,rotation.z);
        }
        attackBox.SetActive(false); attack2Box.SetActive(false);
        playerSpeed = _saveSpeed;
        _attack = false; _airAttack = false;
    }
}
