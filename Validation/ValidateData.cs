using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assigment.Validation
{
    public class ValidateData
    {
        public static bool checkGrade(String value, float min, float max)
        {
            try
            {
                float number = float.Parse(value);
                if (number < min || number > max)
                {
                    throw new Exception();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
