using UnityEngine;

public enum Dir { Right = 0, Up = 90, Left = 180, Down = 270 };
public enum DDir { Right = 0, UpRight = 45, Up = 90, UpLeft = 135,
    Left = 180, DownLeft = 225, Down = 270, DownRight = 315 };

public struct InputData
{

    public Vector2 Press, Release;

    /// <summary> degrees </summary>
    public float Angle { get
        {
            return Vector2.Angle(Press, Release);
        }
    }

    /// <param name="angle">degrees, start due right, go counterclockwise</param>
    /// <param name="divisions">number of directions. 4 for 90 angles, 8 for diagonal</param>
    public bool CheckAngle(float angle, int divisions = 4)
    {
        return Mathf.Abs(Angle - angle) < 360 / divisions / 2;
    }

    public bool CheckAngle(Dir dir)
    {
        return CheckAngle((float)dir, 4);
    }

    /// <summary> checks diagonally </summary>
    public bool CheckAngle(DDir dir)
    {
        return CheckAngle((float)dir, 8);
    }

    /// <param name="divisions">number of directions. 4 for 90 angles, 8 for diagonal</param>
    /// <param name="offset">initial angle. 0 is right, go counterclockwise</param>
    /// <returns></returns>
    /*public int Dir(int divisions = 4, float offset = 0)
    {
        return JMath.Mod((int)((Angle + offset) / 360 * divisions), divisions);
    }*/



}
