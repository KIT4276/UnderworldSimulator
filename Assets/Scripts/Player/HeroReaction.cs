using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeroReaction : MonoBehaviour
{
    [SerializeField] private Text _reactionText;
    [SerializeField] private GameObject _reactionPanel;
    [SerializeField] private float _reactionTime = 3;

    private void Start()
    {
        _reactionPanel.SetActive(false);
    }

    public void ShowReaction(string text)
    {
        _reactionPanel.SetActive(true);
        _reactionText.text = text;

        StartCoroutine(HideReactionRoutine());
    }

    private IEnumerator HideReactionRoutine()
    {
        yield return new WaitForSeconds(_reactionTime);
        HideReaction();
    }

    public void HideReaction()
    {
        _reactionPanel.SetActive(false);
    }
}
