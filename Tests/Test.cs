using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test : StateBehaviour<Test.S>
{
    public enum S { TestState };

    void Awake()
    {
        /*Init(new Dictionary<S, StateFunctions>
        {
            { S.TestState, new StateFunctions { } }
        });*/

        var test_renderers = new List<SpriteRenderer>().AddComponents(4, (i) =>
        {
            var r = JLib.New<SpriteRenderer>("PosTarget Test").Init(null, AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd"));
            r.transform.localPosition = new Vector3(0, -i * 1);
            return r;
        });

        var posTarget0 = test_renderers[0].Add<PosTarget>().Init(new Vector3(1, 0, 0), 120, axes: new Vector3(1, 0, 0), eased: EaseType.None);
        Motion.InvokeChain((m) => m.OnEnd,
            posTarget0,
            test_renderers[0].Copy(posTarget0).Init(new Vector3(0, 1, 0), axes: new Vector3(0, 1, 0)),
            test_renderers[0].Copy(posTarget0).Init(new Vector3(0, 0, 0)),
            test_renderers[0].Copy(posTarget0).Init(new Vector3(0, 0, 0), axes: new Vector3(0, 1, 0))
        );

        var posTarget1 = test_renderers[1].Add<PosTarget>().Init(new Vector3(1, 0, 0), 120, axes: new Vector3(1, 0, 0), eased: EaseType.Out);
        Motion.InvokeChain((m) => m.OnEnd,
            posTarget1,
            test_renderers[1].Copy(posTarget1).Init(Vector3.zero)
        );

        var posTarget2 = test_renderers[2].Add<PosTarget>().Init(new Vector3(1, 0, 0), 120, axes: new Vector3(1, 0, 0), eased: EaseType.In);
        Motion.InvokeChain((m) => m.OnEnd, posTarget2, test_renderers[2].Copy(posTarget2).Init(Vector3.zero));

        var posTarget3 = test_renderers[3].Add<PosTarget>().Init(new Vector3(1, 0, 0), 120, axes: new Vector3(1, 0, 0), eased: EaseType.Both);
        Motion.InvokeChain((m) => m.OnEnd, posTarget3, test_renderers[3].Copy(posTarget3).Init(Vector3.zero));
    }

    public override void Update()
    {
        base.Update();
    }
}
