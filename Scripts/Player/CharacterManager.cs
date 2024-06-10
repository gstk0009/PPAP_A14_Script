using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CharacterManger").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    public Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }
}
