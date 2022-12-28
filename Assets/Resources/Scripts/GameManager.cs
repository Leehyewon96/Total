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
            Debug.Log("이미 존재하는 캐릭터입니다.");
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
            Debug.Log("리스트에 존재하지 않는 캐릭터입니다.");
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
