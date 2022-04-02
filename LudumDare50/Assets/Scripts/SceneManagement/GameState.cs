using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public const int NUMBER_OF_SCENES = 7;

    private static int currentScene = 0;

    // 0 = Bad option, 1 = Okay option, 2 = Good option, 3 = No Option Chosen
    private static Dictionary<int, int> choices = new Dictionary<int, int>();

    public static int CurrentScene
    {
        get
        {
            return currentScene;
        }
        set
        {
            currentScene = value;
        }
    }

    public static Dictionary<int, int> Choices
    {
        get
        {
            return choices;
        }
        set
        {
            choices = value;
        }
    }

    public static int GetChoice(int sceneNumber)
    {
        if (choices.ContainsKey(sceneNumber))
        {
            return choices[sceneNumber];
        } else
        {
            return 3;
        }
    }

    public static void UpdateChoice(int sceneNumber, int choice)
    {
        if (choices.ContainsKey(sceneNumber))
        {
            choices[sceneNumber] = choice;
        } else
        {
            choices.Add(sceneNumber, choice);
        }
    }

    public static bool CanOpenDoor()
    {
        return GetChoice(currentScene) != 3;
    }

    public static void ResetAllChoices()
    {
        choices = new Dictionary<int, int>();
    }

    public static void ResetChoice(int sceneNumber)
    {
        UpdateChoice(sceneNumber, 3);
    }

    public static void ResetCurrentSceneChoice()
    {
        UpdateChoice(currentScene, 3);
    }

    private static int GetTotalPoints() {
        int total = 0;
        foreach(KeyValuePair<int,int> sceneValue in choices) {
            total += sceneValue.Value + 1;
        }   
        Debug.Log("Total points: " + total);
        return total;
    }

    public static int GetEndingValue() {
        int score = GetTotalPoints(); 
        if(score < 14) {
            return 0;
        } else if(score >= 14 && score < 18) {
            return 1;
        } else {
            return 2;
        }
    }
}
