using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ChatFilter
{
    class SlangNode
    {
        Hashtable m_Hash = new Hashtable();
        bool m_IsLeafNode;

        // -------------------------------------------------------------------------------------------------
        public bool IsLeafNode
        {
            get { return m_IsLeafNode; }
            set { m_IsLeafNode = value; }
        }

        // -------------------------------------------------------------------------------------------------
        public SlangNode AddChild(char ch)
        {
            SlangNode resultNode = FindChild(ch);

            if (resultNode == null)
            {
                resultNode = new SlangNode();
                m_Hash.Add(ch, resultNode);
            }
            return resultNode;
        }

        // -------------------------------------------------------------------------------------------------
        public SlangNode FindChild(char ch)
        {
            foreach (var key in m_Hash.Keys)
            {
                if (key.Equals(ch))
                    return m_Hash[key] as SlangNode;
            }
            return null;
        }
    }
}
