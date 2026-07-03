using UnityEngine;

public class RewindSource : MonoBehaviour
{
    bool isTaken = false;

    private void OnTriggerEnter(Collider other)
    {
        AbilityManager abilityManager = other.GetComponentInParent<AbilityManager>();

        if (abilityManager == null)
            return;

        bool accepted = abilityManager.GiveRewindAbility(this);

        if (!accepted)
            return;

        isTaken = true;
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        isTaken = false;
        gameObject.SetActive(true);
    }
}