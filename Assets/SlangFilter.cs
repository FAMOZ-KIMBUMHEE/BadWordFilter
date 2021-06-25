using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatFilter
{
    class SlangFilter
    {
#region IMPL

        public class IMPL
        {
            SlangNode m_Root = new SlangNode();

            public SlangNode Root
            {
                get { return m_Root; }
            }
        }

#endregion

        IMPL m_IMPL = new IMPL();

        // -------------------------------------------------------------------------------------------------
        public string Filter(string original)
        {
            string result = original;
            string convert = ConversionString(original);

            for (int start = 0; start < convert.Length; )
            {
                int count = Match(convert.Substring(start, convert.Length - start));
                if (count > 0)
                {
                    result = Replace(result.ToCharArray(), start, count);
                    start += count;
                }
                else start++;
            }
            return result;
        }

        // -------------------------------------------------------------------------------------------------
        public void AddSlang(string slang)
        {
            if (string.IsNullOrEmpty(slang)) return;
            string convert = ConversionString(slang);

            SlangNode currentNode = m_IMPL.Root;
            for (int i = 0; i < convert.Length; i++)
            {
                if (currentNode != null)
                    currentNode = currentNode.AddChild(convert[i]);
            }

            if (currentNode != null)
                currentNode.IsLeafNode = true;
        }

        // -------------------------------------------------------------------------------------------------
        //int Match(string text)
        //{
        //    SlangNode currentNode = m_IMPL.Root;

        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        currentNode = currentNode.FindChild(text[i]);
        //        if (currentNode == null)
        //            return 0;

        //        if (currentNode.IsLeafNode)
        //            return i + 1;
        //    }
        //    return 0;
        //}

        // -------------------------------------------------------------------------------------------------
        // 수정된 버전. (마지막 리프노드 까지 검사)
        int Match(string text)
        {
            SlangNode currentNode = m_IMPL.Root;

            int matchCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                currentNode = currentNode.FindChild(text[i]);
                if (currentNode == null)
                    return matchCount;

                if (currentNode.IsLeafNode)
                    matchCount = i + 1;
            }
            return matchCount;
        }

        // -------------------------------------------------------------------------------------------------
        string Replace(char[] charArray, int start, int count)
        {
            for (int i = start; i < start + count; i++)
                charArray[i] = '*';

            return new string(charArray);
        }

        // -------------------------------------------------------------------------------------------------
        string ConversionString(string text)
        {
            return text.ToUpper();
        }
    }
}
