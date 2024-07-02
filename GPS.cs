using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRageMath;

namespace IngameScript
{
    public struct GPS
    {
        public string OriginalString { get; }
        public string Name { get; }
        public Vector3D Position { get; }

        public GPS(string originalString, string name, Vector3D position)
        {
            OriginalString = originalString;
            Name = name;
            Position = position;
        }

        public static bool TryParse(string input, out GPS result)
        {
            //support the stupid colon in the name aka the instance gpses
            //gps format: GPS:name:x:y:z:#color:folder:

            result = default(GPS);

            int startIndex = input.IndexOf("gps:", StringComparison.CurrentCultureIgnoreCase);
            if (startIndex < 0)
            {
                return false;
            }

            input = input.Substring(startIndex, input.LastIndexOf(':') + 1 - startIndex);
            string[] parts = input.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            string name;
            Vector3D pos;
            for (int i = 2; i < parts.Length - 2; i++)
            {
                if (double.TryParse(parts[i], out pos.X) &&
                    double.TryParse(parts[i + 1], out pos.Y) &&
                    double.TryParse(parts[i + 2], out pos.Z))
                {
                    name = String.Join(":", parts.Skip(1).Take(i - 1));
                    result = new GPS(input, name, pos);
                    return true;
                }
            }

            return false;
        }
    }
}
