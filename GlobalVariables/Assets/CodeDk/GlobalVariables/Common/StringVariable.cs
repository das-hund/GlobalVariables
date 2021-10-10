using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "StringVariable", menuName = "Global Variables/String", order = 250)]
    public class StringVariable : GlobalVariable<string>
    { }
}
