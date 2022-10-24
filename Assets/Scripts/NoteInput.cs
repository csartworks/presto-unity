using UnityEngine;
using UnityEngine.InputSystem;
namespace presto.unity
{
    public class NoteInput : MonoBehaviour
    {
        private readonly Keyboard _keyboard = Keyboard.current;
        public Staff CurrentStaff { get; private set; }
        private void Awake()
        {
            CurrentStaff = FindObjectOfType<Staff>();    
        }
        
        private void OnNoteInput(InputValue v)
        {
            int key = -1;
            if (_keyboard.aKey.isPressed) key = 0;
            else if (_keyboard.sKey.isPressed) key = 1;
            else if (_keyboard.dKey.isPressed) key = 2;
            else if (_keyboard.fKey.isPressed) key = 3;
            else if (_keyboard.gKey.isPressed) key = 4;
            else if (_keyboard.hKey.isPressed) key = 5;
            else if (_keyboard.jKey.isPressed) key = 6;
            else if (_keyboard.kKey.isPressed) key = 7;
            else if (_keyboard.lKey.isPressed) key = 8;
            if(key == -1) return;

            CurrentStaff.DrawNote("8", key - 2);
        }
    }
}
