using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI quotesText;
        private static readonly string[] QuotesList =
        {
            "전쟁의 첫번째 희생자는 진실이다.\n- 아이스퀼로스 -",
            "정치는 피흘리지 않는 전쟁이고, 전쟁은 피를 흘리는 정치다.\n- 마오쩌둥 -",
            "평화로울 때는 자식이 부모를 땅에 묻는다. 전쟁이 일어나면 부모가 자식을 땅에 묻는다.\n- 헤로도토스 -",
            "전쟁은 즐거워요. 죽지만 않는다면요.\n- 닉 윌리엄스 중위 -",
            "무기는 설사 백 년 동안 쓸 일이 없다 해도, 단 하루도 갖추지 않을 수 없다.\n- 정약용 -",
            "전쟁이 터지면 누가 죽습니까? 바로 니가 죽습니다.\n- 김경진 -",
            "겪어보지 못한 자에게 전쟁이란 달콤한 것이다.\n- 에라스뮈스 -",
            "좋은 전쟁, 나쁜 평화란 이 세상에 있었던 적이 없다.\n- 벤저민 프랭클린 -",
        };
        private void Start()
        {
            quotesText.text = QuotesList[Random.Range(0, QuotesList.Length)];
            SceneLoadingManager.Instance.OnProgressUpdated += SliderUpdate;
        }

        private void OnDestroy()
        {
            SceneLoadingManager.Instance.OnProgressUpdated -= SliderUpdate;
        }

        private void SliderUpdate(float progress)
        {
            slider.value = progress;
        }
    }
}
