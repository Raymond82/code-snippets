using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Let's experiment a bit with indexer-properties.
    /// Invalid properties will be kept track of in the "invalid counter".
    /// </summary>
    public class IndexTest
    {
        int[] _results = new int[10];

        int invalidCounter = 0;


        public string Name { get; set; }

        public int MaxIndex
        {
            get
            {
                if (_results != null)
                {
                    return _results.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int this[int i]
        {
            set
            {
                if (i <= MaxIndex && i >= 0)
                {
                    _results[i] += value;
                }
                else
                {
                    // Handle invalid index case.
                    invalidCounter += value;
                }
            }
        }

        public override string ToString()
        {
            // StringBuilder may be a bit more optimal but since this object has a small amount of indices this will perform fine.
            var retval = "";

            for (int i = 0; i < _results.Length; i++)
            {
                if(_results[i] > 0)
                {
                    retval += i + ": " + _results[i] + Environment.NewLine;
                }
            }
             
            if(invalidCounter > 0)
            {
                retval += "Invalid total: " + invalidCounter + Environment.NewLine;
            }

            // thought process: 
            // 1. strip newline; 
            // 2. wait, retval can be empty, check for that before returning
            // 3. may as well do it properly and strip the newline if it ends with it.

            // return retval.Substring(0, retval.Length - Environment.NewLine.Length)
            // return retval.Length >= Environment.NewLine.Length ? retval.Substring(0, retval.Length - Environment.NewLine.Length) : retval; 

            // also, this can be done ia ternary operator but that would be less clean.
            // substring will be done on Newline.Length since that might be different depending on Environment.
            if (retval.EndsWith(Environment.NewLine))
            {
                retval = retval.Substring(0, retval.Length - Environment.NewLine.Length);
            }

            return retval;
        }

    }
}
