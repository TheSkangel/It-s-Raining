using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace Game
{
	[Serializable]
	public class PlayerController : MonoBehaviour
	{

		public float gravity = -1f; //
		public float runSpeed = 8f;
		public float groundDamping = 20f; // how fast do we change direction? higher means faster
		public float inAirDamping = 5f;
		public float jumpHeight = 3f;

		public float superJumpMultiplier = 2;

		public float fastfallSpeed = 1f;

		public bool canHover = true;
		public float hoverSpeed = 0.5f;
		public float maxHoverTime = 10f;
		private float timeHovering = 0;

		[HideInInspector]
		private float normalizedHorizontalSpeed = 0;

		private CharacterController2D _controller;
		private Animator _animator;
		private RaycastHit2D _lastControllerColliderHit;
		private Vector3 _velocity;
		public string hButton = "Horizontal_P1"; //input for horziontal axis
		public string vButton = "Vertical_P1"; //input for vertical axis

        public AudioClip[] _audioClips;

		public string rButton = "Run_P1"; //Input for boost axis
		public string jButton = "Jump_P1"; //input for jump axis

		public bool isGrounded;

        private Level _level;
        public DeathEffect _deathEffect;

		public string powerupState;

		//		public List<GameObject> _players;

		void Awake ()
		{

			//get components
			_animator = GetComponentInChildren<Animator>();
			_controller = GetComponent<CharacterController2D>();
            _deathEffect = GetComponent<DeathEffect>();

            _level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
			//			_players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

			// listen to some events for illustration purposes
			_controller.onControllerCollidedEvent += onControllerCollider;
			_controller.onTriggerEnterEvent += onTriggerEnterEvent;
			_controller.onTriggerExitEvent += onTriggerExitEvent;
		}

		#region Event Listeners

		void onControllerCollider( RaycastHit2D hit )
		{
            //		// bail out on plain old ground hits cause they arent very interesting
            //			if( hit.normal.y == 1f )
            //				return;

            //check to see if you've landed on someones head
            if (_controller.collisionState.below &&
				!_controller.collisionState.right &&
				!_controller.collisionState.left &&
				hit.collider.gameObject.tag == "Player") 


			{
				killPlayer (hit.collider.gameObject);
			}

			if (hit.collider.isTrigger && hit.collider.gameObject.tag == "Pickup") 
				
			{
				powerUp ();
			}

			//		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
			//		Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
		}

		void onTriggerEnterEvent( Collider2D col )
		{



			Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
		}


		void onTriggerExitEvent( Collider2D col )
		{
			Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
		}

		#endregion

		void FixedUpdate ()
		{

            if (GameController.state != "playing")
                return;

			{
				isGrounded = _controller.isGrounded;

				float v = Input.GetAxisRaw (vButton);	//vertical downwards movement
				float h = Input.GetAxisRaw (hButton);   //horizontal movement
				float j = Input.GetAxisRaw (jButton);	// jump Input
				float r = Input.GetAxisRaw (rButton);	// run input

				if (_controller.isGrounded) {
					_velocity.y = 0;
					canHover = true; timeHovering = 0; //reset values for hover fnctn
				}



				if( h>0 /*KeyCode.RightArrow*/ )
				{

					normalizedHorizontalSpeed = 1;
					if( transform.localScale.x < 0f )
						transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

					if( _controller.isGrounded)
						_animator.Play( Animator.StringToHash( "Run" ) );
				}
				else if( h<0/*KeyCode.LeftArrow )*/ )
				{
					normalizedHorizontalSpeed = -1;
					if( transform.localScale.x > 0f )
						transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

					if( _controller.isGrounded )
						_animator.Play( Animator.StringToHash( "Run" ) );
				}
				else if (_controller.isGrounded) //check that player is grounded before idling
				{
					normalizedHorizontalSpeed = 0;
					_animator.Play( Animator.StringToHash( "Idle" ) );
				}

				//vertical movement in the air

				if (!_controller.isGrounded && v<0) {
					_velocity.y -= fastfallSpeed;
				}

				//hover controlls
				if (!_controller.isGrounded && r > 0 && canHover) {

					_velocity.y = 0; // hover


					timeHovering += Time.deltaTime; //count how long we've been hovering

					//if we've been hovering too long
					if (timeHovering > maxHoverTime) {
						canHover = false;
					}
				


				}


			



				// we can only jump whilst grounded
				if( _controller.isGrounded && j>0)
				{
					_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
					_animator.Play( Animator.StringToHash( "Jump" ) );

                    SFXManager.instance.PlaySound("Jump");

				}

				// check if player is falling
				if(_controller.velocity.y < 0 && _controller.isGrounded == false)
				{
					_animator.Play( Animator.StringToHash( "Fall" ) );
				}




				// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
				var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
				_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

				// apply gravity before moving
				_velocity.y += gravity * Time.deltaTime;

				// 
				// super jump when holding down
				if( _controller.isGrounded &&  v<0  )
				{
					_velocity.y *= superJumpMultiplier;
					_controller.ignoreOneWayPlatformsThisFrame = true;
				}

				_controller.move( _velocity * Time.deltaTime );

				// grab our current _velocity to use as a base for all calculations
				_velocity = _controller.velocity;
			}
		}	

		public void killPlayer (GameObject deadPlayer)
		{

            deadPlayer.GetComponent<PlayerController>()._deathEffect.AnimateDeath();

            SFXManager.instance.PlaySound("Death");

            CameraShake2D.instance.ShakeCamera(0.1f, 1f, 1f);

            Destroy(deadPlayer);

            GameController.playersAlive--;
            
            EndGameCheck();

		}

		public void powerUp (){
			
		}

        void EndGameCheck() {

            if(GameController.playersAlive <= 1) {

                GameOver();

            }

        }

        void GameOver() {

            Invoke("ShowGameOverMenu", 1f);

        }

        void ShowGameOverMenu() {

            _level.ShowGameOverMenu();

        }

	}

}

