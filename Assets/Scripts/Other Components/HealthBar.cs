using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Variables

    [SerializeField] private Image barImage;
    [SerializeField] [Min(0.25f)] private float updateSpeed;

    private Character character;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        character = GetComponentInParent<Character>();
    }

    private void OnEnable()
    {
        character.OnHealthPercentChanged += CharacterOnHealthPercentChanged;
    }

    private void OnDisable()
    {
        character.OnHealthPercentChanged -= CharacterOnHealthPercentChanged;
    }

    private void LateUpdate()
    {
        transform.up = Vector2.up;
    }

    #endregion


    #region Coroutines

    private IEnumerator ChangeToPercent(float percent)
    {
        var preChangePercent = barImage.fillAmount;
        var elapsed = 0f;

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            barImage.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsed / updateSpeed);
            yield return null;
        }

        barImage.fillAmount = percent;
    }

    #endregion


    #region Event Handlers

    private void CharacterOnHealthPercentChanged(float percent)
    {
        StartCoroutine(ChangeToPercent(percent));
    }

    #endregion
}