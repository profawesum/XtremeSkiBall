using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        [Serializable]
        public class MovementSettings
        {
            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float RunMultiplier = 2.0f;   // Speed when sprinting
            public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 30f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;
            //public Animator anim;


#if !MOBILE_INPUT
            private bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
                if (input == Vector2.zero) return;
                //anim.SetBool("Run", true);

                if (input.x > 0 || input.x < 0)
                {
                    //strafe
                    CurrentTargetSpeed = StrafeSpeed;
                }
                if (input.y < 0)
                {
                    //backwards
                    CurrentTargetSpeed = BackwardSpeed;
                }
                if (input.y > 0)
                {
                    //forwards
                    //handled last as if strafing and moving forward at the same time forwards speed should take precedence
                    CurrentTargetSpeed = ForwardSpeed;
                }
#if !MOBILE_INPUT
                if (Input.GetKey(RunKey))
                {
                    CurrentTargetSpeed *= RunMultiplier;
                    m_Running = true;
                }
                else
                {
                    m_Running = false;
                }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_Running; }
            }
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }

        public bool hasBall;
        public GameObject pauseCanvas;

        public Image HUD;

        public float upThrust;
        public Camera cam;
        public Camera AnimationCam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();
        public int PlayerNumber;

        [SerializeField] private float ChargeCooldownMax;
        [SerializeField] private float ChargeTimeMax;
        [SerializeField] private float ChargeSpeed = 100.0f;
        
        public Animator animController;

        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal, m_OldVelocity;
        private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded, m_IsCharged, m_WasHit;
        private float m_ChargeCooldown;
        private float m_ChargeTime;
        private float m_HitTime;
        private BoxCollider m_BoxCollider;
        private float m_HitTimeStart = 4.0f;
        //public Animator animator;

        public AudioSource source;
        public AudioClip hitsfx;
        bool fastDrop = true;

        public float timer;

        public bool IsChargeEnd { get; private set; } = true;

        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }

        public bool Running
        {
            get
            {
 #if !MOBILE_INPUT
				return movementSettings.Running;
#else
	            return false;
#endif
            }
        }


        private void Start()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init(transform, cam.transform);
        }



        private void Update()
        {
            timer -= Time.deltaTime;

            //reset the player speed after they get hit with the stun ball
            if (timer <= 0) {
                movementSettings.ForwardSpeed = 25;
                movementSettings.BackwardSpeed = 25;
            }

            //animator.SetBool("Hit", false);
            if (Input.GetButtonDown("J" + PlayerNumber + "C")) {
                animController.SetTrigger("Dash");
            }

            RotateView();
            if (Input.GetButtonDown("J" + PlayerNumber + "A") && !m_Jump)
            {
                animController.SetTrigger("Jump");
                m_Jump = true;
            }
            if (m_RigidBody.velocity.y > 0.1) {
                animController.SetTrigger("inAir");
            }
            if (m_RigidBody.velocity.y < 0.1) {
                animController.SetTrigger("Land");
            }
            if (Input.GetButtonDown("J1Start")) {
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            if (Input.GetButtonDown("J" + PlayerNumber + "B")) {
                animController.SetTrigger("Shoot");
            }
            if (m_RigidBody.velocity == Vector3.zero) {
                //animController.SetTrigger("Run");
                animController.SetTrigger("Idle");
            }
            if (m_RigidBody.velocity.x > 0.1 || m_RigidBody.velocity.z > 0.1 || m_RigidBody.velocity.y > 0.1) {
                animController.SetTrigger("Run");
            }
        }


        private void FixedUpdate()
        {
            GroundCheck();
            ChargeAttack();
            QuickDrop();

            Vector2 input = GetInput();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
            {
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                desiredMove.x = desiredMove.x*movementSettings.CurrentTargetSpeed;
                desiredMove.z = desiredMove.z*movementSettings.CurrentTargetSpeed;
                desiredMove.y = desiredMove.y*movementSettings.CurrentTargetSpeed;
                if (m_RigidBody.velocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed*movementSettings.CurrentTargetSpeed))
                {
                    m_RigidBody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);
                }
            }

            if (m_IsGrounded)
            {
                m_RigidBody.drag = 5f;
                fastDrop = true;
                if (m_Jump)
                {
                    m_RigidBody.drag = 0f;
                    m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                    m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                    m_Jumping = true;
                }

                if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
                {
                    m_RigidBody.Sleep();
                }
                if(!IsChargeEnd)
                {
                    m_RigidBody.drag = 0f;
                }
                if(m_WasHit)
                {
                    if(m_HitTime <= 0)
                    {
                        m_RigidBody.velocity = new Vector3(0f, 0f);
                        m_WasHit = false;
                    }
                    m_HitTime = m_HitTime - Time.deltaTime;
                }
            }
            else
            {
                m_RigidBody.drag = 0f;
                if (m_PreviouslyGrounded && !m_Jumping)
                {
                    StickToGroundHelper();
                }
            }
            m_Jump = false;
        }


        public float gravityMultiplier = 10;

        private void QuickDrop()
        {
            if (Input.GetButtonDown("J" + PlayerNumber + "RightTrigger") && fastDrop)
            {
                fastDrop = false;
                m_RigidBody.AddForce(Physics.gravity * gravityMultiplier); // Change the 2f to increase and decrease the force
            }
        }

        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "killFloor") {
                this.transform.position = new Vector3(0, 15.0f, 0);
                this.m_RigidBody.velocity = new Vector3 (0,0,0);
                
            }
            //push the player
            if (other.tag == "impactBallThrown")
            {
                if (timer <= 0)
                {
                    //source.PlayOneShot(hitsfx, 0.7F);
                    m_RigidBody.velocity = m_RigidBody.velocity + other.GetComponent<Rigidbody>().velocity;
                    m_HitTime = m_HitTimeStart;
                    m_WasHit = true;
                    timer = 3.5f;
                }
            }
            //stun the player
            if (other.tag == "stunBallThrown")
            {
                if (timer <= 0)
                {
                    movementSettings.BackwardSpeed = 0;
                    movementSettings.ForwardSpeed = 0;
                    timer = 2.0f;
                }
            }

            if (other.tag == "Player" && other.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().IsChargeEnd == false && IsChargeEnd == true)
            {
                //source.PlayOneShot(hitsfx, 0.7F);
                m_RigidBody.velocity = m_RigidBody.velocity + other.GetComponent<Rigidbody>().velocity;
                m_HitTime = m_HitTimeStart;
                m_WasHit = true;
            }
            if (other.tag == "JumpPad") {
                Debug.Log("Added force to jump up");
                m_RigidBody.AddForce(transform.up * (upThrust * Time.deltaTime));
            }
        }

        private void ChargeAttack()
        {
            
            if(Input.GetButtonDown("J" + PlayerNumber + "C") && m_IsCharged == false)
            {
                m_OldVelocity = m_RigidBody.velocity;
                m_RigidBody.velocity = transform.forward * ChargeSpeed;
                m_IsCharged = true;
                m_ChargeTime = ChargeTimeMax;
                m_ChargeCooldown = ChargeCooldownMax;
                IsChargeEnd = false;
            }
            if(m_IsCharged)
            {
                if(m_ChargeCooldown <= 0)
                {
                    m_IsCharged = false;
                }
                else if(m_ChargeTime <= 0 && !IsChargeEnd)
                {
                    m_RigidBody.velocity = m_OldVelocity;
                    IsChargeEnd = true;
                }
                m_ChargeCooldown = m_ChargeCooldown - Time.deltaTime;
                m_ChargeTime = m_ChargeTime - Time.deltaTime;
            }
        }

        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = Input.GetAxis("J" + PlayerNumber + "Horizontal"),
                    y = Input.GetAxis("J" + PlayerNumber + "Vertical")
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation (transform, cam.transform , PlayerNumber);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.velocity = velRotation*m_RigidBody.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
            }
        }
    }
}
