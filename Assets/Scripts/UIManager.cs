using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    private GameObject[] gradeobjs; // CardUI의 Card들의 배열

    private int grade = 0; // 등급
    private const int maxGrade = 3; // 최대 등급

    public static UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // 씬 전환이 유지되도록
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // GameOver UI 비활성화
        GameObject.Find("Fade").SetActive(false);


        gradeobjs = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {
            string gradePath = $"Canvas/UI/CardUI/Card{i}/Grade";
            gradeobjs[i] = GameObject.Find(gradePath);


            //print($"Card{i}");
            if (gradeobjs[i] == null)
            {
                Debug.LogWarning("Grade를 찾을 수 없습니다: " + gradePath);
            }
            // 비활성화
            Image image = gradeobjs[i].GetComponentInChildren<Image>();
            Text text = gradeobjs[i].GetComponentInChildren<Text>();
            if (image != null && text != null)
            {
                image.enabled = false;
                text.enabled = false;
            }
        }
    }

    // 등급을 증가시키는 함수
    public void GradeUpgrade()
    {
        if (grade < maxGrade)
        {
            grade++;
            UpdateGrade();
        }
    }

    // 모든 Grade의 Text를 설정하는 함수
    public void SetGradeText(string newText)
    {
        foreach (GameObject obj in gradeobjs)
        {
            if (obj != null)
            {
                Text text = obj.GetComponentInChildren<Text>();
                if (text != null)
                {
                    text.text = newText;
                }
                else
                {
                    Debug.LogWarning("Text 컴포넌트를 찾을 수 없습니다: " + obj.name);
                }
            }
        }
    }

    // Grade Update
    public void UpdateGrade()
    {
        // 활성화
        for (int i = 0; i < gradeobjs.Length; i++)
        {
            if (i < gradeobjs.Length && gradeobjs[i] != null)
            {
                Image image = gradeobjs[i].GetComponentInChildren<Image>();
                Text text = gradeobjs[i].GetComponentInChildren<Text>();
                if (image != null && text != null)
                {
                    image.enabled = true;
                    text.enabled = true;
                }
            }
        }
    }

    // Grade 값을 반환
    public int GetGrade()
    {
        return grade;
    }
}

