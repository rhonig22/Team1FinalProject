using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform _prepSpawn;
    [SerializeField] private Transform _stoveTopSpawn;
    [SerializeField] private Transform _flatTopSpawn;
    [SerializeField] private Transform _fridgeSpawn;
    [SerializeField] private InputAction _playerControls;
    private readonly float _moveSpeed = 5f;
    private Vector3 _moveTowards;
    private Dictionary<Vector2, Transform> _spawnMap;
    public UnityEvent SameStationAgain = new UnityEvent();

    private void Start()
    {
        _moveTowards = transform.position;
        _spawnMap = new Dictionary<Vector2, Transform>()
        {
            {new Vector2(1, 0), _fridgeSpawn },
            {new Vector2(-1, 0), _flatTopSpawn },
            {new Vector2(0, 1), _stoveTopSpawn },
            {new Vector2(0, -1), _prepSpawn }
        };

        _playerControls.started += (context) => {
            var direction = context.ReadValue<Vector2>();
            MoveTowards(_spawnMap[direction]);
        };
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void MoveTowards(Transform moveTowards)
    {
        if (KitchenCanvasController.IsRhythmSection || RecipeManager.Instance.RecipeCompleted)
            return;

        if (_moveTowards == moveTowards.position)
        {
            SameStationAgain.Invoke();
        }
        else
        {
            _moveTowards = moveTowards.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moveTowards != null && (_moveTowards.x != transform.position.x || _moveTowards.y != transform.position.y))
        {
            transform.position = Vector3.Lerp(transform.position, _moveTowards, Time.fixedDeltaTime * _moveSpeed);
        }
    }
}
