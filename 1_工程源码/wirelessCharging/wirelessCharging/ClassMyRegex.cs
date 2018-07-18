using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiaHao.RegexHelp
{
    class ClassMyRegex
    {
        /// <summary>
        /// 字母加数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool RegexIsAlphanumericMatch(string input)
        {
            string RegexStr = string.Empty;
            RegexStr = @"^(-?\d+)(\.\d+)?$";

            if (System.Text.RegularExpressions.Regex.IsMatch(input, RegexStr))
            {
                //Console.WriteLine("ok");
                return true;
            }
            else
            {
                //Console.WriteLine("error");
                return false;
            }
        }
        /// <summary>
        /// 0+正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool RegexIsPosIntMatch(string input)
        {
            string RegexStr = string.Empty;
            RegexStr = @"^[1-9]\d*$";///@"^(0|[1-9]\d*)$" //含0的正整数
            
            if (System.Text.RegularExpressions.Regex.IsMatch(input, RegexStr))
            {
                //Console.WriteLine("ok");
                return true;
            }
            else
            {
                //Console.WriteLine("error");
                return false;
            }
        }
        public bool RegexIsUintMatch(string input)
        {
            string RegexStr = string.Empty;
            RegexStr = @"^[0-9]*[1-9][0-9]*$";///@"^(0|[1-9]\d*)$" //不含0的正整数

            if (System.Text.RegularExpressions.Regex.IsMatch(input, RegexStr))
            {
                //Console.WriteLine("ok");
                return true;
            }
            else
            {
                //Console.WriteLine("error");
                return false;
            }
        }

    }
}
