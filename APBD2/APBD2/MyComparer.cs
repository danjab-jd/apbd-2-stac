using APBD2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace APBD2
{
    public class MyComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.Name} {x.Surname} {x.Index}", $"{y.Name} {y.Surname} {y.Index}");
        }

        public int GetHashCode([DisallowNull] Student obj)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .GetHashCode($"{obj.Name} {obj.Surname} {obj.Index}");
        }
    }
}
