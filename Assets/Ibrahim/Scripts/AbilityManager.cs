using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] AbilityRewind rewind;

    bool hasRewindAbility = false;
    RewindSource currentRewindSource;

    [SerializeField] bool hasTripleJumpPowerUp = false;
    [SerializeField] bool hasFloatPowerUp = false;

    TripleJumpSource currentTripleJumpSource;

    void Awake()
    {
        if (rewind == null)
        {
            rewind = GetComponent<AbilityRewind >();
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

        Debug.Log("Triple jump power-up consumed.");
    }

    public bool GiveRewindAbility(RewindSource source)
    {
        // If player already has rewind, don't take another one.
        if (hasRewindAbility)
        {
            return false;
        }

        hasRewindAbility = true;
        currentRewindSource = source;

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

        Debug.Log("Rewind ability consumed.");
    }
}