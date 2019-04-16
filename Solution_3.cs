//-----------------------------------------------------------------------------
//无重复字符的最长子串
//
// 给定一个字符串，请你找出其中不含有重复字符的 最长子串 的长度。
// 示例 1:
// 输入: "abcabcbb"
// 输出: 3 
// 解释: 因为无重复字符的最长子串是 "abc"，所以其长度为 3。
//
// 示例 2:
// 输入: "bbbbb"
// 输出: 1
// 解释: 因为无重复字符的最长子串是 "b"，所以其长度为 1。
//
// 示例 3:
// 输入: "pwwkew"
// 输出: 3
// 解释: 因为无重复字符的最长子串是 "wke"，所以其长度为 3。
//
// 请注意，你的答案必须是 子串 的长度，"pwke" 是一个子序列，不是子串。
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public class Solution_3
{
    public Solution_3()
    {
        string s = "pwwkew";

        int result = LengthOfLongestSubstring_3(s);

        Console.WriteLine(result);
    }

    //暴力算法
    public int LengthOfLongestSubstring_0(string str)
    {
        Queue<char> sub = new Queue<char>();
        int maxResult = 0;

        for (int i = 0; i < str.Length; i++)
        {
            sub.Enqueue(str[i]);

            for (int j = i + 1; j < str.Length; j++)
            {
                if (!sub.Contains(str[j]))
                {
                    sub.Enqueue(str[j]);
                }
                else
                {
                    break;
                }
            }

            if (sub.Count > maxResult)
                maxResult = sub.Count;

            sub.Clear();
        }

        return maxResult;
    }
    
    //队列滑动条
    public int LengthOfLongestSubstring_1(string str)
    {
        int maxResult = 0;
        Queue<char> sub = new Queue<char>();

        for (int i = 0; i < str.Length; i++)
        {
            while (true)
            {
                if (!sub.Contains(str[i]))
                {
                    sub.Enqueue(str[i]);
                    if (sub.Count > maxResult)
                        maxResult = sub.Count;

                    break;
                }
                else
                {
                    sub.Dequeue();
                }
            }
        }

        return maxResult;
    }

    //优化后的滑动条
    public int LengthOfLongestSubstring_2(string str)
    {
        int maxResult = 0;
        string sub = "";

        for (int i = 0; i < str.Length; i++)
        {
            if (sub.Contains(str[i]))
            {
                sub = sub.Substring(sub.LastIndexOf(str[i]) + 1, sub.Length - sub.LastIndexOf(str[i]) - 1);
            }

            sub = String.Format("{0}{1}", sub, str[i]);
            maxResult = Math.Max(maxResult, sub.Length);
        }

        return maxResult;
    }

    //精简版的滑动条，只记录序号，不记录数据
    public int LengthOfLongestSubstring_3(string str)
    {
        //用来记录每种字符最靠后的位置索引
        Dictionary<char, int> st = new Dictionary<char, int>();

        //i是滑动条的头部，j是滑动条的尾部
        int i = 0;
        int ans = 0;

        for (int j = 0; j < str.Length; j++)
        {
            //如果遇到重复的就移动滑动条的头部，相当于把重复字符前面的字符串从滑动条中切掉
            if (st.ContainsKey(str[j]))
                i = Math.Max(st[str[j]], i);

            ans = Math.Max(ans, j - i + 1);

            if (!st.ContainsKey(str[j]))
                st.Add(str[j], j + 1);
            else
                st[str[j]] = j + 1;
        }

        return ans;
    }
}