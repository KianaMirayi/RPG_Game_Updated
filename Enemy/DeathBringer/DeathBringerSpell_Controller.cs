using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerSpell_Controller : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask whatIsPlayer;

    private CharacterStats stats;

    public void SetupSpell(CharacterStats _stats) => stats = _stats;

    private void SpellCastAnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position, boxSize,whatIsPlayer);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerStats>() != null)
            {
                hit.GetComponent<Entity>().SetUpKnockDir(transform);
                stats.DoDamage(hit.GetComponent<PlayerStats>());
                Debug.Log("player is damaged");
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(check.position, boxSize);
    }

    private void SelfDestory()
    {
        Destroy(gameObject);
    }
}
