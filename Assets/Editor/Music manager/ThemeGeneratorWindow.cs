using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class ThemeGeneratorWindow : EditorWindow
{
    Theme _loadedTheme;
    Theme LoadedTheme
    {
        get { return _loadedTheme; }
        set
        {
            _loadedTheme = value;
            LoadTheme(value);
        }
    }
    Theme _auxTheme;

    List<Track> _tracks;
    bool _showAllTracks = true;
    Dictionary<Track, bool> _showTrack;

    string _name = "";
    string _path = "-";

    [MenuItem("Dark Room Toolkit/Theme generator %t")]
    public static void CreateWindow()
    {
        var themeGeneratorWindow = GetWindow<ThemeGeneratorWindow>();
        themeGeneratorWindow.titleContent = new GUIContent("Theme generator");
        themeGeneratorWindow.minSize = new Vector2(410, 500);
        themeGeneratorWindow.Show();
    }

    public static void CreateWindow(Theme themeToLoad)
    {
        var themeGeneratorWindow = GetWindow<ThemeGeneratorWindow>();
        themeGeneratorWindow.titleContent = new GUIContent("Theme generator");
        themeGeneratorWindow.minSize = new Vector2(410, 500);
        themeGeneratorWindow.LoadedTheme = themeToLoad;
        themeGeneratorWindow.Show();
    }

    private void Awake()
    {
        if (_loadedTheme != null)
            return;

        _showTrack = new Dictionary<Track, bool>();
        _tracks = new List<Track>();
        AddTrack();
    }

    private void OnGUI()
    {
        GUIStyle boldFoldoutStyle = new GUIStyle(EditorStyles.foldout);
        boldFoldoutStyle.fontStyle = FontStyle.Bold;
        GUIStyle boldLabelStyle = new GUIStyle();
        boldLabelStyle.fontStyle = FontStyle.Bold;
        boldLabelStyle.contentOffset = Vector2.right * 5;

        GUILayoutOption[] nameFieldSettings = { GUILayout.MaxWidth(150) };
        GUILayoutOption[] clipFieldSettings = { GUILayout.MaxWidth(150) };
        GUILayoutOption[] deleteTrackButtonSettings = { GUILayout.MaxWidth(100) };
        GUIContent loopLengthContent = new GUIContent("Length", "If 0, loop infinitely.");

        GUILayoutOption[] helpButtonSettings = { GUILayout.MaxWidth(100) };

        GUI.color = HelpDialog.helpButtonColor;
        if (GUILayout.Button("Help", helpButtonSettings))
        {
            HelpDialog.Show("<b>Editing themes</b>\n" +
                            "To edit a theme select it and press the " +
                            "<i>Edit theme</i> button.");
        }
        GUI.color = Color.white;

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        _showAllTracks = EditorGUILayout.Foldout(_showAllTracks, "Tracks", boldFoldoutStyle);

        if (_showAllTracks)
        {
            #region Tracks
            foreach (var track in _tracks.ToArray())
            {
                EditorGUILayout.BeginHorizontal();
                    _showTrack[track] = EditorGUILayout.Foldout(_showTrack[track], track.name);
                    EditorGUILayout.Space();
                    if (GUILayout.Button("Delete track", deleteTrackButtonSettings))
                        DeleteTrack(track);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                if (_showTrack.ContainsKey(track) && _showTrack[track])
                {
                    EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal();
                            track.name = EditorGUILayout.TextField(track.name, nameFieldSettings);
                            EditorGUILayout.Space();
                            track.clip = (AudioClip)EditorGUILayout.ObjectField(track.clip, typeof(AudioClip), false, clipFieldSettings);             
                        EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                        EditorGUILayout.BeginHorizontal();
                            track.loop = EditorGUILayout.Toggle("Loop", track.loop);
                            EditorGUILayout.Space();
                            if(track.loop)
                                track.loopLength = Mathf.Max(0, EditorGUILayout.IntField(loopLengthContent, track.loopLength));
                        EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }
            }
            #endregion

            if (GUILayout.Button("Add track"))
                AddTrack();
        }

        EditorGUILayout.Space();

        if (!LoadedTheme)
        {
            //Theme path
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                    GUILayout.Label("Save at", boldLabelStyle);
                    GUILayoutOption[] pathSettings = { GUILayout.MaxWidth(200), GUILayout.MinWidth(100), GUILayout.Height(20) };
                    EditorGUILayout.SelectableLabel(_path);
                EditorGUILayout.EndVertical();

            //Select path. Split it in order to get path from the root of the unity project.
                GUILayoutOption[] browseButtonSettings = { GUILayout.MaxWidth(75), GUILayout.MinWidth(60), GUILayout.Height(20) };
                if (GUILayout.Button("Browse", browseButtonSettings))
                    _path = "Assets" + EditorUtility.OpenFolderPanel("Select path", "", "")
                                                    .Split(new string[] { "Assets" }, System.StringSplitOptions.None)[1];
            EditorGUILayout.EndHorizontal();

            //Theme name
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                    GUILayout.Label("Name", boldLabelStyle);
                    GUILayoutOption[] nameInputSettings = { GUILayout.MaxWidth(200), GUILayout.MinWidth(100), GUILayout.Height(20) };
                    _name = EditorGUILayout.TextField(_name, nameInputSettings);
                EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (_path == "-")
                EditorGUILayout.HelpBox("Browse a path to save this theme.", MessageType.Warning);

            GUI.enabled = _path != "-";
        }

        else
        {
            if(CheckForChanges())
                EditorGUILayout.HelpBox("You have unsaved changes.", MessageType.Error);
        }
        
        GUILayout.FlexibleSpace();

        if(LoadedTheme)
            GUI.enabled = CheckForChanges();

        //Save/Generate theme
        if (GUILayout.Button(_loadedTheme ? "Save" : "Generate theme"))
        {
            if (LoadedTheme)
                SaveTheme();
            else
                GenerateTheme();
        }
    }

    void AddTrack()
    {
        Track newTrack = new Track("Track " + (_tracks.Count + 1));
        _tracks.Add(newTrack);
        _showTrack[newTrack] = true;
    }

    void DeleteTrack(Track trackToDelete)
    {
        _tracks.Remove(trackToDelete);
        _showTrack.Remove(trackToDelete);
    }

    void LoadTheme(Theme themeToLoad)
    {
        _tracks = new List<Track>(themeToLoad.tracks);
        foreach (var track in _tracks)
        {
            _showTrack[track] = false;
        }
        _auxTheme = themeToLoad.Clone();
    }

    bool CheckForChanges()
    {
        bool result = false;
        if (_tracks.Count != _auxTheme.tracks.Length)
            return true;
        for (int i = 0; i < _tracks.Count; i++)
        {
            result = _tracks[i].name != _auxTheme.tracks[i].name ||
                   _tracks[i].clip != _auxTheme.tracks[i].clip ||
                   _tracks[i].loop != _auxTheme.tracks[i].loop ||
                   _tracks[i].loopLength != _auxTheme.tracks[i].loopLength;
            if (result)
                break;
        }

        return result;
    }

    void SaveTheme()
    {
        LoadedTheme.tracks = _tracks.ToArray();
        EditorUtility.SetDirty(LoadedTheme);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        _auxTheme = LoadedTheme.Clone();
    }

    void GenerateTheme()
    {
        var newTheme = ScriptableObjectUtility.CreateScriptableObjectAsset<Theme>(_path, _name == "" ? "NewTheme" : _name);
        newTheme.tracks = _tracks.ToArray();
        EditorUtility.SetDirty(newTheme);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        this.LoadedTheme = newTheme;
    }
}
