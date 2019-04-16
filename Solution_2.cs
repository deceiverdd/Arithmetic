//---------------------------------------------------------------------------------------------------------------------------------
// 两数相加
//
// 给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序 的方式存储的，并且它们的每个节点只能存储 一位 数字。
// 如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。
// 您可以假设除了数字 0 之外，这两个数都不会以 0 开头。

// 示例：
// 输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
// 输出：7 -> 0 -> 8
// 原因：342 + 465 = 807
//---------------------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;

public class Solution_2 
{  
    public Solution_2()
    {
        int[] a = new int[]{9};
        int[] b = new int[]{1,9,9,9,9,9,9,9,9};

        ListNode l1 = ArrayToListNode(a);

        ListNode l2 = ArrayToListNode(b);

        //直接相加，int可能会溢出
        //AddTwoNumbersDirect(l1, l2);

        //逐位相加，然后再拼链表
        AddTwoNumbersByNode(l1, l2);
    }

    ListNode AddTwoNumbersDirect(ListNode l1, ListNode l2)
    {        
        int sum = GetValue(l1) + GetValue(l2);
        
        if(sum != 0)
        {
            ListNode l = new ListNode(sum%10);
            sum = sum/10;
            ListNode temp = l;
            while(sum != 0)
            {
                temp.next = new ListNode(sum%10);
                sum = sum/10;
                temp = temp.next;
            }
            
            return l;
        }
        else
        {
            return new ListNode(0);
        }                
    }
    
    ListNode AddTwoNumbersByNode(ListNode l1, ListNode l2)
    {
        int carry = 0;
        ListNode result = new ListNode(0);
        ListNode curNode = result;

        while (l1 != null || l2 != null)
        {
            int a = (l1 != null) ? l1.val : 0;
            int b = (l2 != null) ? l2.val : 0; 

            int sum = a + b + carry;
            carry = sum / 10;

            curNode.next = new ListNode(sum % 10);
            curNode = curNode.next;

            if(l1 != null) l1 = l1.next;
            if(l2 != null) l2 = l2.next;
        }
        if(carry > 0)
        {
            curNode.next = new ListNode(carry);
        }

        return result.next;
    }

    int GetValue(ListNode l)
    {
        int length = 0;
        int value = 0;
        
        while(l != null)
        {
            value += l.val * (int)MathF.Pow(10,length);
            length++;
            l = l.next;
        }
        
        return value;
    }

    ListNode ArrayToListNode(int[] a)
    {
        if(a == null)
            return null;

        ListNode l = new ListNode(a[0]);
        ListNode temp = l;

        for(int i = 1; i < a.Length; i++)
        {
            temp.next = new ListNode(a[i]);
            temp = temp.next;
        }

        return l;
    }
}

    
public class ListNode
{
    public int val;     
    public ListNode next;
    public ListNode(int x) { val = x; }
}