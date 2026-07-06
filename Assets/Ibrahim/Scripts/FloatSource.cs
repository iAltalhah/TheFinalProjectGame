using UnityEngine;

public class FloatSource : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AbilityManager abilityManager = other.GetComponentInParent<AbilityManager>();

        if (abilityManager == null)
            return;

        bool accepted = abilityManager.GiveFloatPowerUp(this);

        if (!accepted)
            return;

        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}