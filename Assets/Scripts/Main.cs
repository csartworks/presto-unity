using presto.parser;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace presto.unity
{
    public class Main : MonoBehaviour
    {
        public static Main instance;
        public static Dictionary<string, GlyphName> GlyphNames { get; private set; } = new();
        public static FontMetaData MetaData { get; private set; }

        public GameObject Staff1Line { get => _staff1Line; }
        public GameObject Leger { get => _leger; }
        public GameObject BarlineSingle { get => _barlineSingle; }
        public GameObject Glyph { get => _glyph; }
        public GameObject Note { get => _note; }

        [SerializeField] private GameObject _staff1Line;
        [SerializeField] private GameObject _leger;
        [SerializeField] private GameObject _barlineSingle;
        [SerializeField] private GameObject _glyph;
        [SerializeField] private GameObject _note;

        private void Awake()
        {
            instance = this;
            var json = Resources.Load<TextAsset>("raw/glyphnames");
            GlyphNames = JsonParser.Parse(json.text);
            using StreamReader reader = new("Assets/Fonts/bravura_metadata.json");
            MetaData = MetaDataParser.Parse(reader.ReadToEnd());
            GlyphBehaviour.engv = MetaData.engravingDefaults;
            GlyphBehaviour.glyphs = GlyphNames;
        }
        private void Update()
        {
            //Input.GetKey(KeyCode.A)
        }
    }
}