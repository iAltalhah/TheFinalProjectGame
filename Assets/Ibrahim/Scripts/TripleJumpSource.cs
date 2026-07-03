using UnityEngine;

public class TripleJumpSource : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AbilityManager abilityManager = other.GetComponentInParent<AbilityManager>();

        if (abilityManager == null)
            return;

        bool accepted = abilityManager.GiveTripleJumpPowerUp(this);

        if (!accepted)
            return;

        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}