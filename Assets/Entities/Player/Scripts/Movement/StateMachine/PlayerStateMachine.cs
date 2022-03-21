using System;
using Entities.Player.Scripts.Movement.MovementInput;
using Entities.Player.Scripts.Movement.StateMachine.States;
using UnityEngine;

namespace Entities.Player.Scripts.Movement.StateMachine
{
    [RequireComponent(typeof(CharacterInputHandler))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerStateMachine : MonoBehaviour
    {
        [Header("Move parameters")] 
        [Range(0f, 10f)] public float walkSpeed = 3f;
        [Range(0f, 5f)] public float runModifier = 1.5f;
        [Range(0f, 1f)] public float minSlidingTime = 0.2f;
        [Range(0f, 3f)] public float slopeSpeed = 1f;
        [Range(1f, 3f)] public float slidingSpeedUpModifier = 1.2f;
        [Space]
        public float jumpStrength = 4f;
        public bool isGravitated = true;
        public float gravity = -9.81f;
        public float minStateTime = 0.1f;
        [Space]
        [Header("Move Curves")]
        public AnimationCurve runSpeedUpCurve;
        public AnimationCurve stopAfterRunCurve;
        public AnimationCurve slopeSpeedModifierCurve;
        public AnimationCurve slidingSlopeSpeed;
        [Header("Parkour parameters")] 
        
        [Header("Fight parameters")]
        
        [NonSerialized] public CharacterController CharacterController;
        [NonSerialized] public CharacterInputHandler CharacterInput;
        [NonSerialized] public PlayerMover PlayerMover;
        [NonSerialized] public Transform Transform;

        [NonSerialized] public string GravityMoveName = "Gravity Move";
        [NonSerialized] public string StateMoveName = "State Move";
        
        private IState _currentState;
        private StateFactory _factory;

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            CharacterInput = GetComponent<CharacterInputHandler>();
            PlayerMover = GetComponent<PlayerMover>();
            Transform = GetComponent<Transform>();

            _factory = new StateFactory(this);
            _currentState = _factory.MovementState();
            _currentState.EnterState();
        }

        private void Update()
        {
            if (_currentState.TrySwitchState(out var newState)) SwitchState(newState);
            _currentState.UpdateState();
        }

        private void OnDisable() => _currentState.ExitState();


        private void SwitchState(IState newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public void SetDefaultGravity()
        {
            var possibleGravitation = gravity * Time.deltaTime;
            
            if(!isGravitated) PlayerMover.ChangeMove(GravityMoveName, Vector3.zero);
            else PlayerMover.ChangeMove(GravityMoveName, new Vector3(0f, possibleGravitation, 0f));
        }

        public float GetSlopeModifier()
        {
            var position = Transform.position;

            var sourceRay = new Ray(position, Vector3.down);
            var frontRay = new Ray(position + Transform.forward * 0.05f, Vector3.down);

            if (!Physics.Raycast(sourceRay, out var sourceHit, CharacterController.height)) return 1f;
            var rawModifier = slopeSpeedModifierCurve.Evaluate(Vector3.Angle(Vector3.up, sourceHit.normal));
            if (!Physics.Raycast(sourceRay, out var frontHit, CharacterController.height + 1f) ||
                sourceHit.transform.position.y - frontHit.transform.position.y == 0) return 1f;

            if (sourceHit.transform.position.y > frontHit.transform.position.y) rawModifier += 1f;
            return rawModifier;
        }

        public Vector3 GetLastMoveVector()
        {
            var rawInput = CharacterInput.InputLog[0];
            var vector3Input = new Vector3(rawInput.x, 0f, rawInput.y);

            vector3Input *= Time.deltaTime;

            return vector3Input;
        }
        
        public bool TryGetIncline(out float incline)
        {
            incline = 0f;
            var ray = new Ray(Transform.position, Vector3.down);
            if(!Physics.Raycast(ray, out var hitInfo, CharacterController.height)) return false;
            
            incline = Vector3.Angle(Vector3.up, hitInfo.normal);
            return true;
        }
    }
}
