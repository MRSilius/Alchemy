using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    private YandexSDK _yandex;

    [SerializeField] private Image _logo;
    [SerializeField] private Sprite _logoRu;
    [SerializeField] private Sprite _logoEn;
    [SerializeField] private Sprite _logoTr;

    [SerializeField] private Text _diffText;
    [SerializeField] private Text _easyText;
    [SerializeField] private Text _mediumText;
    [SerializeField] private Text _hardText;
    [SerializeField] private Text _randomText;

    [SerializeField] private Text _levelsText;

    [SerializeField] private Text _skipText;
    [SerializeField] private Text _help_1Text;
    [SerializeField] private Text _help_2Text;
    [SerializeField] private Text _winText;
    [SerializeField] private Text _windeskText;



    private void Start()
    {
        _yandex = YandexSDK.instance;
        _yandex.GetEnvironment();
    }

    public void SetLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                if (_logo)
                {
                    _logo.sprite = _logoRu;

                    _diffText.text = "СЛОЖНОСТЬ";
                    _easyText.text = "ЛЕГКО";
                    _mediumText.text = "НОРМАЛЬНО";
                    _hardText.text = "СЛОЖНО";
                    _randomText.text = "СЛУЧАЙНО";

                    _levelsText.text = "уровни";
                }

                if (_skipText)
                {
                    _skipText.text = "Пропустить";
                    _help_1Text.text = "В одной колбе может быть только один цвет";
                    _help_2Text.text = "Разлейте зелье по колбам, чтобы получить зелье любви!";
                    _winText.text = "Победа!";
                    _windeskText.text = "Вы сварили зелье!";
                }
                break;
            case "en":
                if (_logo)
                {
                    _logo.sprite = _logoEn;

                    _diffText.text = "DIFFICULTY";
                    _easyText.text = "EASY";
                    _mediumText.text = "MEDIUM";
                    _hardText.text = "HARD";
                    _randomText.text = "RANDOM";

                    _levelsText.text = "levels";
                }
                if (_skipText)
                {
                    _skipText.text = "Skip";
                    _help_1Text.text = "There can be only one color in one flask";
                    _help_2Text.text = "Pour the potion into flasks to get a love potion!";
                    _winText.text = "Victory!";
                    _windeskText.text = "You brewed a potion!";
                }
                break;
            case "tr":
                if (_logo)
                {
                    _logo.sprite = _logoTr;

                    _diffText.text = "zorluk";
                    _easyText.text = "kolay";
                    _mediumText.text = "ORTA";
                    _hardText.text = "orta";
                    _randomText.text = "rasgele";

                    _levelsText.text = "seviyeler";
                }
                if (_skipText)
                {
                    _skipText.text = "Atlamak";
                    _help_1Text.text = "Bir şişede sadece bir renk olabilir";
                    _help_2Text.text = "Bir aşk iksiri elde etmek için iksiri şişelere dökün!";
                    _winText.text = "Zafer!";
                    _windeskText.text = "Bir iksir hazırladın!";
                }
                break;
            default:
                break;
        }
    }


}
