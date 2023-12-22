using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWand : MonoBehaviour, IWand
{
    #region IWAND_PROPERTIES
    public GameObject SpellPrefab => _spellPrefab;
    public Animator spellAnimator;
    public Transform SpellContainer => _spellContainer;
    public GameObject TeleportParticles => _teleportParticles;
    public RaycastHit hit;
    public GameObject teleportParticles;

    #endregion

    #region PRIVATE_PROPERTIES
    [SerializeField] private GameObject _spellPrefab;
    [SerializeField] private Transform _spellContainer;
    [SerializeField] public float TeleportCost;

    [SerializeField] public GameObject _teleportParticles;
    #endregion

    #region UNITY_EVENTS
    // Start is called before the first frame update
    void Start()
    {
        spellAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region IWAND_METHODS
    public void Shoot() {
        GameObject bullet = Instantiate(_spellPrefab, transform.position, transform.rotation, SpellContainer);
        bullet.GetComponent<SimpleSpell>().forward = transform.parent.forward;
    }

    public void Teleport() {
        hit = new RaycastHit();
        if (aimTeleport(out hit)) {
            Debug.Log(hit.point);
            float mana = GetComponentInParent<MainCharacter>().getMana();
            float maxMana = GetComponentInParent<MainCharacter>().getMaxMana();
            GetComponentInParent<MainCharacter>().setMana(mana - TeleportCost);
            EventsManager.instance.SpellCast(mana, maxMana);
            Invoke("ExecuteTeleport", 2.0f);
            teleportParticles = Instantiate(_teleportParticles, hit.point, Quaternion.identity);
        }
    }

    public void ExecuteTeleport() {
        transform.root.position = hit.point;
        Destroy(teleportParticles, 2.0f);
    }

    public bool aimTeleport(out RaycastHit hit) {
        return Physics.Raycast(transform.position, transform.parent.forward, out hit, 70, 1);
    }
    #endregion
}
