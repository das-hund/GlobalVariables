using System;
using UnityEngine;

namespace CodeDk
{
    [Serializable, CreateAssetMenu(fileName = "IntVariable", menuName = "Global Variables/Int", order = 250)]
    public class IntVariable : GlobalVariable<int>
    { }
}
