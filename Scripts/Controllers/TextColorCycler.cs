using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextColorCycler : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    public float speed = 200f;

    private int curColor = 0; //0, 1, 2 각각 Red, Green, Blue
    private float[] rgbValues = { 81f, 81f, 81f };
    private bool ascending = true; //색상 값이 증가중인지 감소중인지를 나타내는 변수

    void Update()
    {
        CycleColor();
        if (image != null)
            image.transform.Rotate(0, 0, 50f * Time.deltaTime);
    }

    void CycleColor()
    {
        if (ascending)
        {
            rgbValues[curColor] += speed * Time.deltaTime;
            if (rgbValues[curColor] >= 255f)
            {
                rgbValues[curColor] = 255f;
                ascending = false;
                curColor = (curColor + 1) % 3;
            }
        }
        else
        {
            rgbValues[curColor] -= speed * Time.deltaTime;
            if (rgbValues[curColor] <= 81f)
            {
                rgbValues[curColor] = 81f;
                ascending = true;
                curColor = (curColor + 1) % 3; 
            }
        }
        text.color = new Color(rgbValues[0] / 255f, rgbValues[1] / 255f, rgbValues[2] / 255f);
    }

}
