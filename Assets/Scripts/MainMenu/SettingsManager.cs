using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTaSchweppes.Tools
{
 public class SettingsManager : MonoBehaviour
 {
        public void MuteAllSound() =>AudioListener.volume = 0;
 }
}