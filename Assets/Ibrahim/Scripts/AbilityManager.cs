using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] AbilityRewind rewind;
    [SerializeField] AbilityUI abilityUI;

    bool hasRewindAbility = false;
    RewindSource currentRewindSource;

    [SerializeField] bool hasTripleJumpPowerUp = false;
    [SerializeField] bool hasFloatPowerUp = false;
    TripleJumpSource currentTripleJumpSource;

    void Awake()
    {
        if (rewind == null)
        {
            rewind = GetComponent<AbilityRewind>();
        }

    }


    FloatSource currentFloatSource;

    public bool HasFloatPowerUp()
    {
        return hasFloatPowerUp;
    }

    public bool GiveFloatPowerUp(FloatSource source)
    {
        if (hasFloatPowerUp)
        {
            return false;
        }

        hasFloatPowerUp = true;
        currentFloatSource = source;

        abilityUI?.SetFloatImage(true);

        Debug.Log("Player gained float power-up.");
        return true;
    }

    public void ConsumeFloatPowerUp()
    {
        if (!hasFloatPowerUp)
        {
            return;
        }

        hasFloatPowerUp = false;

        if (currentFloatSource != null)
        {
            currentFloatSource.Respawn();
        }

        currentFloatSource = null;
        abilityUI?.SetFloatImage(false);

        Debug.Log("Float power-up consumed.");
    }
    public bool HasTripleJumpPowerUp()
    {
        return hasTripleJumpPowerUp;
    }

    public bool GiveTripleJumpPowerUp(TripleJumpSource source)
    {
        if (hasTripleJumpPowerUp)
        {
            return false;
        }

        hasTripleJumpPowerUp = true;
        currentTripleJumpSource = source;

        abilityUI?.SetTripleJumpImage(true);

        Debug.Log("Player gained triple jump power-up.");
        return true;
    }

    public void ConsumeTripleJumpPowerUp()
    {
        if (!hasTripleJumpPowerUp)
        {
            return;
        }

        hasTripleJumpPowerUp = false;

        if (currentTripleJumpSource != null)
        {
            currentTripleJumpSource.Respawn();
        }

        currentTripleJumpSource = null;
        abilityUI?.SetTripleJumpImage(false);

        Debug.Log("Triple jump power-up consumed.");
    }

    public bool GiveRewindAbility(RewindSource source)
    {
        if (hasRewindAbility)
        {
            return false;
        }

        hasRewindAbility = true;
        currentRewindSource = source;

        abilityUI?.SetRewindImage(true);

        Debug.Log("Player gained rewind ability.");
        return true;
    }

    public void TryUseRewind()
    {
        if (!hasRewindAbility)
        {
            Debug.Log("No rewind ability.");
            return;
        }

        if (rewind == null)
        {
            Debug.LogWarning("No Rewind script found on player.");
            return;
        }

        RewindSource sourceToRespawn = currentRewindSource;

        bool started = rewind.StartRewind(() =>
        {
            if (sourceToRespawn != null)
            {
                sourceToRespawn.Respawn();
            }
        });

        if (!started)
        {
            return;
        }

        // Consume the ability.
        hasRewindAbility = false;
        currentRewindSource = null;
        abilityUI?.SetRewindImage(false);

        Debug.Log("Rewind ability consumed.");
    }
}