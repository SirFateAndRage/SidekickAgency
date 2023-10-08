using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MusicManager))]
public class MusicManagerEditor : Editor
{
    MusicManager _target;

    bool _showAllTransitions;
    Dictionary<Transition, bool> _showTransition = new Dictionary<Transition, bool>();

    private void OnEnable()
    {
        _target = (MusicManager)target;

        if(_target.transitions.Count > 0)
        {
            foreach (var transition in _target.transitions)
            {
                _showTransition[transition] = false;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        _target.playOnStart = EditorGUILayout.Toggle("Play on start", _target.playOnStart);

        if (_target.playOnStart)
            _target.themeToPlay = (Theme)EditorGUILayout.ObjectField("Theme to play", _target.themeToPlay, typeof(Theme), false);

        EditorGUILayout.Space();

        GUIStyle allTransitionsStyle = new GUIStyle(EditorStyles.foldout);
        allTransitionsStyle.fontStyle = FontStyle.Bold;

        _showAllTransitions = EditorGUILayout.Foldout(_showAllTransitions, "Transitions", allTransitionsStyle);

        if (_showAllTransitions)
        {
            GUILayoutOption[] removeButtonSettings = { GUILayout.Width(75) };

            foreach (var transition in _target.transitions.ToArray())
            {
                EditorGUILayout.BeginHorizontal();
                    _showTransition[transition] = EditorGUILayout.Foldout(_showTransition[transition], transition.name);
                    if (GUILayout.Button("Remove", removeButtonSettings))
                        RemoveTransition(transition);
                EditorGUILayout.EndHorizontal();

                if (_showTransition.ContainsKey(transition) && _showTransition[transition])
                {
                    EditorGUILayout.BeginVertical();
                        EditorGUILayout.BeginHorizontal();
                            transition.name = EditorGUILayout.TextField(transition.name);
                            EditorGUILayout.Space();
                            transition.themeToTransitionTo = (Theme)EditorGUILayout.ObjectField(transition.themeToTransitionTo, typeof(Theme), false);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                    #region Transition parameters
                        EditorGUILayout.BeginHorizontal();
                            transition.transitionMode = (TransitionModes)EditorGUILayout.EnumPopup(transition.transitionMode);
                            switch (transition.transitionMode)
                                {
                                    case TransitionModes.Sudden:
                                    EditorGUILayout.Space();
                                        break;
                                    case TransitionModes.FadeOut:
                                    EditorGUILayout.BeginVertical();
                                        transition.floatParameters[0] = Mathf.Max(0, EditorGUILayout.FloatField("Fade out speed", transition.floatParameters[0]));
                                        transition.boolParameters[0] = EditorGUILayout.Toggle("Wait end of track", transition.boolParameters[0]);
                                    EditorGUILayout.EndVertical();
                                        break; 
                                    case TransitionModes.Mix:
                                    transition.floatParameters[0] = Mathf.Max(0, EditorGUILayout.FloatField("Transition speed", transition.floatParameters[0]));
                                        break;
                                    default:
                                        break;
                                }
                        EditorGUILayout.EndHorizontal();
                    #endregion

                    //Mix transition alert for missing audio source
                    if (transition.transitionMode == TransitionModes.Mix && _target.GetComponents<AudioSource>().Length < 2)
                    {
                        EditorGUILayout.HelpBox("Mix transition needs to audio sources in order to work", MessageType.Warning);
                        if (GUILayout.Button("Add audio source"))
                            _target.gameObject.AddComponent<AudioSource>();
                    }

                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Space();
            }

            GUILayoutOption[] addTransitionButtonSettings = { GUILayout.MaxWidth(100) };

            if (GUILayout.Button("Add transition", addTransitionButtonSettings))
                AddTransition();
        }

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(_target);
    }

    void AddTransition()
    {
        var newTransition = new Transition("Transition " + (_target.transitions.Count + 1));
        _target.transitions.Add(newTransition);
        _showTransition[newTransition] = true;
    }

    void RemoveTransition(Transition transitionToRemove)
    {
        _target.transitions.Remove(transitionToRemove);
        _showTransition.Remove(transitionToRemove);
    }
}
