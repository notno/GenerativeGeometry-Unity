using UnityEngine;

/// <summary>
/// Display a field as read-only in the inspector.
/// CustomPropertyDrawers will not work when this attribute is used.
/// </summary>
/// <seealso cref="BeginReadOnlyGroupAttribute"/>
/// <seealso cref="EndReadOnlyGroupAttribute"/>
public class ReadOnlyAttribute : PropertyAttribute { }

/// <summary>
/// Display one or more fields as read-only in the inspector.
/// Use <see cref="EndReadOnlyGroupAttribute"/> to close the group.
/// Works with CustomPropertyDrawers.
/// </summary>
/// <seealso cref="EndReadOnlyGroupAttribute"/>
/// <seealso cref="ReadOnlyAttribute"/>
public class BeginReadOnlyGroupAttribute : PropertyAttribute { }

/// <summary>
/// Use with <see cref="BeginReadOnlyGroupAttribute"/>.
/// Close the read-only group and resume editable fields.
/// </summary>
/// <seealso cref="BeginReadOnlyGroupAttribute"/>
/// <seealso cref="ReadOnlyAttribute"/>
public class EndReadOnlyGroupAttribute : PropertyAttribute { }

     
//     public class ReadOnlyExample : MonoBehaviour
//     {
//         [BeginReadOnlyGroup] // tag a group of fields as ReadOnly
//         public string a;
//         public int b;
//         public Material c;
//         public List<int> d = new List<int>();
//         public CustomTypeWithPropertyDrawer e; // Works!
//         [EndReadOnlyGroup]
//         [ReadOnly] public string a2;
//         [ReadOnly] public CustomTypeWithPropertyDrawer e2; // DOES NOT USE CustomPropertyDrawer!
//         [BeginReadOnlyGroup]
//         public int b2;
//         public Material c2;
//         public List<int> d2 = new List<int>();
//         // Attribute tags apply to the next field of which there are no more so Unity/C# complains.
//         // Since there are no more fields, we can omit the closing tag.
//         // [EndReadOnlyGroup]
//     }     