using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject aboutPanel; // اللوحة الجديدة الخاصة بـ "حول اللعبة"

    public void GoToSettings()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void GoToAbout()
    {
        mainMenuPanel.SetActive(false); // إخفاء الأزرار الرئيسية
        aboutPanel.SetActive(true);    // إظهار لوحة حول اللعبة
    }

    public void GoBackToMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        aboutPanel.SetActive(false); // إخفاء لوحة حول اللعبة أيضاً عند العودة
    }

    public void QuitGame()
    {
       
        Application.Quit();
        Debug.Log("game is now closing!");
    }
}