using System.Collections;
using UnityEngine;

public class PlayerButtonsController : MonoBehaviour
{
    [SerializeField] private NumberButtonController buttonNorthController;
    [SerializeField] private NumberButtonController buttonSouthController;

    private void Start()
    {
        StartCoroutine(TempButtonActivationCoroutine());
        ClearBoth();
    }

    public NumberButtonController GetNorthButtonController() { return buttonNorthController; }

    public NumberButtonController GetSouthButtonController() { return buttonSouthController; }

    public bool HasNumber(int number)
    {
        return ((!buttonNorthController.isClear && buttonNorthController.buttonNumber == number) || 
            (!buttonSouthController.isClear && buttonSouthController.buttonNumber == number));
    }

    public void ClearBoth()
    {
        buttonNorthController.Clear();
        buttonSouthController.Clear();
    }

    private IEnumerator TempButtonActivationCoroutine()
    {
        float timeBeweenActivations = 5.0f;

        while (true)
        {
            if (buttonNorthController.isClear || buttonSouthController.isClear)
            {
                bool northAppear = Random.value > 0.5f;

                if (!buttonNorthController.isClear && buttonSouthController.isClear)
                {
                    northAppear = false;
                }

                if (buttonNorthController.isClear && !buttonSouthController.isClear)
                {
                    northAppear = true;
                }

                int randomNumber = Random.Range(0, 10);

                if (northAppear) {
                    buttonNorthController.Activate(randomNumber);
                } else {
                    buttonSouthController.Activate(randomNumber);
                }

            }

            yield return new WaitForSeconds(timeBeweenActivations);
        }
    }
}
