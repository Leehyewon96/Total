using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Characters> CharacterList = new List<Characters>();

    public bool isReStarted = false;
    public bool isRoundEnd = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddCharacter(Characters character)
    {
        if(CharacterList.Contains(character))
        {
            Debug.Log("�̹� �����ϴ� ĳ�����Դϴ�.");
        }
        else
        {
            CharacterList.Add(character);
        }
    }

    public void RemoveCharacter(Characters character)
    {
        if (CharacterList.Contains(character))
        {
            CharacterList.Remove(character);
        }
        else
        {
            Debug.Log("����Ʈ�� �������� �ʴ� ĳ�����Դϴ�.");
        }
    }

    public void NotifyCharacters()
    {
        if(isReStarted)
        {
            foreach (Characters character in CharacterList)
            {
                character.Restart();
            }

            isReStarted = false;
        }
        else
        {
            foreach (Characters character in CharacterList)
            {
                character.RoundEnd();
            }
        }
    }
}
