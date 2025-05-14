// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.UI;

// public class DialogueManager : MonoBehaviour
// {
//     [SerializeField] GameObject dialogBox;
//     [SerializeField] Text dialogText;
//     [SerializeField] int letterPerSecond;

//     public static DialogueManager Instance { get; private set;}

//     //expose dialog manager to other scripts. it could cause dependency issue if abused.
//     private void Awake() {
//         Instance = this;
//     }
    
//     public void ShowDialog(Dialog dialog){
//         dialogBox.SetActive(true);
//         foreach (var line in dialog.Lines) {
//             StartCoroutine(TypeDialog(dialog.Lines[0]));
//         }
//     }
//     //this displays letters one by one
//     public IEnumerator TypeDialog(string line) {
//         dialogText.text = "";
//         foreach (var letter in line.ToCharArray()) {
//             dialogText.text += letter;
//             yield return new WaitForSecondsRealtime(0.1f / letterPerSecond);
//         }
//     }
// }
