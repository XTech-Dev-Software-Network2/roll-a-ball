using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [RequireComponent(typeof(Button))]
    public class LevelUIComponent : MonoBehaviour
    {
        public Text LevelName;
        public Text Difficulty;
        private Button button;

        protected void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(LevelSelected);
        }

        protected void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SetText(string levelName, int difficulty, bool isPlayable)
        {
            LevelName.text = levelName;
            var sb = new StringBuilder("");
            for (int i = 0; i < 5; i++)
            {
                sb.Append(i < difficulty ? "★" : "☆");
            }
            Difficulty.text = sb.ToString();
            button.interactable = isPlayable;
        }

        private void LevelSelected()
        {
            if (GameInstance.Instance)
            {
                GameInstance.Instance.LevelSelectedOnMenu(LevelName.text);
            }
        }
    }
}
