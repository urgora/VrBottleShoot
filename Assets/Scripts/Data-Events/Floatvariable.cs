
using UnityEngine;
[CreateAssetMenu(fileName ="FloatObj",menuName ="Data/FloatData")]
public class Floatvariable : ScriptableObject
{
    public int value;

    public void SetValue(int f)
    {
        value = f;
    }

    public void applyChange(int f)
    {
        value += f;
    }

}
