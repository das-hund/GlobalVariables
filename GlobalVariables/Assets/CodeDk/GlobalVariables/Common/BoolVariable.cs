using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "BoolVariable", menuName = "Global Variables/Bool", order = 250)]
    public class BoolVariable : GlobalVariable<bool>
    { }
}
