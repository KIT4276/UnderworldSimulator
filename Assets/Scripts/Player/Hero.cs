using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private Text _reaction;
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _reactionTime = 3;

    private void Start()
    {
        _panel.SetActive(false);
    }


    public void ShowReaction(string text)
    {
        _panel.SetActive(true);
        _reaction.text = text;

        StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(_reactionTime);
        HideReaction();
    }

    private void HideReaction()
    {
        _panel.SetActive(false);
    }
}