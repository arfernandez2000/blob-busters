using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public enum SlimeAnimationState { Idle,Walk,Jump,Attack,Damage,Follow,Dying }

public class Blob : Character
{
    #region PRIVATE_PROPERTIES
    public EnemyStats Stats => _enemyStats;
    [SerializeField] private EnemyStats _enemyStats;
    #endregion
    private float onHitFlashTime = 0.2f;
    Color origionalColor;
    public float Damage => _enemyStats.Damage;
    // [SerializeField] protected float damage = 5f;
    [SerializeField] private SkinnedMeshRenderer SkinnedMeshRenderer;

    [SerializeField] private Face faces;
    [SerializeField] private GameObject SmileBody;
    [SerializeField] private SlimeAnimationState currentState; 
   
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform mainCharacter;
    [SerializeField] private int damType;
    private AudioSource _audioSource;

    private int m_CurrentWaypointIndex;

    private bool move;
    private Material faceMaterial;
    private Vector3 originPos;
    private SlimeAnimationState lastState;
    public enum WalkType { Patroll ,ToOrigin }
    private WalkType walkType;
    Vector3 startScale;
    public float ShrinkDuration = 2.5f;
    public Vector3 TargetScale = Vector3.one * 0.5f;
    float t = 0;
    void Start()
    {
        GameObject meshRenderer = transform.Find("MeshRenderer").gameObject;
        SkinnedMeshRenderer = meshRenderer.gameObject.GetComponent<SkinnedMeshRenderer>();
        origionalColor = SkinnedMeshRenderer.material.color;
        originPos = transform.position;
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        walkType = WalkType.Patroll;
        startScale = transform.localScale;
        t = 0;
        mainCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        base.stats = _enemyStats;
        base.Start();
    }

    public void WalkToNextDestination()
    {
        currentState = SlimeAnimationState.Walk;
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        SetFace(faces.WalkFace);
    }
    public void CancelGoNextDestination() =>CancelInvoke(nameof(WalkToNextDestination));

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    void FlashRed() {
        SkinnedMeshRenderer.material.color = Color.red;
        Invoke("ResetColor", onHitFlashTime);
    }

    void ResetColor() {
        SkinnedMeshRenderer.material.color = origionalColor;
    }

    // Update is called once per frame
    void Update()
    {   
        switch (currentState)
        {
            case SlimeAnimationState.Dying:
                transform.GetChild(0).transform.Rotate(Vector3.forward.normalized, 30f * Time.deltaTime);
                transform.GetChild(0).transform.Rotate(Vector3.down.normalized, 30f * Time.deltaTime);

                t += Time.deltaTime / ShrinkDuration;
                Vector3 newScale = Vector3.Lerp(startScale, TargetScale, t);
                transform.localScale = newScale;
                return;

            case SlimeAnimationState.Idle:
                
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;
                StopAgent();
                SetFace(faces.Idleface);
                break;

            case SlimeAnimationState.Walk:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) return;

                agent.isStopped = false;
                agent.updateRotation = true;

                if (Vector3.Distance(transform.position, mainCharacter.position) < 15f) {
                    currentState = SlimeAnimationState.Follow;
                    return;
                }

                if (walkType == WalkType.ToOrigin) {
                    agent.SetDestination(originPos);
                    // Debug.Log("WalkToOrg");
                    SetFace(faces.WalkFace);
                    // agent reaches the destination
                    if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        walkType = WalkType.Patroll;

                        //facing to camera
                        transform.rotation = Quaternion.identity;

                        currentState = SlimeAnimationState.Idle;
                    }
                       
                } else {
                    if (waypoints[0] == null) return;
                   
                     agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

                    // agent reaches the destination
                    if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        currentState = SlimeAnimationState.Idle;

                        //wait 2s before go to next destionation
                        Invoke(nameof(WalkToNextDestination), 2f);
                    }

                }
                // set Speed parameter synchronized with agent root motion moverment
                animator.SetFloat("Speed", agent.velocity.magnitude);
                

                break;

            case SlimeAnimationState.Jump:

                // if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;

                StopAgent();
                SetFace(faces.jumpFace);
                animator.SetTrigger("Jump");

                //Debug.Log("Jumping");
                break;

            case SlimeAnimationState.Attack:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
                StopAgent();
                SetFace(faces.attackFace);
                animator.SetTrigger("Attack");

                agent.SetDestination(mainCharacter.position);
                if (Vector3.Distance(transform.position, mainCharacter.position) >= 3f) {
                    currentState = SlimeAnimationState.Follow;
        
                }
                break;
            
            case SlimeAnimationState.Follow:

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Follow")) return;

                agent.isStopped = false;
                agent.updateRotation = true;
                agent.SetDestination(mainCharacter.position);
                SetFace(faces.WalkFace);
                animator.SetFloat("Speed", agent.velocity.magnitude);
                if (agent.remainingDistance < agent.stoppingDistance + 2f) {
                    currentState = SlimeAnimationState.Attack;
                }            
                if (agent.remainingDistance > agent.stoppingDistance + 25f) {
                    currentState = SlimeAnimationState.Walk;
                }

                break;

            case SlimeAnimationState.Damage:

               // Do nothing when animtion is playing
               if(animator.GetCurrentAnimatorStateInfo(0).IsName("Damage0")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage1")
                    || animator.GetCurrentAnimatorStateInfo(0).IsName("Damage2") ) return;

                StopAgent();
                animator.SetTrigger("Damage");
                animator.SetInteger("DamageType", damType);
                SetFace(faces.damageFace);

                //Debug.Log("Take Damage");
                currentState = lastState;
                break;
       
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "SimpleSpell") {
            // FlashRed();
            lastState = currentState;
            currentState = SlimeAnimationState.Damage;
            EventQueueManager.instance.AddCommand(new CmdApplyDamage(this, col.gameObject.GetComponent<ISpell>().Damage));
            col.gameObject.GetComponent<ISpell>().Die();
            EventsManager.instance.SoundEffect(_audioSource.clip);
        }
    }
    private void StopAgent()
    {
        agent.isStopped = true;
        animator.SetFloat("Speed", 0);
        agent.updateRotation = false;
    }

    void OnAnimatorMove()
    {
        // apply root motion to AI
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }

    public override void Die() {
        currentState = SlimeAnimationState.Dying;
        animator.enabled = false;
        Destroy(gameObject, 2.5f);
    }
}
