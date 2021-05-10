using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Line98
{
    public class Game : MonoBehaviour
    {
        public GameObject object1;
        public AudioSource source;
        public Text text;
        [HideInInspector]
        public AudioClip clip;
        [SerializeField]
        private GameObject[] _balls = new GameObject[0];

        private Button[,] buttons;
        [SerializeField]
        private Image[] images = new Image[0];
        private Lines lines;

        void Start()
        {
            object1.SetActive(false);
            lines = new Lines(ShowBox, PlayCut, PlayEnd, PlayStart);
            InitButtons();
            ShowBox(1, 2, 3);
            lines.Start();
        }
        public void ShowBox(int x, int y, int ball)
        {
            //Debug.Log($"ShowBox({x}, {y}, {ball})");
            buttons[x, y].GetComponent<Image>().sprite = images[ball].sprite;
            //buttons[x, y] = Instantiate(_balls[ball], );
        }
        public void PlayCut()
        {
            source = source.GetComponent<AudioSource>();
            clip = Resources.Load("cut", typeof(AudioClip)) as AudioClip;
            source.clip = clip;
            source.Play();
            text.text = lines.Record.ToString();
        }

        public void PlayStart()
        {
            source = source.GetComponent<AudioSource>();
            clip = Resources.Load("startgame", typeof(AudioClip)) as AudioClip;
            source.clip = clip;
            source.Play();

        }
        public void PlayEnd()
        {
            source = source.GetComponent<AudioSource>();
            clip = Resources.Load("endgame", typeof(AudioClip)) as AudioClip;
            source.clip = clip;
            source.Play();
        }
        public void Click()
        {
            string name = EventSystem.current.currentSelectedGameObject.name;
            Debug.Log($"Click({name})");
            int nr = GetNumber(name);
            int x = nr % Lines.size;
            int y = nr / Lines.size;
            lines.Click(x, y);
            if (lines.IsGameOver)
            {
                object1.SetActive(true);
            }
        }
        private void InitButtons()
        {
            Debug.Log("InitButtons()");
            buttons = new Button[Lines.size, Lines.size];
            for (int nr = 0; nr < Lines.size * Lines.size; nr++)
            {
                buttons[nr % Lines.size, nr / Lines.size] = GameObject.Find($"Button ({nr})").GetComponent<Button>();
            }
        }

        private int GetNumber(string name)
        {
            Debug.Log($"GetNumber({name})");
            Regex regex = new Regex("\\((\\d+)\\)");
            Match math = regex.Match(name);
            if (!math.Success)
                throw new System.Exception();
            Group group = math.Groups[1];
            return Convert.ToInt32(group.Value);
        }
    }
}
